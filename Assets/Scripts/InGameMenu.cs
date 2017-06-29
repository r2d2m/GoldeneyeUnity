using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    private bool gamePaused = false;

    public AudioClip menuMusic;
    public AudioClip levelMusic;

    AudioSource source;

	void Start () {
        source = GetComponent<AudioSource>();
	}
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                //Time.timeScale = 1;
                source.Stop();
                source.PlayOneShot(levelMusic);
                //MoveCameraToWatch();
            }
            else
            {
                //Time.timeScale = 0;
                source.Stop();
                source.PlayOneShot(menuMusic, 1f);
                //MoveCameraToWatch();
            }
            gamePaused ^= true;
        }
	}

    void MoveCameraToWatch ()
    {
        Debug.Log(transform.localEulerAngles);
        if (transform.localEulerAngles.y > -45f)
        {
            transform.localEulerAngles = new Vector3(10f, 0);
        }
        else if (Camera.main.fieldOfView > 15)
        {
            Camera.current.fieldOfView -= 0.1f;
        }
    }
}
