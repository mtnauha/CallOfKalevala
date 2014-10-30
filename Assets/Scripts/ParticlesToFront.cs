using UnityEngine;
using System.Collections;

public class ParticlesToFront : MonoBehaviour {

	float kulma = 0.0f;

	// Use this for initialization
	void Start () {

		//		particleSystem.renderer.sortingLayerName = "Roiske";
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
		particleSystem.renderer.sortingLayerID = spriteRenderer.sortingLayerID;
		particleSystem.renderer.sortingOrder = spriteRenderer.sortingOrder;
		particleSystem.renderer.sortingLayerName = "BloodInFront";
		//kulma = particleSystem.transform.localEulerAngles.z;
			}
			
		
		

	}
	


