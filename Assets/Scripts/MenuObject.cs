using UnityEngine;
using System.Collections;

public class MenuObject : MonoBehaviour {

	public bool isQuit = false;

	void Start () {
		Debug.LogError("Started MenuObject script.");      
	}

	void onMouseEnter() {

		renderer.material.color = Color.red;
		Debug.LogError("Entered menuobject area."); 
	}

	void onMouseExit() {
		
		renderer.material.color = Color.white;
	}

	void onMouseDown() {
		
		if (isQuit) {
			Application.Quit ();
		} else {
			Application.LoadLevel (1);
		}
	}
}
