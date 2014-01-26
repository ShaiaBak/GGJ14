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
//			Swipe();
			Shoot(TileClass.WEST);
		}
		if(Input.GetButtonDown("P1_Choose3")){
			Shoot(TileClass.SOUTH);
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

		switch(direction){

		case TileClass.NORTH:
			int i = (int) transform.position.y;
			while(i<gameboard.height){
				Instantiate(Camera.main.GetComponent<ObjectStore>().projectile, new Vector3(transform.position.x, i), Quaternion.identity);
				GameObject tile = gameboard.GetTileAtCoordinate(transform.position.x, i);
				if(tile != null){
					TileClass tc = tile.GetComponent<TileClass>();
					if(tc.HasEntity() && tc.entity.tag == "Player" && tc.entity != gameObject){
						tc.entity.GetComponent<CharacterClass>().Die();
						break;
					}else if(tc.HasWall(TileClass.NORTH)){
						break;
					}
				}
				i++;
			}
			break;
	
		case TileClass.EAST:
			int j = (int) transform.position.x;
			while(j<gameboard.width){
				Instantiate(Camera.main.GetComponent<ObjectStore>().projectile, new Vector3(j,transform.position.y), Quaternion.identity);
				GameObject tile = gameboard.GetTileAtCoordinate(j,transform.position.y);
				if(tile != null){
					TileClass tc = tile.GetComponent<TileClass>();
					if(tc.HasEntity() && tc.entity.tag == "Player" && tc.entity != gameObject){
						tc.entity.GetComponent<CharacterClass>().Die();
						break;
					}else if(tc.HasWall(TileClass.EAST)){
						break;
					}
				}
				j++;
			}
			break;

		case TileClass.SOUTH:
			int k = (int) transform.position.y;
			while(k>=0){
				Instantiate(Camera.main.GetComponent<ObjectStore>().projectile, new Vector3(transform.position.x, k), Quaternion.identity);
				GameObject tile = gameboard.GetTileAtCoordinate(transform.position.x, k);
				if(tile != null){
					TileClass tc = tile.GetComponent<TileClass>();
					if(tc.HasEntity() && tc.entity.tag == "Player" && tc.entity != gameObject){
						tc.entity.GetComponent<CharacterClass>().Die();
						break;
					}else if(tc.HasWall(TileClass.SOUTH)){
						break;
					}
				}
				k--;
			}
			break;

		case TileClass.WEST:
			int l = (int) transform.position.x;
			while(l>=0){
				Instantiate(Camera.main.GetComponent<ObjectStore>().projectile, new Vector3(l,transform.position.y), Quaternion.identity);
				GameObject tile = gameboard.GetTileAtCoordinate(l,transform.position.y);
				if(tile != null){
					TileClass tc = tile.GetComponent<TileClass>();
					if(tc.HasEntity() && tc.entity.tag == "Player" && tc.entity != gameObject){
						tc.entity.GetComponent<CharacterClass>().Die();
						break;
					}else if(tc.HasWall(TileClass.WEST)){
						break;
					}
				}
				l--;
			}
			break;
		}
		
	}
	
}
