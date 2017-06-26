using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        
        Debug.DrawLine(new Vector3(-2f, -2f, 0f), new Vector3(-2f, 2f, 0f), Color.red);
        Debug.DrawLine(new Vector3(-2f, 2f, 0f), new Vector3(2f, 2f, 0f), Color.red);
        Debug.DrawLine(new Vector3(2f, 2f, 0f), new Vector3(2f, -2f, 0f), Color.red);
        Debug.DrawLine(new Vector3(2f, -2f, 0f), new Vector3(-2f, -2f, 0f), Color.red);

        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;
        float movement = Time.deltaTime * 3;

        if (Mathf.Abs(x) >= 2.0f || Mathf.Abs(y) >= 2.0f) {
            LoadMyLevel();
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.position = new Vector3(x, y + movement, z);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position = new Vector3(x, y - movement, z);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.position = new Vector3(x - movement, y, z);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector3(x + movement, y, z);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            float scaleX = transform.localScale.x;
            float scaleY = transform.localScale.y;
            float scaleZ = transform.localScale.z;
            transform.localScale = new Vector3(scaleX + 0.1f * scaleX, scaleY + 0.1f * scaleY, scaleZ + 0.1f * scaleZ);
        }
	}

    public void LoadMyLevel()
    {
        SceneManager.LoadSceneAsync("Credits");
    }
}
