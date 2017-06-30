using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    public bool gamePaused = false;
    private bool isAnimated = false;

    public AudioClip menuMusic;
    public AudioClip levelMusic;

    private AudioSource source;

	void Start () {
        source = GetComponent<AudioSource>();
	}
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isAnimated)
		{
            isAnimated = true;
            if (gamePaused)
            {
                //Time.timeScale = 1;
                source.Stop();
                source.PlayOneShot(levelMusic);
            }
            else
            {
                //;Time.timeScale = 0;
                source.Stop();
                source.PlayOneShot(menuMusic, 1f);
            }
            gamePaused ^= true;
        }
	}

    private void EventCanExitMenu()
    {
        isAnimated = false;
    }
}
