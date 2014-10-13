using UnityEngine;

public class MestausScript : MonoBehaviour
{

	public Transform Irtopaa;
	
	/// <summary>
	/// Cooldown in seconds between two shots
	/// </summary>
	public float shootingRate = 0.25f;
	private float headCooldown;

	//GameObject Irtopaa;
	
	void Start()
	{
		headCooldown = 0f;
		//gameObject.collider.enabled = false;
	}
	
	void Update()
	{
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
		
		
		headCooldown = shootingRate;
		
		// Create a new shot
		var headTransform = Instantiate(Irtopaa) as Transform;
		
		// Assign position
		headTransform.position = transform.position;
	
		
		
	}


	
}

