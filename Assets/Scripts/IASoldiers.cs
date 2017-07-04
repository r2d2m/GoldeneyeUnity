using UnityEngine;
using UnityEngine.AI;

public class IASoldiers : MonoBehaviour {

    public float HP = 3f;

	public float angleVision = 45f;
    public float seenDistance = 15f;
    public float helpDistance = 15f;

	public bool activatesAlarm = false;

    public AudioClip[] reactions;
    private AudioSource source;
    private Animator animator;
    private Rigidbody rb;

    public Transform target;
	public GameObject alarm;

    NavMeshAgent agent;
    IASoldiers _iaSoldiers;

    bool isHostile = false;
     
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
	}
	
	void Update () {
        if (!isHostile)
        {
			if (Vector3.Distance(target.position, transform.position) < seenDistance)
            {
				Ray ray = new Ray(transform.position, target.position);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit)) {
					GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
					foreach (GameObject enemy in enemies) {
						if (Vector3.Distance (transform.position, enemy.transform.position) <= helpDistance) {
							_iaSoldiers = enemy.GetComponent<IASoldiers> ();
							_iaSoldiers.RequestHelp ();
						}
					}
				}
				Debug.DrawRay(transform.position, target.position, Color.green, 100);
            }
        }
        else
        {
			if (activatesAlarm && alarm != null) {
				agent.SetDestination(alarm.transform.position);
				if (Vector3.Distance(transform.position, alarm.transform.position) < 5f) {
					alarm.GetComponent<Alarm>().ActivateAlarm();
					activatesAlarm = false;
				}
			} else {
				agent.SetDestination(target.position);
			}
            animator.SetBool("Moving", rb.velocity.magnitude > 0f);
        }
	}

    public void RequestHelp()
    {
        isHostile = true;
    }

    public void HitAndDecreaseHP(float damage)
    {
        HP -= damage;
        isHostile = true;
        source.PlayOneShot(reactions[Random.Range(0, reactions.Length)]);
        if (HP <= 0f)
        {
            Destroy(this.gameObject, 1);
        }
    }
}
