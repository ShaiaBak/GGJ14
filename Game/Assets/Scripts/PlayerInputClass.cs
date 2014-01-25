using UnityEngine;
using System.Collections;

enum GameState
{
	GS_AWAITING_INPUT,
	GS_SIMULATING,
	GS_INMENU
};

public class PlayerInputClass : MonoBehaviour {

	GameState GS;

	private int P1CurrentChoice;
	private int P2CurrentChoice;

	private string[] P1InputNames;
	private string[] P2InputNames;

	// Use this for initialization
	void Start () {

		GS = GameState.GS_AWAITING_INPUT;
		P1CurrentChoice = -1;
		P2CurrentChoice = -1;

		P1InputNames = new string[6] {"P1_Choose1", "P1_Choose2", "P1_Choose3", "P1_Choose4", "P1_Choose5", "P1_Choose6" };
		P2InputNames = new string[6] {"P2_Choose1", "P2_Choose2", "P2_Choose3", "P2_Choose4", "P2_Choose5", "P2_Choose6" };
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetButtonDown("DebugResetInputs"))
		{
			ResetInputs();
		}

		if (GS == GameState.GS_AWAITING_INPUT)
		{
			// Go through our inputs and figure out what was chosen, update our choices
			FindPlayerInputs();

			// Pass off to the game board when both players have made a choice
			if (P1CurrentChoice != -1 && P2CurrentChoice != -1)
			{
				SendInputsToBoard();
			}
		}
	}

	void FindPlayerInputs()
	{
		// Find out what player one is pressing
		if (P1CurrentChoice == -1)
		{
			for (int i = 0; i < P1InputNames.Length; i++)
			{
				if (Input.GetButtonDown(P1InputNames[i]))
				{
					P1CurrentChoice = i;
					print(P1InputNames[i] + " pressed by P1."); // Debugging stub
					break;
				}
			}
		}

		// Find out what player two is pressing
		if (P2CurrentChoice == -1)
		{
			for (int i = 0; i < P2InputNames.Length; i++)
			{
				if (Input.GetButtonDown(P2InputNames[i]))
				{
					P2CurrentChoice = i;
					print(P2InputNames[i] + " pressed by P2."); // Debugging stub
					break;
				}
			}
		}
	}

	void SendInputsToBoard()
	{
		// Figure out how to set appropriate cards based on inputs here
		GS = GameState.GS_SIMULATING;
		print("Inputs accepted!");

		// Set to simulating, have the board do a callback to set the enum when it's done simming
	}

	void ResetInputs()
	{
		GS = GameState.GS_AWAITING_INPUT;
		P1CurrentChoice = -1;
		P2CurrentChoice = -1;
		print("Reset player inputs.");
	}

	public void PingForNextInputs()
	{

	}
}
