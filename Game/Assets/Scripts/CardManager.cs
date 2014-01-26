using UnityEngine;
using System.Collections;

// Manages and generates the card pool; available to all classes
// Should be a singleton?

public enum CardCommand
{
	CC_MoveLeft = 0,
	CC_MoveRight,
	CC_MoveUp,
	CC_MoveDown,
	CC_AttackAdj,
	CC_ShootLeft,
	CC_ShootRight,
	CC_ShootUp,
	CC_ShootDown,
	CC_HoldPosition,
	CC_LASTINDEX
};

public struct Card
{
	private CardCommand FirstCommand;
	private CardCommand SecondCommand;
	
	public Card(bool bGenerateAsPureMove)
	{
		FirstCommand  = CardCommand.CC_LASTINDEX;
		SecondCommand = CardCommand.CC_LASTINDEX;
		GenerateValues(bGenerateAsPureMove);
	}

	public void GenerateValues(bool bIsPureMove)
	{
		if (bIsPureMove)
		{
			FirstCommand = (CardCommand) Random.Range(0, (int) CardCommand.CC_MoveDown + 1);
			SecondCommand = (CardCommand) Random.Range(0, (int) CardCommand.CC_MoveDown + 1);

			Debug.Log("Card Generated: 1st: " + FirstCommand.ToString() + ", 2nd: " + SecondCommand.ToString());
		}
		else
		{
			FirstCommand = (CardCommand) Random.Range(0, (int) CardCommand.CC_LASTINDEX);
			SecondCommand = (CardCommand) Random.Range(0, (int) CardCommand.CC_LASTINDEX);
			
			Debug.Log("Card Generated: 1st: " + FirstCommand.ToString() + ", 2nd: " + SecondCommand.ToString());
		}

		// We need to prevent redundant movements - if it's redundant, we're just going to double up
		// @ZackM: THIS IS ALSO BAD CODE. AAAHHHHHHHH!!!!
		bool bIsRedundant = false;
		switch(FirstCommand)
		{
		case CardCommand.CC_MoveDown: 
			bIsRedundant = (SecondCommand == CardCommand.CC_MoveUp);
			break;
		case CardCommand.CC_MoveLeft: 
			bIsRedundant = (SecondCommand == CardCommand.CC_MoveRight);
			break;
		case CardCommand.CC_MoveRight: 
			bIsRedundant = (SecondCommand == CardCommand.CC_MoveLeft);
			break;
		case CardCommand.CC_MoveUp: 
			bIsRedundant = (SecondCommand == CardCommand.CC_MoveDown);
			break;
		}

		if (bIsRedundant)
		{ 
			Debug.Log("Redundancy detected, doubling up move");
			SecondCommand = FirstCommand; 
		}

	}

	public bool IsValid()
	{
		return (FirstCommand != CardCommand.CC_LASTINDEX && SecondCommand != CardCommand.CC_LASTINDEX);
	}
	
	public CardCommand[] GetCommands()
	{
		return new CardCommand[2] { FirstCommand, SecondCommand };
	}
}

public class CardManager : MonoBehaviour {

	private const int CARDPOOLMAX = 6;
	private Card[] CurrentCardPool;

	public GameObject CardActor0;
	public GameObject CardActor1;
	public GameObject CardActor2;
	public GameObject CardActor3;
	public GameObject CardActor4;
	public GameObject CardActor5;


	// Use this for initialization
	void Start () {
	
	}

	void Awake()
	{
		CurrentCardPool = new Card[CARDPOOLMAX];
		GenerateCardPool();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GenerateCardPool()
	{
		for (int i = 0; i < CurrentCardPool.Length; i++)
		{
			Card NewCard;

			// When Generating Card Pool, we have one rule:
			// 1. The first two cards selected must be PURE movement cards
			if (i < 2)
			{
				NewCard = new Card(true);
			}
			// 2. The remaining for cards can be randomized
			else
			{
				NewCard = new Card(false);
			}

			CurrentCardPool[i] = NewCard;

			CardCommand[] NewCardCommands = NewCard.GetCommands();
			CardActorClass CAClass =CardActor0.GetComponent<CardActorClass>(); // BAD CODING - NEED TO THROW EXCEPTION INSTEAD!
			switch(i)
			{
			case 0: CAClass = CardActor0.GetComponent<CardActorClass>(); break;
			case 1: CAClass = CardActor1.GetComponent<CardActorClass>(); break;
			case 2: CAClass = CardActor2.GetComponent<CardActorClass>(); break;
			case 3: CAClass = CardActor3.GetComponent<CardActorClass>(); break;
			case 4: CAClass = CardActor4.GetComponent<CardActorClass>(); break;
			case 5: CAClass = CardActor5.GetComponent<CardActorClass>(); break;
			};

			CAClass.SetupUserInterface(NewCardCommands[0], NewCardCommands[1]);
		}
	}

	public Card GetCardFromCurrentPool(int index)
	{
		if (index >= 0 && index < CurrentCardPool.Length)
		{
			return CurrentCardPool[index];
		}

		Debug.Log("CardManager: ERROR HAPPENED WHILE GETTING CARD (index=" + index + ")");
		return new Card();
	}


}
