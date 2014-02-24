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

}
