using UnityEngine;
using UnityEngine.AI;

public class IASoldiers : MonoBehaviour {

    public float HP = 3f;

	public float angleVision = 45f;
    public float seenDistance = 15f;
    public float helpDistance = 50f;
    public float shootDistance = 15f;
    public int consecutiveShoots = 1000;

	public bool activatesAlarm = false;

    public AudioClip[] reactions;
    private AudioSource source;
    private Animator animator;
    private Rigidbody rb;

    public Transform target;
	public GameObject alarm;

    NavMeshAgent agent;
    IASoldiers _iaSoldiers;

    private bool canShoot = true;
    private bool isHostile = false;
    private int currentShoots = 0;
     
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
	}
	
	void Update () {
        float distance = Vector3.Distance(target.position, transform.position);
        if (!isHostile)
        {
			if (distance < seenDistance)
            {
				Ray ray = new Ray(transform.position, target.position);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit)) {
					GameObject[] allies = GameObject.FindGameObjectsWithTag("Enemy");
					foreach (GameObject ally in allies) {
						if (Vector3.Distance (transform.position, ally.transform.position) <= helpDistance) {
							_iaSoldiers = ally.GetComponent<IASoldiers>();
							_iaSoldiers.RequestHelp();
						}
					}
				}
            }
        }
        else
        {
			if (activatesAlarm && alarm != null && !alarm.GetComponent<AudioSource>().isPlaying) {
				agent.SetDestination(alarm.transform.position);
				if (Vector3.Distance(transform.position, alarm.transform.position) <= 2f) {
					alarm.GetComponent<Alarm>().ActivateAlarm();
					activatesAlarm = false;
				}
			} else if (distance >= shootDistance) {
				agent.SetDestination(target.position);
                animator.SetBool("Shooting", false);
            }
            else
            {
                animator.SetInteger("Bullets", consecutiveShoots - currentShoots);
                if (currentShoots == consecutiveShoots)
                {
                    animator.SetBool("Shooting", false);
                }
                else if (canShoot)
                {
                    ++currentShoots;
                    canShoot = false;
                    animator.SetBool("Shooting", true);
                    transform.LookAt(target.transform, Vector3.up);
                }
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

    public void EventReloadBullets()
    {
        currentShoots = 0;
    }

    public void EventCanShootAgain()
    {
        canShoot = true;
    }
}
