using UnityEngine;
using System.Collections;

public class VeriLentaa : MonoBehaviour {

	public Transform VerisuihkuPerus;
	public Transform Veriroiske;


	// Use this for initialization
	void Start () {
	
	}
	


	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void suihkauta()
	{
		
		//var headTransform = Instantiate(Head, Vector3.zero, Quaternion.identity) as GameObject;
		
		
		// Create a new shot
		var suihku = Instantiate(VerisuihkuPerus) as Transform;
		var xMod = Random.Range(-0.35f,0.01f);
		var yMod = Random.Range(-0.51f,0.01f);




		// Assign position
		suihku.position = transform.position;

				
		// Assign position
		suihku.position = new Vector3(suihku.position.x+xMod, suihku.position.y+yMod, suihku.position.z);


		var roiske = Instantiate(Veriroiske) as Transform;
		roiske.position = transform.position;
		roiske.position = new Vector3(suihku.position.x+xMod, suihku.position.y+yMod, suihku.position.z);

		 xMod = Random.Range(-0.35f,0.01f);
		 yMod = Random.Range(-0.51f,0.01f);
		
		 roiske = Instantiate(Veriroiske) as Transform;
		roiske.position = transform.position;
		roiske.position = new Vector3(suihku.position.x+xMod, suihku.position.y+yMod, suihku.position.z);

		 xMod = Random.Range(-0.35f,0.01f);
		 yMod = Random.Range(-0.51f,0.01f);
				
		 roiske = Instantiate(Veriroiske) as Transform;
		roiske.position = transform.position;
		roiske.position = new Vector3(suihku.position.x+xMod, suihku.position.y+yMod, suihku.position.z);

		xMod = Random.Range(-0.35f,0.01f);
		yMod = Random.Range(-0.51f,0.01f);
		
		roiske = Instantiate(Veriroiske) as Transform;
		roiske.position = transform.position;
		roiske.position = new Vector3(suihku.position.x+xMod, suihku.position.y+yMod, suihku.position.z);

		xMod = Random.Range(-0.35f,0.01f);
		yMod = Random.Range(-0.51f,0.01f);
		
		roiske = Instantiate(Veriroiske) as Transform;
		roiske.position = transform.position;
		roiske.position = new Vector3(suihku.position.x+xMod, suihku.position.y+yMod, suihku.position.z);
}
}