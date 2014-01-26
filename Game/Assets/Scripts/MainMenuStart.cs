using UnityEngine;
using System.Collections;

public class MainMenuStart : MonoBehaviour {

	void OnMouseDown()
	{
		Debug.Log ("Wat");
		Application.LoadLevelAdditive("InstructionScreen");
	}
}
