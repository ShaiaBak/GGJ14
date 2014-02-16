using UnityEngine;
using System.Collections;

public class GameSelector : MonoBehaviour {
	
	private bool bSingleMode = true; 
	// True = 1P mode, False = 2P mode

	void Update () {
		if (bSingleMode && Input.GetKeyDown (KeyCode.DownArrow) ) {
			transform.position = new Vector2 (transform.position.x, transform.position.y - 1f);
			bSingleMode = !bSingleMode;
		}
		if (!bSingleMode && Input.GetKeyDown (KeyCode.UpArrow) ) {
			transform.position = new Vector2 (transform.position.x, transform.position.y + 1f);
			bSingleMode = !bSingleMode;
		}
		if (Input.GetKeyDown (KeyCode.Return)) {

			if (bSingleMode) {
				PlayerPrefs.SetInt("1PMode",1);
				Destroy (GameObject.FindWithTag("MainMenu"));
				Application.LoadLevelAdditive("InstructionScreen");
			} else {
				PlayerPrefs.SetInt("1PMode",0);
				Destroy (GameObject.FindWithTag("MainMenu"));
				Application.LoadLevelAdditive("InstructionScreen");
			}
		}
	}
}