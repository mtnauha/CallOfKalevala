using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	public GameObject player;
	public Transform Enemy;
	public float spawnTime = 3f;
	public Transform[] spawnPoints;
	
	
	void Start ()
	{
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}
	
	
	void Spawn ()
	{
		if (player.GetComponent<PlayerControllerScript> () != null) {
		
			if (player.GetComponent<PlayerControllerScript>().health <= 0f) {
				return;
			}
		
			int spawnPointIndex = Random.Range (0, spawnPoints.Length);
		
			Instantiate (Enemy, spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation);
			Debug.Log ("Instantiated an enemy at", spawnPoints [spawnPointIndex]);

		//	var newenemy = Instantiate (Enemy) as Transform;
		//	var xMod = Random.Range (-5.5f, 5.5f);
		//	newenemy.position = transform.position;
		//	newenemy.position = new Vector3 (0f+xMod, 0f, 0f);

		}
	}

}