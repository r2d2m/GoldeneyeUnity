using UnityEngine;
using UnityEngine.AI;

public class IASoldiers : MonoBehaviour {

    public float HP = 5f;

	public float angleVision = 45f;
    public float seenDistance = 15f;
    public float helpDistance = 15f;

    public AudioClip[] reactions;
    private AudioSource source;

    public Transform target;

    NavMeshAgent agent;
    IASoldiers _iaSoldiers;
    Random random = new Random();
    bool isHostile = false;
     
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        source = GetComponent<AudioSource>();
	}
	
	void Update () {
        if (!isHostile)
        {
			Vector3 heading = target.position - transform.position;
			float distance = heading.magnitude;
			Vector3 direction = heading / distance;

			if (distance < seenDistance)
            {
				Ray ray = new Ray(transform.position, target.position);
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
				Debug.DrawRay(transform.position, target.position, Color.green, 1);
            }
        }
        else
        {
            //agent.SetDestination(target.position);
        }
	}

    protected void RequestHelp()
    {
        isHostile = true;
    }

    public void HitAndDecreaseHP(float damage)
    {
        HP -= damage;
        source.PlayOneShot(reactions[Random.Range(0, reactions.Length)]);
        if (HP <= 0f)
        {
            Destroy(this.gameObject, 1);
        }
    }
}
