using UnityEngine;
using System.Collections;

public class PomppivaPaa : MonoBehaviour {

	float dropTimer = 0f;
	float viiruTimer = 0f;
	float bloodCounter = 0f;
	Vector3 startVector;
	float startY;
	float klopsTimer = 0f;
	bool noSplatters;

	public Transform Verilammikko;
	public Transform Veriviiru;

	public AudioSource[] sounds;
	public AudioSource decapitate;
	public AudioSource headhit;
	public AudioSource squirt;


	// Use this for initialization
	void Start () {
		sounds = GetComponents<AudioSource>();
		decapitate = sounds[0];
		headhit = sounds[1];
		squirt = sounds[2];

		if (gameObject.name.Contains ("(Clone)")) {
						decapitate.Play ();
				}
		dropTimer = 0f;
		klopsTimer = 0f;
		gameObject.collider2D.enabled = true;
		gameObject.collider2D.isTrigger = true;
		startVector = transform.position;
		startY = transform.position.y;
		noSplatters = true;
		//var v1Mod = Random.Range (100f, 200f);
		//var v2Mod = Random.Range (160f, 360f);

		var v1Mod = Random.Range (-200f, 200f);
		var v2Mod = Random.Range (-360f, 360f);

		Vector2 v = new Vector2(v1Mod,v2Mod);
		//Vector2 v = new Vector2(150f,260f);


		gameObject.rigidbody2D.AddRelativeForce (v);
		//gameObject.rigidbody2D.rotation (q);

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (gameObject.name.Contains("(Clone)")) {

		headhit.pitch = Random.Range(0.8f, 1.2f);// headhit.volume = Random.Range(0.75f, 1.0f);
		squirt.pitch = Random.Range(0.9f, 1.1f); //squirt.volume = Random.Range(0.75f, 1.0f);

		viiruTimer += Time.deltaTime;
		dropTimer += Time.deltaTime;
		klopsTimer += Time.deltaTime;
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

		if (gameObject.name.Contains("(Clone)") && bloodCounter < 10.0f && !noSplatters && viiruTimer > 0.05f && dropTimer > 0.25f && dropTimer < 12.0f) {
			
						//if (gameObject.transform.localEulerAngles.z > 285.0f || gameObject.transform.localEulerAngles.z < 75.0f) {
								//if (gameObject.transform.localEulerAngles.z >= -0.2f && gameObject.transform.localEulerAngles.z <= 0.2f) {
								var xMod = Random.Range (-0.5f, 0.5f);
								var yMod = Random.Range (-0.25f, 0.25f);
								var viiru = Instantiate (Veriviiru) as Transform;
				
				
								// Assign position
								viiru.position = transform.position;
								viiru.position = new Vector3 (viiru.position.x+xMod, viiru.position.y + yMod, viiru.position.z);
								//lammikko.position.y = lammikko.position.y + 3.0f;
								viiruTimer = 0.0f;
						//}
				}
		if (dropTimer>12f && gameObject.name.Contains("(Clone)")) {
		    gameObject.rigidbody2D.velocity = Vector3.zero;
		    gameObject.rigidbody2D.isKinematic = true;
		    gameObject.collider2D.enabled = false;
		}
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

	void OnCollisionHit2D(Collider2D col) {

		if (bloodCounter < 10.0f && !noSplatters && viiruTimer>0.05f) {

			if (gameObject.transform.localEulerAngles.z > 285.0f || gameObject.transform.localEulerAngles.z < 75.0f) {
				//if (gameObject.transform.localEulerAngles.z >= -0.2f && gameObject.transform.localEulerAngles.z <= 0.2f) {
				var yMod = Random.Range(-0.1f,0.1f);
				var viiru = Instantiate (Veriviiru) as Transform;
				squirt.Play ();
				
				// Assign position
				viiru.position = transform.position;
				viiru.position = new Vector3(viiru.position.x, viiru.position.y+yMod, viiru.position.z);
				//lammikko.position.y = lammikko.position.y + 3.0f;
				viiruTimer=0.0f;
			}
		}

		}

	void OnCollisionEnter2D(Collision2D col) {
		if (klopsTimer > 0.25f) {
						headhit.Play ();
						klopsTimer=0f;
						headhit.volume = headhit.volume * 0.70f;
				}
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
								squirt.volume = squirt.volume * 0.80f;
								squirt.Play ();
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
