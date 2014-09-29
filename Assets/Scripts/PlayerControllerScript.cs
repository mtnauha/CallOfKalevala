using UnityEngine;
using System.Collections;

public class PlayerControllerScript : MonoBehaviour {

	public float maxSpeed = 10f;
	bool facingRight = true;

	// Use this for initialization
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
	}

	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
