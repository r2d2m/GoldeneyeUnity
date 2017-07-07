using UnityEngine.SceneManagement;
using UnityEngine;

public class EndMission : MonoBehaviour {

	private bool finishing = false;
	private Camera camera;
	private Mission _mision;

	void Start()
	{
		camera = GameObject.Find("Main Camera").GetComponent<Camera>();
		_mision = GameObject.Find("HUD").GetComponent<Mission>();
	}

	void Update()
	{
		if (finishing)
		{
			camera.fieldOfView -= Time.deltaTime * 50;
			_mision.UpdateProgress(2, false);
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
