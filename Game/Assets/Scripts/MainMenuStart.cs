using UnityEngine;
using System.Collections;

public class MainMenuStart : MonoBehaviour {

	void OnMouseDown()
	{	Destroy (this.gameObject);
		Application.LoadLevelAdditive("InstructionScreen");
	}
}
