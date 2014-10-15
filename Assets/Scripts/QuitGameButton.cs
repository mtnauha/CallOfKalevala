using UnityEngine;
using System.Collections;

public class QuitGameButton : MonoBehaviour {

	void OnMouseOver() {
		renderer.material.color -= new Color(0.1F, 0, 0) * Time.deltaTime;
	}
}
