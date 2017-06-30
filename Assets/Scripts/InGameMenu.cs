using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    private bool gamePaused = false;

    public AudioClip menuMusic;
    public AudioClip levelMusic;
	public Animation watchAnimation;

    private AudioSource source;

	void Start () {
        source = GetComponent<AudioSource>();
		watchAnimation = GetComponent<Animation>();
	}
	
	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Escape) && !watchAnimation.isPlaying)
		{
            if (gamePaused)
            {
                Time.timeScale = 1;
                source.Stop();
                source.PlayOneShot(levelMusic);
				watchAnimation["Watch"].speed = -1;
				watchAnimation["Watch"].time = watchAnimation["Watch"].length;
				watchAnimation.Play();
            }
            else
            {
                Time.timeScale = 0;
                source.Stop();
                source.PlayOneShot(menuMusic, 1f);
				watchAnimation["Watch"].speed = 1;
				watchAnimation["Watch"].time = 0;
				watchAnimation.Play();
            }
            gamePaused ^= true;
        }
	}
}
