using UnityEngine;
using System.Collections;

public class MouseCamera : MonoBehaviour
{
	public float minY = -45f;
	public float maxY = 45f;

	float rotationY = 0f;

	void Start()
	{ 
		Cursor.visible = false;
	}

	void Update()
	{
		rotationY += Input.GetAxis("Mouse Y") * 100f * Time.deltaTime;
		rotationY = Mathf.Clamp(rotationY, minY, maxY);
		transform.localEulerAngles = new Vector3(-rotationY, 0);
	}
}