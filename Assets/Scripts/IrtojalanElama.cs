using UnityEngine;
using System.Collections;

public class IrtojalanElama : MonoBehaviour {

	private float dropTimer = 0f;
	bool luotu =false;
	bool lammikoitu = false;
	bool facingRight = true;
	float edellinenX = 0f;
	
	float viiruTimer = 0f;
	
	
	float startY = 0f;
	public Transform Verilammikko;
	public Transform Veriviiru;
//	Animator anim;
	
	
	// Use this for initialization
	void Start () {
		
//		anim = GetComponent<Animator> ();
//		anim.SetTrigger ("Ilmassa");
		facingRight = true;
		gameObject.collider2D.enabled = false;
		gameObject.renderer.enabled = true;
		gameObject.rigidbody2D.isKinematic = false;
		var v1Mod = Random.Range (-125f, 125f);
		var v2Mod = Random.Range (-125f, 125f);
		var v3Mod = Random.Range (-125f, 125f);
		Vector2 v = new Vector2(v1Mod,v2Mod);
		Vector3 v3 = new Vector3(v1Mod,v2Mod, v3Mod);
		//Vector2 v = new Vector2(150f,260f);
		
		luotu = true;
		lammikoitu = false;
		
		gameObject.rigidbody2D.AddRelativeForce (v3);
		startY = transform.position.y;
		edellinenX = transform.position.x;
		dropTimer = 0f;
		var xMod = Random.Range (-0.5f, 0.5f);
		if (xMod > 0.0f) {
			Flip ();
		}
		gameObject.rigidbody2D.AddTorque (1000f);
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		
		
		
		dropTimer += Time.deltaTime;
		viiruTimer += Time.deltaTime;
		
		
		if (gameObject.name.Contains("(Clone)") && viiruTimer > 0.025f && dropTimer < 0.45f) {
			
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
		
		
		if (gameObject.name.Contains("(Clone)") && dropTimer > 0.45f && luotu && !lammikoitu) {
			//if (transform.position.y < startY-3.25f) {
			gameObject.rigidbody2D.velocity = Vector3.zero;
			gameObject.rigidbody2D.isKinematic = true;
			gameObject.collider2D.enabled = false;
			//gameObject.renderer.enabled = false;
		//	anim.ResetTrigger("Ilmassa");
		//	anim.SetTrigger ("Maassa");
			gameObject.renderer.sortingOrder = 12;
			gameObject.renderer.sortingLayerName = "Verilammikko";
			
			var veriTransform = Instantiate(Verilammikko) as Transform;
			
			var xMod = Random.Range (-0.5f, 0.5f);
			var yMod = Random.Range (-0.25f, 0.25f);
			
			veriTransform.position = transform.position;
			veriTransform.position = new Vector3 (veriTransform.position.x+xMod, veriTransform.position.y + yMod, veriTransform.position.z);
			
			veriTransform = Instantiate(Verilammikko) as Transform;
			
			xMod = Random.Range (-0.5f, 0.5f);
			yMod = Random.Range (-0.25f, 0.25f);
			
			veriTransform.position = transform.position;
			veriTransform.position = new Vector3 (veriTransform.position.x+xMod, veriTransform.position.y + yMod, veriTransform.position.z);
			
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