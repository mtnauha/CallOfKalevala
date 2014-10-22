using UnityEngine;
using System.Collections;

public class VeriLammikonMuotoilija : MonoBehaviour {

	Animator anim;
	private float lammikkoTimer=0.0f;
	private bool ekaTummennus = false;
	private bool tokaTummennus = false;
	// Use this for initialization

	void Start () {
		anim = GetComponent<Animator> ();
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
