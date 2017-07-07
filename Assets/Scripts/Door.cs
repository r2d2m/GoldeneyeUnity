using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	public GameObject door;
	private int currentStep = 0;
	private bool opening = false;
	private bool moving = false;
	private int steps = 400;

	void Update()
	{
		if (moving && currentStep > 0 && opening) {
			door.transform.position = new Vector3 (
				door.transform.position.x - Time.deltaTime,
				door.transform.position.y,
				door.transform.position.z
			);
			--currentStep;
		} else if (moving && currentStep < steps && !opening) {
			door.transform.position = new Vector3 (
				door.transform.position.x + Time.deltaTime,
				door.transform.position.y,
				door.transform.position.z
			);
			++currentStep;
		} else {
			moving = false;
		}
	}

	void OnTriggerStay(Collider hit)
	{
		if (hit.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
		{
			opening = !opening;
			currentStep = (opening ? steps : 0);
			moving = true;
		}
	}
}
