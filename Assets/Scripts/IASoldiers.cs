using UnityEngine;
using UnityEngine.AI;

public class IASoldiers : MonoBehaviour {

    public float HP = 5f;

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
            if (Vector3.Distance(transform.position, target.position) < 15f)
            {
                isHostile = true;
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (GameObject enemy in enemies)
                {
                    if (Vector3.Distance(transform.position, enemy.transform.position) <= helpDistance)
                    {
                        _iaSoldiers = enemy.GetComponent<IASoldiers>();
                        _iaSoldiers.RequestHelp();
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
