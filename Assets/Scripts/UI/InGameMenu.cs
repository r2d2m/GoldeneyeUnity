﻿using UnityEngine;
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

    private Image menu;
    private Image menuBackground;
    private AudioSource watch;
    private Text[] missions;

    private Mission _mission;

	void Start () {
        source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        GameObject goMenu = GameObject.Find("Menu");
        GameObject goBackground = GameObject.Find("MenuBackground");

        menu = goMenu.GetComponent<Image>();
        menuBackground = goBackground.GetComponent<Image>();
        watch = goMenu.GetComponent<AudioSource>();
        missions = goMenu.GetComponentsInChildren<Text>();

        _mission = GameObject.Find("HUD").GetComponent<Mission>();
        foreach(Objective o in _mission.GetObjectives()) {
            Text text = goMenu.AddComponent<Text>();
            text.text = o.GetDescription();
            Font font = (Font)Resources.GetBuiltinResource(typeof(Font), "007 GoldenEye");
            text.font = font;
            text.material = font.material;
        }
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

    }

    private void EventCanEnterMenu()
    {
        isAnimated = false;
        animator.enabled = false;
    }

    private void EventCanExitMenu()
    {
        menu.enabled = true;
        watch.PlayOneShot(enableWatch);
        menuBackground.enabled = true;
        foreach (Text t in missions)
        {
            t.enabled = true;
        }
        Time.timeScale = 0;
        isAnimated = false;
    }
}
