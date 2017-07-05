using UnityEngine;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    private bool gamePaused = false;
    private bool isAnimated = false;

    public AudioClip menuMusic;
    public AudioClip levelMusic;

    public AudioClip enableWatch;
    public AudioClip disableWatch;

    public Font font;

    private AudioSource source;
    private Animator animator;

    private Image menu;
    private Image menuBackground;
    private AudioSource watch;
    private Text[] missions;

    private GameObject player;
    private GameObject camera;

    private Mission _mission;

	void Start () {
        source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        GameObject goMenu = GameObject.Find("Menu");
        GameObject goBackground = GameObject.Find("MenuBackground");

        menu = goMenu.GetComponent<Image>();
        menuBackground = goBackground.GetComponent<Image>();
        watch = goMenu.GetComponent<AudioSource>();

        _mission = GameObject.Find("HUD").GetComponent<Mission>();
        foreach(Objective objective in _mission.GetObjectives()) {
            string objectiveName = "Objective-" + objective.GetId().ToString();
            GameObject.Find(objectiveName).GetComponent<Text>().text =
                char.ConvertFromUtf32(65 + objective.GetId()) + "- " + objective.GetDescription();
            string statusName = "Status-" + objective.GetId().ToString();
            GameObject.Find(statusName).GetComponent<Text>().text = objective.GetStatus().ToString();
        }

        player = GameObject.Find("007");
        camera = GameObject.Find("Main Camera");
	}
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isAnimated)
		{
            isAnimated = true;
            if (gamePaused)
            {
				Time.timeScale = 1;
                menu.enabled = false;
				watch.PlayOneShot(disableWatch);
				menuBackground.enabled = false;
                foreach (Text t in missions)
                {
                    t.enabled = false;
                }
				source.Stop();
                source.PlayOneShot(levelMusic);
            }
            else
            {
                player.GetComponent<MovementScript>().enabled = false;
                player.GetComponent<MousePlayer>().enabled = false;
                camera.GetComponent<MouseCamera>().enabled = false;

                animator.enabled = true;
				source.Stop();
                source.PlayOneShot(menuMusic, 1f);
            }
            gamePaused = !gamePaused;
            animator.SetBool("gamePaused", gamePaused);
        }
	}

    public void UpdateMissionStatus(int id, MissionStatus status)
    {
        missions = GameObject.Find("Menu").GetComponentsInChildren<Text>();
        foreach (Text t in missions)
        {
            if (t.name.StartsWith("Status-") && t.name.Contains(id.ToString()))
            {
                t.text = status.ToString();
                if (status == MissionStatus.Completed)
                {
                    t.fontStyle = FontStyle.Bold;
                }
                else if (status == MissionStatus.Failed)
                {
                    t.color = new Color(255, 0, 0);
                }
                return;
            }
        }
    }

    private void EventCanEnterMenu()
    {
        isAnimated = false;
        animator.enabled = false;
        player.GetComponent<MovementScript>().enabled = true;
        player.GetComponent<MousePlayer>().enabled = true;
        camera.GetComponent<MouseCamera>().enabled = true;
    }

    private void EventCanExitMenu()
    {
        menu.enabled = true;
        watch.PlayOneShot(enableWatch);
        menuBackground.enabled = true;
        missions = GameObject.Find("Menu").GetComponentsInChildren<Text>();
        foreach (Text t in missions)
        {
            t.enabled = true;
        }
        Time.timeScale = 0;
        isAnimated = false;
    }
}
