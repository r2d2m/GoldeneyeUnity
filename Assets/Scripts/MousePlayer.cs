using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePlayer : MonoBehaviour
{
    public float mouseMultiplier;
    float rotationX = 0f;

    void Start()
    {
        Cursor.visible = false;
		Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    void Update()
    {
        rotationX += Input.GetAxis("Mouse X") * 100f * Time.deltaTime;
        transform.localEulerAngles = new Vector3(0, rotationX * mouseMultiplier, 0);
    }
}
