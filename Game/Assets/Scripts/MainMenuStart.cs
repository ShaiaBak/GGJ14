using UnityEngine;
using System.Collections;

public class MainMenuStart : MonoBehaviour {

	void OnMouseDown()
	{
		Application.LoadLevelAdditive("InstructionScreen");
	}
}
