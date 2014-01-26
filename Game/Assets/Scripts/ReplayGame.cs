using UnityEngine;
using System.Collections;

public class ReplayGame : MonoBehaviour {

	void OnMouseDown() {
		Application.LoadLevel("Gameboard");
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Return)) {
			Application.LoadLevel("MainMenu");
		}
	}
}
