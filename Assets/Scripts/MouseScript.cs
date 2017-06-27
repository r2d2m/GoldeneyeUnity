using UnityEngine;
using System.Collections;

public class MouseScript : MonoBehaviour
{
	public float minY = -60f;
	public float maxY = 60f;

	float rotationY = 0f;

	void Start()
	{ 
		Cursor.visible = false;
	}

	void Update()
	{
		rotationY += Input.GetAxis("Mouse Y") * 100f * Time.deltaTime;
		rotationY = Mathf.Clamp(rotationY, minY, maxY);
		transform.localEulerAngles = new Vector3(-rotationY, 0, 0);
	}
}