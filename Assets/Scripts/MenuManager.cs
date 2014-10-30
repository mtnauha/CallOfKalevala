using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {
	
	// Update is called once per frame
	public void ChangeToScene (string sceneToChangeTo) {
		Application.LoadLevel (sceneToChangeTo);
	}

	public void QuitGame () {
		Application.Quit();
	}
}
