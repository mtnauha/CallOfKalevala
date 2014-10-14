using UnityEngine;
using System.Collections;

public class Verilammikoija : MonoBehaviour {

	public Transform Verilammikko;
	float dropTimer = 0f;
	bool lammikkoLuotu = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
				
			dropTimer += Time.deltaTime;


		if (dropTimer>0.45f && !lammikkoLuotu) {
			lammikkoLuotu=true;
			var xMod = Random.Range(-0.22f,0.22f);
			var yMod = Random.Range(-0.20f,0.20f);
			
			// Create a new shot
			var veriTransform = Instantiate(Verilammikko) as Transform;
			
			// Assign position

			veriTransform.position = transform.position;
			veriTransform.position = new Vector3(veriTransform.position.x+xMod, veriTransform.position.y+yMod, veriTransform.position.z);

	}


}
}
