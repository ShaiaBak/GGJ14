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
		if(Input.GetButtonDown("P1_Choose3")){
			Shoot(TileClass.EAST);
		}
		if(Input.GetButtonDown("P2_Choose2")){
			gameboard.CreateWall((int)transform.position.x, (int)transform.position.y, TileClass.NORTH);
		}
		if(Input.GetButtonDown("P2_Choose1")){
			gameboard.DestroyWall((int)transform.position.x, (int)transform.position.y, TileClass.NORTH);
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
		//Pretend we're shooting right
		//Loop through transform.position.x to gameboardcontroller.width-1
		for(int i=(int)transform.position.x+1; i<gameboard.width; i++){
			Instantiate(Camera.main.GetComponent<ObjectStore>().hWall, new Vector3(i,transform.position.y), Quaternion.identity);
			GameObject tile = gameboard.GetTileAtCoordinate(i,transform.position.y);
			if(tile != null){
				TileClass tc = tile.GetComponent<TileClass>();
				if(tc.HasEntity()){
					tc.entity.GetComponent<CharacterClass>().Die();
				}
			}
		}
	}
	
}
