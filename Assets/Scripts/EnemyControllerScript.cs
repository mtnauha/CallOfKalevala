using UnityEngine;
using System.Collections;

public class EnemyControllerScript : MonoBehaviour {

	public int health;

	// Use this for initialization
	void Start () {
		health = 100;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ApplyDamage(int damage) {
		health -= damage;
	}
}
