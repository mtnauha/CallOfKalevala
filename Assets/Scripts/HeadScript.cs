using UnityEngine;

/// <summary>
/// Launch projectile
/// </summary>
public class HeadScript : MonoBehaviour
{
	//--------------------------------
	// 1 - Designer variables
	//--------------------------------
	
	/// <summary>
	/// Projectile prefab for shooting
	/// </summary>
	public Transform Head2;
	
	/// <summary>
	/// Cooldown in seconds between two shots
	/// </summary>
	public float shootingRate = 0.25f;
	
	//--------------------------------
	// 2 - Cooldown
	//--------------------------------
	
	private float shootCooldown;
	
	void Start()
	{
		shootCooldown = 0f;
	}
	
	void Update()
	{
		if (shootCooldown > 0)
		{
			shootCooldown -= Time.deltaTime;
		}
	}
	
	//--------------------------------
	// 3 - Shooting from another script
	//--------------------------------
	
	/// <summary>
	/// Create a new projectile if possible
	/// </summary>
	public void Attack()
	{

		//var headTransform = Instantiate(Head, Vector3.zero, Quaternion.identity) as GameObject;


			shootCooldown = shootingRate;
			
			// Create a new shot
			var headTransform = Instantiate(Head2) as Transform;

			// Assign position
			headTransform.position = transform.position;
		//headTransform.position.y = transform.position.y(15);

//		Rigidbody2D headTransform = Instantiate(Head, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
//		headTransform.velocity.AddForce(100,100f);
			
			// The is enemy property
			//ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
			//if (shot != null)
			//{
				//shot.isEnemyShot = isEnemy;
			//}
		//headTransform.gameObject.GetComponent<MoveScript>();
			// Make the weapon shot always towards it
			
		
	}
	
	/// <summary>
	/// Is the weapon ready to create a new projectile?
	/// </summary>

}
