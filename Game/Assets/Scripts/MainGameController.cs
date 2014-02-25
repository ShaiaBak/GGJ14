using UnityEngine;
using System.Collections;

enum GameState
{
	GS_AWAITING_INPUT,
	GS_SIMULATING,
	GS_INMENU
};

public struct GameboardCommand
{
	private GameObject Character;
	private Card CommandsCard;

	public GameboardCommand(GameObject CharacterRef, Card ComCard)
	{
		Character = CharacterRef;
		CommandsCard = ComCard;
	}

	public GameObject GetCharacter()
	{
		return Character;
	}

	public Card GetCard()
	{
		return CommandsCard;
	}
}

public class MainGameController : MonoBehaviour {

	GameState GS;

	private GameObject Board;
	private GameboardController GBController;
	private CardManager CManager;
	private GameObject P1Character;
	private GameObject P2Character;
	private GameObject[] NPCCharacters;

	private int P1CurrentChoice;
	private int P2CurrentChoice;
	private int[] NPCCurrentChoices;

	private string[] P1InputNames;
	private string[] P2InputNames;

	public GameObject P1LockInObj;
	public GameObject P2LockInObj;
	public Texture P1Waiting;
	public Texture P2Waiting;
	public Texture P1Locked;
	public Texture P2Locked;

	const float BOARDUPDATECYCLE = 2.0f;
	float BoardUpdateTime = 0.0f;
	int BoardActionCycle = 0;

	private GameboardCommand[] GBCommands;

	// Use this for initialization
	void Start () {

		Board = GameObject.FindGameObjectWithTag("Gameboard");
		GBController = Board.GetComponent<GameboardController>();
		P1Character = GameObject.FindGameObjectWithTag("P1");
		P2Character = GameObject.FindGameObjectWithTag("P2");
		NPCCharacters = GameObject.FindGameObjectsWithTag("Character");

		CManager = Camera.main.GetComponent<CardManager>();

		NPCCurrentChoices = new int[NPCCharacters.Length];

		ResetInputs();

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
				PackageActionsForBoard();
			}
		}
		else if (GS == GameState.GS_SIMULATING)
		{
			if (BoardUpdateTime >= BOARDUPDATECYCLE)
			{
				if (BoardActionCycle >= 2) // Magic Number, but 2 is just for two actions/cycles
				{
//					Debug.Log("SIMULATION: Finishing Cycle, going back to Input");
					// Check if we're in a gameover state
					CheckGameover();
					// We're done simulating, wait for input again
					ResetInputs();
					CManager.GenerateCardPool();
				}
				else
				{
//					Debug.Log("SIMULATION: Executing Cycle: " + BoardActionCycle);
					// DO ALL CURRENT ACTIONS IN CYCLE
					// We're running two loops - the first runs all commands that are moves, the second runs the remaining actions
					for (int i = 0; i < GBCommands.Length; i++)
					{
						ExecuteGBCommand(GBCommands[i], BoardActionCycle, true, false);
					}
					for (int i = 0; i < GBCommands.Length; i++)
					{
						ExecuteGBCommand(GBCommands[i], BoardActionCycle, false, true);
					}

					BoardActionCycle++;
					BoardUpdateTime = 0.0f;
				}
			}
			else
			{
				BoardUpdateTime += Time.deltaTime;
			}
		}
	
	}

	// Check if we're in a gameover state (ie p1 or p2 or both dead)
	private void CheckGameover(){
		// Both players dead
		if(GBController.player1 == null && GBController.player2 == null){
			Application.LoadLevel ("tiescreen");
			print("tie");
		}
		// Computer wins
		else if(GBController.player1 == null && GBController.IsSPMode() == 1){
			Application.LoadLevel ("compwin");
		}
		// Player1 wins
		else if(GBController.player2 == null){
			Application.LoadLevel ("player1win");
		}
		// Player2 wins
		else if(GBController.player1 == null){
			Application.LoadLevel ("player2win");

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

					if (P1LockInObj != null)
					{
						P1LockInObj.renderer.material.mainTexture = P1Locked;
					}

					break;
				}
			}
		}

		// Find out what player two is pressing
		if (GBController.IsSPMode() == 1) {
			// Player two is pressing a random key
			P2CurrentChoice = Random.Range (0, 6);
			P2LockInObj.renderer.material.mainTexture = P2Locked;
		} else if (P2CurrentChoice == -1) {
			for (int i = 0; i < P2InputNames.Length; i++) {
				if (Input.GetButtonDown (P2InputNames [i])) {
					P2CurrentChoice = i;

					if (P2LockInObj != null) {
						P2LockInObj.renderer.material.mainTexture = P2Locked;
					}

					break;
				}
			}
		}
	}

	void FindNPCInputs()
	{
		// Make arbitrary choices for NPCs from the card pool
		for (int i = 0; i < NPCCurrentChoices.Length; i++)
		{
			NPCCurrentChoices[i] = Random.Range(0, 6); // 6 is magic number == to CARDPOOLMAX in CardManager
			// TODO: ZackM - Fix.
		}
	}

	void PackageActionsForBoard()
	{
		FindNPCInputs();

		// Figure out how to set appropriate cards based on inputs here
		GS = GameState.GS_SIMULATING;
//		print("INPUT: Inputs accepted! Starting sim...");

		// Find the Card Manager, get associated cards with our indexes
		GBCommands = new GameboardCommand[NPCCurrentChoices.Length + 2]; // # of NPC's plus 2 players
		GBCommands[0] = new GameboardCommand( P1Character, CManager.GetCardFromCurrentPool(P1CurrentChoice));
		GBCommands[1] = new GameboardCommand( P2Character, CManager.GetCardFromCurrentPool(P2CurrentChoice));

		for (int i = 2; i < GBCommands.Length; i++)
		{
			GBCommands[i] = new GameboardCommand(NPCCharacters[i-2], CManager.GetCardFromCurrentPool(NPCCurrentChoices[i-2]));
		}

		BoardActionCycle = 0;
		BoardUpdateTime = BOARDUPDATECYCLE;
			                                                                         	
	}

	void ResetInputs()
	{
		GS = GameState.GS_AWAITING_INPUT;
		P1CurrentChoice = -1;
		P2CurrentChoice = -1;

		if (P1LockInObj != null)
		{
			P1LockInObj.renderer.material.mainTexture = P1Waiting;
		}
		if (P2LockInObj != null)
		{
			P2LockInObj.renderer.material.mainTexture = P2Waiting;
		}
	}

	// ONLY execute move if true, ONLY execute action if true
	// NOTE: ZACKM - THIS IS HORRENDOUS CODE. OH MY GOODNESS GRACIOUS!
	void ExecuteGBCommand(GameboardCommand GBCom, int CurrCycle, bool bExecuteMove, bool bExecuteAction)
	{
		CardCommand CommandToExecute = GBCom.GetCard().GetCommands()[CurrCycle];

		switch (CommandToExecute)
		{
			case CardCommand.CC_MoveLeft:
				if (bExecuteMove)
				{
//					Debug.Log(GBCom.GetCharacter().tag + " moved left");
					GBController.MoveCharacter(GBCom.GetCharacter(), TileClass.WEST);
				}
				break;

			case CardCommand.CC_MoveRight:
				if (bExecuteMove)
				{
//					Debug.Log(GBCom.GetCharacter().tag + " moved right");
					GBController.MoveCharacter(GBCom.GetCharacter(), TileClass.EAST);
				}
				break;

			case CardCommand.CC_MoveUp:
				if (bExecuteMove)
				{
//					Debug.Log(GBCom.GetCharacter().tag + " moved up");
					GBController.MoveCharacter(GBCom.GetCharacter(), TileClass.NORTH);
				}
				break;

			case CardCommand.CC_MoveDown:
				if (bExecuteMove)
				{
//					Debug.Log(GBCom.GetCharacter().tag + " moved down");
					GBController.MoveCharacter(GBCom.GetCharacter(), TileClass.SOUTH);
				}
				break;

			case CardCommand.CC_AttackAdj:
				if (bExecuteAction)
				{
//					Debug.Log(GBCom.GetCharacter().tag + " attacked adjacently");
					GBController.CharacterSwipe(GBCom.GetCharacter());
				}
				break;

			case CardCommand.CC_ShootLeft:
				if (bExecuteAction)
				{
//					Debug.Log(GBCom.GetCharacter().tag + " shot to the left");
					GBController.CharacterShoot(GBCom.GetCharacter(), TileClass.WEST);
				}
				break;

			case CardCommand.CC_ShootRight:
				if (bExecuteAction)
				{
//					Debug.Log(GBCom.GetCharacter().tag + " shot to the right");
					GBController.CharacterShoot(GBCom.GetCharacter(), TileClass.EAST);
				}
				break;
			case CardCommand.CC_ShootUp:
				if (bExecuteAction)
				{
//					Debug.Log(GBCom.GetCharacter().tag + " shot up");
					GBController.CharacterShoot(GBCom.GetCharacter(), TileClass.NORTH);
				}
				break;
			case CardCommand.CC_ShootDown:
				if (bExecuteAction)
				{
//					Debug.Log(GBCom.GetCharacter().tag + " shot down");
					GBController.CharacterShoot(GBCom.GetCharacter(), TileClass.SOUTH);
				}
				break;
			case CardCommand.CC_HoldPosition:
				if (bExecuteMove)
				{
//					Debug.Log(GBCom.GetCharacter().tag + " did not move");
				}
				break;
		}
	}
}
