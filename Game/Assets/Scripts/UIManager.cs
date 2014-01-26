using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

	private GameObject PlayerCamera;

	// Dimension Specifications
	const int GameWidth = 1280;
	const int GameHeight = 720;

	int UIStartX;
	int UIEndX;
	int UIStartY;
	int UIEndY;
	
	//** For Cards
	const int CARDHEIGHT = 150; //px
	const int CARDHPADDING = 17; //px
	const int CARDVPADDING = 45; //px
	const int CARDINTERPADDING = 75; //px

	const int CARDFIRSTACTOFFSET = 40; //px
	const int CARDSECONDACTOFFSET = 98; //px

	// ** Textures
	


	// Notify bools
	bool bRefreshCardUI;


	// Use this for initialization
	void Start () {
		bRefreshCardUI = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (bRefreshCardUI)
		{
			RefreshCardUI();
		}

		// Need to check for when a player has "Locked In" to signify

	}

	void RefreshCardUI()
	{
		bRefreshCardUI = false;
		gameObject.GetComponent<CardManager>().GenerateCardPool();

		// We need to figure out how we're going to display our cards...
		// Could we dynamically create prefabs?
		// Or just have game objects set up ahead of time that we modify?
	}

	public void NotifyForCardRefresh()
	{
		bRefreshCardUI = true;
	}
}
