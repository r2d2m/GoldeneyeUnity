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

    private AudioSource source;
    private Animator animator;
    private GameObject go;

	void Start () {
        source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
	}
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isAnimated)
		{
            isAnimated = true;
            if (gamePaused)
            {
                Time.timeScale = 1;
                source.Stop();
                source.PlayOneShot(levelMusic);
            }
            else
            {
                go = GameObject.Find("Menu");
                go.GetComponent<Image>().enabled = false;
                go.GetComponent<AudioSource>().PlayOneShot(disableWatch);
                go = GameObject.Find("MenuBackground");
                go.GetComponent<Image>().enabled = false;
                source.Stop();
                source.PlayOneShot(menuMusic, 1f);
            }
            gamePaused = !gamePaused;
            animator.SetBool("gamePaused", gamePaused);
        }
	}

    private void EventCanEnterMenu()
    {
        isAnimated = false;
    }

    private void EventCanExitMenu()
    {
        go = GameObject.Find("Menu");
        go.GetComponent<Image>().enabled = true;
        go.GetComponent<AudioSource>().PlayOneShot(enableWatch);
        go = GameObject.Find("MenuBackground");
        go.GetComponent<Image>().enabled = true;
        Time.timeScale = 0;
        isAnimated = false;
    }
}
