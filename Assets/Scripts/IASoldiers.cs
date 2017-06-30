using UnityEngine;
using UnityEngine.AI;

public class IASoldiers : MonoBehaviour {

    public float HP = 5f;

	public float angleVision = 45f;
    public float seenDistance = 15f;
    public float helpDistance = 15f;

    public Transform target;

    NavMeshAgent agent;
    IASoldiers _iaSoldiers;
    bool isHostile = false;
     
	void Start () {
        agent = GetComponent<NavMeshAgent>();
	}
	
	void Update () {
        if (!isHostile)
        {
			Vector3 heading = target.position - transform.position;
			float distance = heading.magnitude;
			Vector3 direction = heading / distance;

			if (distance < 15f)
            {
				Ray ray = new Ray(transform.position, direction);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.name == "007") {


					isHostile = true;
					GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
					foreach (GameObject enemy in enemies) {
						if (Vector3.Distance (transform.position, enemy.transform.position) <= helpDistance) {
							_iaSoldiers = enemy.GetComponent<IASoldiers> ();
							_iaSoldiers.RequestHelp ();
						}
					}
				}
            }
        }
        else
        {
            agent.SetDestination(target.position);
        }
	}

    protected void RequestHelp()
    {
        isHostile = true;
    }

    public void HitAndDecreaseHP(float damage)
    {
        HP -= damage;
        if (HP <= 0f)
        {
            Destroy(this.gameObject);
        }
    }
}
