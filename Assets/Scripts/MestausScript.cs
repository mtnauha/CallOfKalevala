using UnityEngine;

public class MestausScript : MonoBehaviour
{

	public Transform Irtopaa;
	public Transform IrtopaaOrc;
	public Transform IrtokasiOrc;
	public Transform VerisuihkuPerus;
	public Transform Veriroiske;
	float bloodTimer = 0f;
	bool lisaSuihkausA = false;
	bool lisaSuihkausB = false;
	bool lisaSuihkausC = false;
	bool lisaSuihkausD = false;
	bool lisaSuihkausE = false;
	bool sankarinPaa = false;
	bool orcinKasi = false;
	bool orcinPaa = false;
	
	/// <summary>
	/// Cooldown in seconds between two shots
	/// </summary>
	public float shootingRate = 0.25f;
	private float headCooldown;

	//GameObject Irtopaa;
	
	void Start()
	{
		headCooldown = 0f;
		float bloodTimer = 0f;
		lisaSuihkausA = false;
		lisaSuihkausB = false;
		lisaSuihkausC = false;
		lisaSuihkausD = false;
		lisaSuihkausE = false;
		sankarinPaa = false;
		orcinKasi = false;
		orcinPaa = false;
		//gameObject.collider.enabled = false;
	}
	
	void fixedUpdate()
	{
		bloodTimer += Time.deltaTime;
		if (gameObject.name.Contains ("(Clone)") && bloodTimer > 0.45f && !lisaSuihkausA && (orcinKasi || sankarinPaa)) {
			suihkauta ();
			lisaSuihkausA=true;
				}
		if (gameObject.name.Contains ("(Clone)") && bloodTimer > 1.25f && !lisaSuihkausB && orcinKasi) {
			suihkauta ();
			lisaSuihkausB=true;
		}
		if (gameObject.name.Contains ("(Clone)") && bloodTimer > 1.85f && !lisaSuihkausC && (orcinKasi || sankarinPaa)) {
			suihkauta ();
			suihkauta ();
			lisaSuihkausC=true;
		}
		if (gameObject.name.Contains ("(Clone)") && bloodTimer > 2.8f && !lisaSuihkausD && orcinKasi) {
			suihkauta ();
			lisaSuihkausD=true;
		}

		if (gameObject.name.Contains ("(Clone)") && bloodTimer > 3.8f && !lisaSuihkausE && orcinKasi) {
			suihkauta ();
			lisaSuihkausE=true;
		}


		if (headCooldown > 0)
		{
			headCooldown -= Time.deltaTime;
		}



	}
	
	//--------------------------------
	// 3 - Shooting from another script
	//--------------------------------
	
	/// <summary>
	/// Create a new projectile if possible
	/// </summary>
	public void katkaise()
	{
		
		//var headTransform = Instantiate(Head, Vector3.zero, Quaternion.identity) as GameObject;
		
		sankarinPaa = true;

		headCooldown = shootingRate;
		
		// Create a new shot
		var headTransform = Instantiate(Irtopaa) as Transform;
		
		// Assign position
		headTransform.position = transform.position;
		headTransform.renderer.sortingLayerName = "Verilammikko";
		suihkauta ();
		
	}

	public void katkaiseOrc()
	{
		
		//var headTransform = Instantiate(Head, Vector3.zero, Quaternion.identity) as GameObject;

		orcinPaa = true;
		
		headCooldown = shootingRate;
		
		// Create a new shot
		var headTransform = Instantiate(IrtopaaOrc) as Transform;
		
		// Assign position
		headTransform.position = transform.position;
		headTransform.renderer.sortingLayerName = "Verilammikko";
		suihkauta ();
		
	}

	public void katkaiseOrcKasi()
	{
		
		//var headTransform = Instantiate(Head, Vector3.zero, Quaternion.identity) as GameObject;

		orcinKasi = true;
		
		headCooldown = shootingRate;
		
		// Create a new shot
		var kasiTransform = Instantiate(IrtokasiOrc) as Transform;
		
		// Assign position
		kasiTransform.position = transform.position;
		kasiTransform.renderer.sortingOrder = 30;
		suihkauta ();
		
	}


	public void suihkauta()
	{
		
		//var headTransform = Instantiate(Head, Vector3.zero, Quaternion.identity) as GameObject;
		
		
		// Create a new shot
		var suihku = Instantiate(VerisuihkuPerus) as Transform;
		var xMod = Random.Range(-0.01f,0.01f);
		var yMod = Random.Range(-0.01f,0.01f);
		var montako = Random.Range(-0.01f,5.0f);
		
		
		
		// Assign position
		suihku.position = transform.position;
		
		
		// Assign position
		suihku.position = new Vector3(suihku.position.x+xMod, suihku.position.y+yMod, suihku.position.z);
		

		var roiske = Instantiate(Veriroiske) as Transform;
		roiske.position = transform.position;
		roiske.position = new Vector3(suihku.position.x+xMod, suihku.position.y+yMod, suihku.position.z);
		roiske.transform.localScale -= new Vector3(0.5f,0.5f,0.5f);


		if (montako > 3.0f) {
						roiske = Instantiate (Veriroiske) as Transform;
						roiske.position = transform.position;
						roiske.position = new Vector3 (suihku.position.x + xMod, suihku.position.y + yMod, suihku.position.z);
						roiske.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
				}


		//WaitForSeconds (0.3f);
		if (montako > 4.0f) {
						roiske = Instantiate (Veriroiske) as Transform;
						roiske.position = transform.position;
						roiske.position = new Vector3 (suihku.position.x + xMod, suihku.position.y + yMod, suihku.position.z);
						roiske.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
				}

		roiske = Instantiate(Veriroiske) as Transform;
		roiske.position = transform.position;
		roiske.position = new Vector3(suihku.position.x+xMod, suihku.position.y+yMod, suihku.position.z);
		roiske.transform.localScale -= new Vector3(0.5f,0.5f,0.5f);

		//WaitForSeconds (0.3f);

		
		roiske = Instantiate(Veriroiske) as Transform;
		roiske.position = transform.position;
		roiske.position = new Vector3(suihku.position.x+xMod, suihku.position.y+yMod, suihku.position.z);
		roiske.transform.localScale -= new Vector3(0.5f,0.5f,0.5f);
	}


	
}

