using UnityEngine;
using System.Collections;

public class VeriLentaa : MonoBehaviour {

	public Transform VerisuihkuPerus;


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
		
		// Assign position
		suihku.position = transform.position;
		

}
}