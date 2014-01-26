using UnityEngine;
using System.Collections;

public class CharacterActionsClass : MonoBehaviour {

	private GameboardController gameboard;

	// Use this for initialization
	void Start () {
		gameboard = GameObject.FindGameObjectWithTag("Gameboard").GetComponent<GameboardController>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("P1_Choose1")){
			Swipe();
		}
	}

	private void Swipe(){
//		print (gameboard.GetTileAtCoordinate(transform.position.x, transform.position.y+1));
//		print (gameboard.GetTileAtCoordinate(transform.position.x+1, transform.position.y));
//		print (gameboard.GetTileAtCoordinate(transform.position.x, transform.position.y-1));
//		print (gameboard.GetTileAtCoordinate(transform.position.x-1, transform.position.y));

		TileClass tile = gameboard.GetTileAtCoordinate(transform.position.x, transform.position.y).GetComponent<TileClass>();
		if(!tile.HasWall(TileClass.NORTH)){
			gameboard.AttackTile((int)transform.position.x, (int)transform.position.y+1);
		}
		if(!tile.HasWall(TileClass.EAST)){
			gameboard.AttackTile((int)transform.position.x+1, (int)transform.position.y);
		}
		if(!tile.HasWall(TileClass.SOUTH)){
			gameboard.AttackTile((int)transform.position.x, (int)transform.position.y-1);
		}
		if(!tile.HasWall(TileClass.WEST)){
			gameboard.AttackTile((int)transform.position.x-1, (int)transform.position.y);
		}

	}

	private void Shoot(int direction){
		
	}
	
}
