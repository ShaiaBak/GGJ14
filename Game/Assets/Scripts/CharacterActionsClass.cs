using UnityEngine;
using System.Collections;

public class CharacterActionsClass : MonoBehaviour {

	private GameboardController gameboard;
	Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		gameboard = GameObject.FindGameObjectWithTag("Gameboard").GetComponent<GameboardController>();
	}
	
	// Update is called once per frame
	void Update () {
//		if(Input.GetButtonDown("P1_Choose1")){
//			Swipe();
////			Shoot(TileClass.NORTH);
//		}
//		if(Input.GetButtonDown("P1_Choose3")){
//			Shoot(TileClass.WEST);
//		}
//		if(Input.GetButtonDown("P2_Choose2")){
//			gameboard.CreateWall((int)transform.position.x, (int)transform.position.y, TileClass.NORTH);
//		}
//		if(Input.GetButtonDown("P2_Choose1")){
//			gameboard.DestroyWall((int)transform.position.x, (int)transform.position.y, TileClass.NORTH);
//		}
	}

	public void Swipe(){

		anim.SetTrigger ("Attack");
		TileClass tile = gameboard.GetTileAtCoordinate(transform.position.x, transform.position.y).GetComponent<TileClass>();
		if(!tile.HasWall(TileClass.NORTH)){
			Vector3 pos = new Vector2(transform.position.x, transform.position.y+1);
			gameboard.AttackTile((int)pos.x, (int)pos.y);
			Instantiate(Camera.main.GetComponent<ObjectStore>().projectile, new Vector3((int)pos.x, (int)pos.y), Quaternion.identity);
		}
		if(!tile.HasWall(TileClass.EAST)){
			Vector3 pos = new Vector2(transform.position.x+1, transform.position.y);
			gameboard.AttackTile((int)pos.x, (int)pos.y);
			Instantiate(Camera.main.GetComponent<ObjectStore>().projectile, new Vector3((int)pos.x, (int)pos.y), Quaternion.identity);
		}
		if(!tile.HasWall(TileClass.SOUTH)){
			Vector3 pos = new Vector2(transform.position.x, transform.position.y-1);
			gameboard.AttackTile((int)pos.x, (int)pos.y);
			Instantiate(Camera.main.GetComponent<ObjectStore>().projectile, new Vector3((int)pos.x, (int)pos.y), Quaternion.identity);
		}
		if(!tile.HasWall(TileClass.WEST)){
			Vector3 pos = new Vector2(transform.position.x-1, transform.position.y);
			gameboard.AttackTile((int)pos.x, (int)pos.y);
			Instantiate(Camera.main.GetComponent<ObjectStore>().projectile, new Vector3((int)pos.x, (int)pos.y), Quaternion.identity);
		}

	}

	public void Shoot(int direction){
		anim.SetTrigger ("Attack");
		switch(direction){

		case TileClass.NORTH:
			int i = (int) transform.position.y+1;
			while(i<gameboard.height){
				Instantiate(Camera.main.GetComponent<ObjectStore>().projectile, new Vector3(transform.position.x, i), Quaternion.identity);
				GameObject tile = gameboard.GetTileAtCoordinate(transform.position.x, i);
				if(tile != null){
					TileClass tc = tile.GetComponent<TileClass>();
					if(tc.HasWall(TileClass.NORTH) || tc.HasWall(TileClass.SOUTH)){
						break;
					}
					if(gameboard.AttackTile(tile)){
						break;
					}
				}
				i++;
			}
			break;
	
		case TileClass.EAST:
			int j = (int) transform.position.x+1;
			while(j<gameboard.width){
				Instantiate(Camera.main.GetComponent<ObjectStore>().projectile, new Vector3(j,transform.position.y), Quaternion.identity);
				GameObject tile = gameboard.GetTileAtCoordinate(j,transform.position.y);
				if(tile != null){
					TileClass tc = tile.GetComponent<TileClass>();
					if(tc.HasWall(TileClass.EAST) || tc.HasWall(TileClass.WEST)){
						break;
					}
					if(gameboard.AttackTile(tile)){
						break;
					}
				}
				j++;
			}
			break;

		case TileClass.SOUTH:
			int k = (int) transform.position.y-1;
			while(k>=0){
				Instantiate(Camera.main.GetComponent<ObjectStore>().projectile, new Vector3(transform.position.x, k), Quaternion.identity);
				GameObject tile = gameboard.GetTileAtCoordinate(transform.position.x, k);
				if(tile != null){
					TileClass tc = tile.GetComponent<TileClass>();
					if(tc.HasWall(TileClass.SOUTH) || tc.HasWall(TileClass.NORTH)){
						break;
					}
					if(gameboard.AttackTile(tile)){
						break;
					}
				}
				k--;
			}
			break;

		case TileClass.WEST:
			int l = (int) transform.position.x-1;
			while(l>=0){
				Instantiate(Camera.main.GetComponent<ObjectStore>().projectile, new Vector3(l,transform.position.y), Quaternion.identity);
				GameObject tile = gameboard.GetTileAtCoordinate(l,transform.position.y);
				if(tile != null){
					TileClass tc = tile.GetComponent<TileClass>();
					if(tc.HasWall(TileClass.WEST) || tc.HasWall(TileClass.EAST)){
						break;
					}
					if(gameboard.AttackTile(tile)){
						break;
					}
				}
				l--;
			}
			break;
		}
		
	}
	
}
