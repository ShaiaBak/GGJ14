using UnityEngine;
using System.Collections;

public class InstructionKeyPress : MonoBehaviour {

	private string key;
	private const float pressTimer = 1f;

	// Use this for initialization
	void Start () {
		// warning! This depends on the name of the GameObject.
		switch (this.gameObject.name) {
		case "player1_Q":
			key = "P1_Choose1";
			break;
		case "player1_A":
			key = "P1_Choose2";
			break;
		case "player1_Z":
			key = "P1_Choose3";
			break;
		case "player1_W":
			key = "P1_Choose4";
			break;
		case "player1_S":
			key = "P1_Choose5";
			break;
		case "player1_X":
			key = "P1_Choose6";
			break;
		case "player2_I":
			key = "P2_Choose1";
			break;
		case "player2_J":
			key = "P2_Choose2";
			break;
		case "player2_N":
			key = "P2_Choose3";
			break;
		case "player2_O":
			key = "P2_Choose4";
			break;
		case "player2_K":
			key = "P2_Choose5";
			break;
		case "player2_M":
			key = "P2_Choose6";
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown (key)) {
			print ("Key pressed " + key);
			SpriteRenderer spr = this.GetComponent<SpriteRenderer> ();
			spr.color = Color.green;
		}
		if (Input.GetButtonUp (key)) {
			SpriteRenderer spr = this.GetComponent<SpriteRenderer> ();
			spr.color = Color.white;
		}
	}
}
