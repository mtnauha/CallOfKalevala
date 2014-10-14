using UnityEngine;
using System.Collections;

public class PomppivaPaa : MonoBehaviour {

	float dropTimer = 0f;
	float bloodCounter = 0f;
	Vector3 startVector;
	float startY;
	bool noSplatters;

	public Transform Verilammikko;

	// Use this for initialization
	void Start () {
		gameObject.collider2D.enabled = true;
		gameObject.collider2D.isTrigger = true;
		startVector = transform.position;
		startY = transform.position.y;
		noSplatters = true;
		Vector2 v = new Vector2(150F,260);


		gameObject.rigidbody2D.AddRelativeForce (v);
		//gameObject.rigidbody2D.rotation (q);

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		dropTimer += Time.deltaTime;
		//angle = gameObject.transform.localEulerAngles.z;

		//if (dropTimer > 3.05f) {
		//	gameObject.collider2D.enabled = true;
		//		}

		//if (dropTimer > 0.85f) {
		//				enableCollider ();
		//		}
		if (transform.position.y > startY-1.25f && dropTimer<0.5f) {
						noSplatters = true;
					gameObject.collider2D.enabled = false;
				} else {
			noSplatters = false;
			gameObject.collider2D.enabled = true;
				}
	
	}

	void OnTriggerEnter2D(Collider2D col) {
			//if (col.gameObject.tag == "maa" && dropTimer>0.5f) {
			//Debug.Log("Osui maahan, trigger on false");
			//gameObject.collider2D.isTrigger=false;

			if (col.gameObject.tag == "Vapaa liikkuvuus") {
						Debug.Log ("Osui triggeriin, trigger on false");
						gameObject.collider2D.isTrigger = true;
				}
					
			// Create a new shot
			

				
		}
	void OnTriggerExit2D(Collider2D col) {
			gameObject.collider2D.isTrigger = false;
		}


	void OnCollisionEnter2D(Collision2D col) {
		//dropTimer = 0f;
		//Debug.Log("On collision ajettu");
		//if (col.gameObject.tag == "maa") {
			//Irtopaa.SendMessage("enableCollider");
			//Debug.Log("Osui maahan");
			//gameObject.collider2D.enabled = true;
		//}
		Debug.Log(gameObject.transform.localEulerAngles.z);
		//if (this.dropTimer <= 6.0f) {
	    if (bloodCounter < 10.0f && !noSplatters) {

			if (gameObject.transform.localEulerAngles.z > 285.0f || gameObject.transform.localEulerAngles.z < 75.0f) {
						//if (gameObject.transform.localEulerAngles.z >= -0.2f && gameObject.transform.localEulerAngles.z <= 0.2f) {
								Debug.Log ("angle: " + gameObject.transform.localEulerAngles.z);
								var lammikko = Instantiate (Verilammikko) as Transform;
								bloodCounter += 1.0f;
	
								// Assign position
								lammikko.position = transform.position;
								//lammikko.position.y = lammikko.position.y + 3.0f;
						}
				}
		//		}
		//if (col.gameObject.tag == "ylaraja") {
			//Irtopaa.SendMessage("enableCollider");
		//	Debug.Log("Osui ylärajaan");
		//	gameObject.collider2D.isTrigger=true;
		//}
	
	}


	void enableCollider() {
		gameObject.collider2D.enabled = true;
	}
}
