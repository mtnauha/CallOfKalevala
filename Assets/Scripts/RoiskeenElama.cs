using UnityEngine;
using System.Collections;

public class RoiskeenElama : MonoBehaviour {

	private float dropTimer = 0f;
	bool luotu =false;
	bool lammikoitu = false;
	bool facingRight = true;
	float edellinenX = 0f;

	float startY = 0f;
	public Transform Verilammikko;

	// Use this for initialization
	void Start () {

		facingRight = true;
		gameObject.collider2D.enabled = false;
		gameObject.renderer.enabled = true;
		gameObject.rigidbody2D.isKinematic = false;
		var v1Mod = Random.Range (-200f, 200f);
		var v2Mod = Random.Range (-200f, 200f);
		var v3Mod = Random.Range (-200f, 200f);
		Vector2 v = new Vector2(v1Mod,v2Mod);
		Vector3 v3 = new Vector3(v1Mod,v2Mod, v3Mod);
		//Vector2 v = new Vector2(150f,260f);
		
		luotu = true;
		lammikoitu = false;

		gameObject.rigidbody2D.AddRelativeForce (v3);
		startY = transform.position.y;
		edellinenX = transform.position.x;
		dropTimer = 0f;

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (transform.position.x > edellinenX) {
						Flip ();
				}


	
		
		dropTimer += Time.deltaTime;

		if (dropTimer > 0.75f && luotu && !lammikoitu) {
		//if (transform.position.y < startY-3.25f) {
			gameObject.rigidbody2D.velocity = Vector3.zero;
			gameObject.rigidbody2D.isKinematic = true;
			gameObject.collider2D.enabled = false;
			gameObject.renderer.enabled = false;

			var veriTransform = Instantiate(Verilammikko) as Transform;
			
			veriTransform.position = transform.position;
			lammikoitu = true;
		//	Destroy (gameObject);
		}

	
	}

	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

}
