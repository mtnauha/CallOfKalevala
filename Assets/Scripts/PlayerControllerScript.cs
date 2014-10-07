using UnityEngine;
using System.Collections;

public class PlayerControllerScript : MonoBehaviour {

	public float maxSpeed = 10f;
	private bool facingRight = true;
	private bool attack = false;

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}

	void Update () {
		if (Input.GetKeyDown ("space")) {
			anim.SetTrigger ("Attack");
			attack = true;
		}

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Base.Attack")) {
			anim.SetFloat ("Speed", Mathf.Abs (moveHorizontal + moveVertical));
			rigidbody2D.velocity = new Vector2 (moveHorizontal * maxSpeed, moveVertical * maxSpeed);
		} else {
			anim.SetFloat ("Speed", 0);
			rigidbody2D.velocity = Vector2.zero;
		}

		if (moveHorizontal > 0 && !facingRight)
			Flip ();
		else if (moveHorizontal < 0 && facingRight)
			Flip ();
	}

	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void OnTriggerStay2D(Collider2D col) {
		if (col.gameObject.tag == "Enemy" && attack) {
			col.gameObject.SendMessage("ApplyDamage", 10);
			attack = false;
			Debug.Log ("Enemy collision!");
		}
	}

}
