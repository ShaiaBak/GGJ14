using UnityEngine;
using System.Collections;

public class MainMenuStart : MonoBehaviour {

	void OnMouseDown() {
		PlayerPrefs.SetInt("1PMode",0);;
		Destroy (GameObject.FindWithTag("MainMenu"));
		Application.LoadLevelAdditive("InstructionScreen");
	}

	// Update is called once per frame
//	void Update () {
//		if (Input.GetKeyDown (KeyCode.Return) ) {
//			PlayerPrefs.SetInt("1PMode",0);
//			Destroy (GameObject.FindWithTag("MainMenu"));
//			Application.LoadLevel("InstructionScreen");
//		}
//	}
}
