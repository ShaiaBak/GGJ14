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
	private GameObject[] CardActors;


	// Use this for initialization
	void Start () {
	
	}

	void Awake()
	{
		CardActors = GameObject.FindGameObjectsWithTag("Card");
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

			Debug.Log("CardActors Length: " + CardActors.Length + ", i: " + i);
			if (i < CardActors.Length)
			{
				CardCommand[] NewCardCommands = NewCard.GetCommands();
				CardActors[i].GetComponent<CardActorClass>().SetupUserInterface(NewCardCommands[0], NewCardCommands[1]);
			}
		}
	}

	public Card GetCardFromCurrentPool(int index)
	{
		if (index > 0 && index < CurrentCardPool.Length)
		{
			return CurrentCardPool[index];
		}

		Debug.Log("ERROR HAPPENED WHILE GETTING CARD");
		return new Card();
	}


}
