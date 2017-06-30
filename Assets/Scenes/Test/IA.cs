using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IA : MonoBehaviour {

    NavMeshAgent agent;
    public Transform target;
    bool following = false;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        float distance = Vector3.Distance(transform.position, target.position);
        if (!following && distance <= 20f)
        {
            following = true;
        }
        else if (following && distance > 40f)
        {
            following = false;
        }

        if (following)
        {
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //RaycastHit hit;
            //if (Physics.Raycast(ray, out hit))
            //{
            //    agent.SetDestination(hit.point);
            //}
            agent.SetDestination(target.position);
        }
	}
}
