using UnityEngine;
using System.Collections;

public class PomppivaPaa : MonoBehaviour {

	float dropTimer = 0f;

	// Use this for initialization
	void Start () {
		gameObject.collider2D.enabled = true;
		gameObject.collider2D.isTrigger = true;
		Vector2 v = new Vector2(150F,260);

		gameObject.rigidbody2D.AddRelativeForce (v);
		//gameObject.rigidbody2D.rotation (q);

	}
	
	// Update is called once per frame
	void Update () {

		dropTimer += Time.deltaTime;


		//if (dropTimer > 3.05f) {
		//	gameObject.collider2D.enabled = true;
		//		}

		//if (dropTimer > 0.85f) {
		//				enableCollider ();
		//		}
	
	}

	void OnTriggerEnter2D(Collider2D col) {
			if (col.gameObject.tag == "maa") {
			Debug.Log("Osui maahan, trigger on false");
			gameObject.collider2D.isTrigger=false;
				}
		}

	void OnCollisionEnter2D(Collision2D col) {
		//dropTimer = 0f;
		//Debug.Log("On collision ajettu");
		//if (col.gameObject.tag == "maa") {
			//Irtopaa.SendMessage("enableCollider");
			//Debug.Log("Osui maahan");
			//gameObject.collider2D.enabled = true;
		//}
		if (col.gameObject.tag == "ylaraja") {
			//Irtopaa.SendMessage("enableCollider");
			Debug.Log("Osui ylärajaan");
			gameObject.collider2D.isTrigger=true;
		}
	
	}


	void enableCollider() {
		gameObject.collider2D.enabled = true;
	}
}
