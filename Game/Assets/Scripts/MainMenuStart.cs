using UnityEngine;
using System.Collections;

public class MainMenuStart : MonoBehaviour {

	void OnMouseDown() {
		Destroy (this.gameObject);
		Application.LoadLevelAdditive("InstructionScreen");
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Return)) {
			Destroy (this.gameObject);
			Application.LoadLevel("InstructionScreen");
		}
	}
}
