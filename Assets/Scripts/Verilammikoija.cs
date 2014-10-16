using UnityEngine;
using System.Collections;

public class Verilammikoija : MonoBehaviour {

	public Transform Verilammikko;
	//public Transform Veriroiske;
	float dropTimer = 0f;
	bool lammikkoLuotu = false;
	bool luoBlammikko = false;
	bool luoClammikko = false;

	// Use this for initialization
	void Start () {

	

		var number = Random.Range(10f,40f);
		if (number > 20f) {
						luoBlammikko = true;
				}
		if (number > 30f) {
			luoClammikko = true;
				}
	}
	
	// Update is called once per frame
	void Update () {
				
			dropTimer += Time.deltaTime;

		if (dropTimer > 0.15f && luoBlammikko) {
			var xMod = Random.Range(-0.62f,0.02f);
			var yMod = Random.Range(-0.20f,0.20f);
			var veriTransform = Instantiate(Verilammikko) as Transform;
		
			veriTransform.position = transform.position;
			veriTransform.position = new Vector3(veriTransform.position.x+xMod, veriTransform.position.y+yMod, veriTransform.position.z);
			luoBlammikko=false;
				}

		if (dropTimer>0.25f && !lammikkoLuotu) {
			lammikkoLuotu=true;
			var xMod = Random.Range(-0.22f,0.22f);
			var yMod = Random.Range(-0.20f,0.20f);
			
			// Create a new shot
			var veriTransform = Instantiate(Verilammikko) as Transform;
			
			// Assign position

			veriTransform.position = transform.position;
			veriTransform.position = new Vector3(veriTransform.position.x+xMod, veriTransform.position.y+yMod, veriTransform.position.z);

			}

		if (dropTimer > 0.45f && luoClammikko) {
			var xMod = Random.Range(-0.02f,0.62f);
			var yMod = Random.Range(-0.20f,0.20f);
			var veriTransform = Instantiate(Verilammikko) as Transform;
			
			veriTransform.position = transform.position;
			veriTransform.position = new Vector3(veriTransform.position.x+xMod, veriTransform.position.y+yMod, veriTransform.position.z);
			luoClammikko=false;
		}


}
}
