using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	public GameObject player;
	public GameObject enemy;
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
		
			Instantiate (enemy, spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation);
			Debug.Log ("Instantiated an enemy at", spawnPoints [spawnPointIndex]);
		}
	}

}