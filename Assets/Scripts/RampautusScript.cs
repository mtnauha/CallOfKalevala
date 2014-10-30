using UnityEngine;
using System.Collections;

public class RampautusScript : MonoBehaviour {

	public Transform VerisuihkuJalka;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void katkaise()
	{
		
		//var headTransform = Instantiate(Head, Vector3.zero, Quaternion.identity) as GameObject;
		
				
		// Create a new shot
		var legTransform = Instantiate(VerisuihkuJalka) as Transform;
		
		// Assign position
		legTransform.position = transform.position;
		
		
		
	}
}
