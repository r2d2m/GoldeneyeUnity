using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour
{
    public float deltaMovement = 2f;
	public float speed = 10.0f;
	public float gravity = 10.0f;
	public float maxVelocityChange = 10.0f;
	public bool canJump = true;
	public float jumpHeight = 1.0f;
	private bool grounded = false;
	Rigidbody rb;

    void Start ()
    {

    }

    void Update ()
    {
        Movement();
    }

    void OnCollisionStay (Collision collision)
    {
        if (collision.gameObject.name == "Plane_base")
            if (Input.GetKeyDown(KeyCode.Space))
                GetComponent<Rigidbody>().AddForce(Vector3.up * 50f, ForceMode.Impulse);
    }
    
    private void Movement ()
    {
        if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.forward * deltaMovement * Time.deltaTime);
        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.back * deltaMovement * Time.deltaTime);
        if (Input.GetKey(KeyCode.A))
            transform.Translate(Vector3.left * deltaMovement * Time.deltaTime);
        if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * deltaMovement * Time.deltaTime);
    }

/*    private void RecollectAmmo ()
    {
        _shoot.AddAmmo();
        GameObject usedWeapon = GameObject.Find("Weapon_pp7");
        Destroy(usedWeapon);
    }*/

	void Awake()
	{
		rb = gameObject.GetComponent<Rigidbody>();
		rb.freezeRotation = true;
		rb.useGravity = false;
	}

	void FixedUpdate()
	{
		if (grounded)
		{
			Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			targetVelocity = transform.TransformDirection(targetVelocity);
			targetVelocity *= speed;

			// Apply a force that attempts to reach our target velocity
			Vector3 velocity = rb.velocity;
			Vector3 velocityChange = (targetVelocity - velocity);
			velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
			velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
			velocityChange.y = 0;
			rb.AddForce(velocityChange, ForceMode.VelocityChange);

			// Jump
			if (canJump && Input.GetButton("Jump"))
			{
				rb.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
			}
		}

		rb.AddForce(new Vector3(0, -gravity * rb.mass, 0));

		grounded = false;
	}

	void OnCollisionStay()
	{
		grounded = true;
	}

	float CalculateJumpVerticalSpeed()
	{
		return Mathf.Sqrt(2 * jumpHeight * gravity);
	}

	public void ModifySpeed(float difference)
	{
		speed += difference;
	}
}