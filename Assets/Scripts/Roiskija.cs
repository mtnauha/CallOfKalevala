using UnityEngine;
using System.Collections;

public class Roiskija : MonoBehaviour {

	public Transform Veriroiske;

	bool lammikkoLuotu = false;
	//var roiskeTransform;

	// Use this for initialization



	public void roiskaise () {

		var roiskeTransform = Instantiate(Veriroiske) as Transform;
		
		roiskeTransform.position = transform.position;
		//roiskeTransform.SendMessage ("resetTimer()");
		
		//roiskeTransform.SendMessage ("resetTimer");
		//roiskeTransform.rigidbody2D.
	
	}
	
	// Update is called once per frame
//	void FixedUpdate () {
	
		//dropTimer += Time.deltaTime;
		//if (dropTimer > 0.5f) {
		//	roiskeTransform.rigidbody2D.velocity = Vector3.zero;
		//	roiskeTransform.rigidbody2D.isKinematic = true;
		//	roiskeTransform.collider2D.enabled = false;
		//		}

//	}

	//void OnTriggerEnter2D(Co col) {
		//if (col.gameObject.tag == "maa" && dropTimer>0.5f) {
		//Debug.Log("Osui maahan, trigger on false");
		//gameObject.collider2D.isTrigger=false;
	////	if (col.gameObject.tag == "Verimaa") {
		//	Debug.Log ("Osui verimaahan");
		////	gameObject.rigidbody2D.isKinematic = true;
			//gameObject.collider2D.enabled = false;

		//}

	}

