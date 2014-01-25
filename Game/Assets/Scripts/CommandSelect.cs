using UnityEngine;
using System;

public class CommandSelect : MonoBehaviour {

	//DEFINING EACH COMMAND
	public enum Command {
		LEFT, RIGHT, UP, DOWN, HOLD, SHOOT, STAB 
	}

	//Input from Builder
	public Command commandChoice1;
	public Command commandChoice2;

	public bool isAction = false;
	public bool isActionCard = false;

	//TODO Remove texts, only used for testing
	public GUIText commandText1;
	public GUIText commandText2;

	//Textures for each command
	public Texture2D leftTexture;

	//Display for command image
	public GameObject commandDisplay1;
	public GameObject commandDisplay2;


	void Start () {
		commandText1.text = "";
		commandText2.text = "";
		
		checkAction (commandChoice1);
		checkAction (commandChoice2);

		if (isAction) {
			isActionCard = true;
		}

		assignCommand (commandChoice1, commandText1, commandDisplay1);
		assignCommand (commandChoice2, commandText2, commandDisplay2);
	}
	// If either STAB or SHOOT are present, set a flag to state that an action is Present
	void checkAction ( Command choiceParameter ) {
		if (choiceParameter == Command.STAB || choiceParameter == Command.SHOOT) {
			isAction = true;
		}
	}
	//Determine what the command is and assign an action/movement
	//TODO Remove Command Text and replace with displays
	void assignCommand ( Command choiceParameter, GUIText commandTextParameter, GameObject commandDisplayParameter ) {
		switch (choiceParameter) {
			case Command.LEFT:
			commandTextParameter.text = "Left";
			commandDisplayParameter.renderer.material.mainTexture = leftTexture;
			break;
	
			case Command.RIGHT:
			commandTextParameter.text = "Right";
			break;

			case Command.UP:
			commandTextParameter.text = "Up";
			break;

			case Command.DOWN:
			commandTextParameter.text = "Down";
			break;
		
			case Command.HOLD:
			commandTextParameter.text = "Hold";
			break;

			case Command.SHOOT:
			commandTextParameter.text = "Shoot";
			break;
		
			case Command.STAB:
			commandTextParameter.text = "Stab";
			break;
		}
	
	}
}