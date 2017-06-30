using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public Transform target;
    Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = target.position - transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(target.position.x, 0, target.position.z) - offset;
	}
}
