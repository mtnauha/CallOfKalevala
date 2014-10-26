using UnityEngine;
using System.Collections;

public class VeriLammikonMuotoilija : MonoBehaviour {

	Animator anim;
	private float lammikkoTimer=0.0f;
	private bool ekaTummennus = false;
	private bool tokaTummennus = false;
	public AudioSource[] splat;
	// Use this for initialization

	void Start () {
		anim = GetComponent<Animator> ();
		splat = GetComponents<AudioSource>();
		splat[0].pitch = Random.Range(0.75f, 1.25f); splat[0].volume = Random.Range(0.25f, 0.65f);
		if (gameObject.name.Contains ("(Clone)")) {
						splat [0].Play ();
				}


		var number = Random.Range(10f,40f);
		if (number < 20f) {
			anim.SetBool ("lammikko1", true);}
		else if (number < 30f) {
			anim.SetBool ("lammikko2", true);}
		else {
			anim.SetBool ("lammikko3", true);}
		gameObject.renderer.sortingLayerName = "BloodInBack";
		gameObject.renderer.sortingLayerName = "Verilammikko";
		lammikkoTimer=0.0f;
		 ekaTummennus = false;
		 tokaTummennus = false;


	//	if (transform.localPosition.y > 10f) {
	//		transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);
	//	} else if (transform.localPosition.y > 8f) {
	//		transform.localScale = new Vector3 (0.75f, 0.75f, 0.75f);
	//	}
	//	else if (transform.localPosition.y < 6f) {
	//		transform.localScale = new Vector3 (1.4f, 1.4f, 1.4f);
	//	} else if (transform.localPosition.y < 4f) {
	//		transform.localScale = new Vector3 (1.8f, 1.8f, .18f);
	//	}

	}

	//void FixedUpdate() {
	//	lammikkoTimer += Time.deltaTime;

	//	if (lammikkoTimer > 20.0f && !tokaTummennus) {
	//		Color c = gameObject.renderer.material.color;
	//		c.a = 0.75f; c.r = 0.75f;
	//		gameObject.renderer.material.color = c;
	//		tokaTummennus = true;

	//			}
	//	if (lammikkoTimer > 10.0f && !ekaTummennus) {
	//		Color c = gameObject.renderer.material.color;
	//		c.a = 0.33f; c.r = 0.33f;
	//		gameObject.renderer.material.color = c;
	//		ekaTummennus = true;
			
	//	}

	

}
