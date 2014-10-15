using UnityEngine;
using System.Collections;

public class VeriLammikonMuotoilija : MonoBehaviour {

	Animator anim;
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
	}
	

}
