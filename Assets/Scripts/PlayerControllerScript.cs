using UnityEngine;
using System.Collections;

public class PlayerControllerScript : MonoBehaviour {

	public float maxSpeed = 10f;
	bool facingRight = true;
	Animator anim;  
	public bool isMoving = false;
	public float plrHealth = 1;
	public bool paaPoikki = false;

//	public Rigidbody2D PaaPrefab;
	public float speed = 10f;	

	void Awake ()
	{
		anim = GetComponent <Animator> ();
	}

	void Start () {
	

	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		rigidbody2D.velocity = new Vector2 (moveHorizontal * maxSpeed, moveVertical * maxSpeed);

		if (moveHorizontal > 0 && !facingRight)
			Flip ();
		else if (moveHorizontal < 0 && facingRight)
			Flip ();
			

		if (moveHorizontal != 0)
			anim.SetBool ("isMoving", true);
				else
			anim.SetBool ("isMoving", false);

		if (Input.GetKeyDown ("space")) 
			anim.SetBool ("isGoreLeg", true);

		if (Input.GetKeyDown ("enter")) {
						anim.SetBool ("isGoreHead", true);

				HeadScript head = GetComponent<HeadScript>();

				head.Attack();

			}

				
			//
					//	Rigidbody2D bulletInstance = Instantiate (PaaPrefab, transform.position, Quaternion.Euler (new Vector3 (0, 0, 0))) as Rigidbody2D;
				//		bulletInstance.velocity = new Vector2 (speed, 0);
				
	}

	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		//isMoving = true;
	}
}
