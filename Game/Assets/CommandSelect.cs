using UnityEngine;
using System;

public class CommandSelect : MonoBehaviour {

	//DEFINING EACH COMMAND
	public enum Command {
		LEFT, RIGHT, UP, DOWN, HOLD, SHOOT, STAB 
	}

	public string[] commandArray;

	public Command commandChoice1;
	public Command commandChoice2;

	public bool isAction = false;
	public bool isActionCard = false;



	void Start () {

		if (isAction) {
			isActionCard = true;
		}



//		if (commandArray[0] == null || commandArray[1] == null) {
//			//IF NEITHER CHOICE HAS A COMMAND
//		}
//		checkAction(commandArray);
	}

	void checkAction ( Command choiceParameter ) {
		if (choiceParameter == Command.STAB || choiceParameter == Command.SHOOT) {
			isAction = true;
		}
	}}