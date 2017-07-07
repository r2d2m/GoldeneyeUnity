using UnityEngine.SceneManagement;
using UnityEngine;

public class EndMission : MonoBehaviour {

	private bool finishing = false;
	private Camera camera;

	void Start()
	{
		camera = GameObject.Find("Main Camera").GetComponent<Camera>();
	}

	void Update()
	{
		if (finishing)
		{
			camera.fieldOfView -= Time.deltaTime * 50;
		}
		if (camera.fieldOfView <= 0)
		{
			finishing = false;
			SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
		}
	}

	void OnTriggerEnter(Collider hit)
	{
		if (hit.gameObject.tag == "Player")
		{
			finishing = true;
		}
	}
}
