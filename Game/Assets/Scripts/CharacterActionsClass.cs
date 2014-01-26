using UnityEngine;
using System.Collections;

public class CharacterActionsClass : MonoBehaviour {

	public GameObject projectileobj;
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
			Instantiate(projectileobj, new Vector3((int)pos.x, (int)pos.y), Quaternion.Euler(0,0,90));
		}
		if(!tile.HasWall(TileClass.EAST)){
			Vector3 pos = new Vector2(transform.position.x+1, transform.position.y);
			gameboard.AttackTile((int)pos.x, (int)pos.y);
			Instantiate(projectileobj, new Vector3((int)pos.x, (int)pos.y), Quaternion.identity);
		}
		if(!tile.HasWall(TileClass.SOUTH)){
			Vector3 pos = new Vector2(transform.position.x, transform.position.y-1);
			gameboard.AttackTile((int)pos.x, (int)pos.y);
			Instantiate(projectileobj, new Vector3((int)pos.x, (int)pos.y), Quaternion.Euler(0,0,-90));
		}
		if(!tile.HasWall(TileClass.WEST)){
			Vector3 pos = new Vector2(transform.position.x-1, transform.position.y);
			gameboard.AttackTile((int)pos.x, (int)pos.y);
			Instantiate(projectileobj, new Vector3((int)pos.x, (int)pos.y), Quaternion.Euler(0,0,180));
		}

	}

	public void Shoot(int direction){
		anim.SetTrigger ("Attack");
		audio.PlayOneShot (Camera.main.GetComponent<ObjectStore> ().shootSound);
	
		GameObject projectile = (GameObject) Instantiate(projectileobj, transform.position, Quaternion.identity);
		ProjectileClass pc = projectile.GetComponent<ProjectileClass>();
		Vector2 target = pc.transform.position;

		switch(direction){

		case TileClass.NORTH:
			projectile.transform.Rotate(new Vector3(0,0,90));
			if(gameboard.GetTileAtCoordinate(transform.position.x, transform.position.y).GetComponent<TileClass>().HasWall(TileClass.NORTH)){
				Destroy(projectile);
				break;
			}
			int i = (int) transform.position.y+1;
			// Find the target to shoot at
			while(i<gameboard.height){
				GameObject tile = gameboard.GetTileAtCoordinate((int) transform.position.x, i);
				if(tile != null){
					TileClass tc = tile.GetComponent<TileClass>();
					if(gameboard.AttackTile(tile)){
						target = tile.transform.position;
						break;
					}
					if(tc.HasWall(TileClass.NORTH)){
						target = tile.transform.position;
						break;
					}
				}
				i++;

			}
			break;
	
		case TileClass.EAST:
			if(gameboard.GetTileAtCoordinate(transform.position.x, transform.position.y).GetComponent<TileClass>().HasWall(TileClass.EAST)){
				Destroy(projectile);
				break;
			}
			int j = (int) transform.position.x+1;
			while(j<gameboard.width){
				GameObject tile = gameboard.GetTileAtCoordinate(j,transform.position.y);
				if(tile != null){
					TileClass tc = tile.GetComponent<TileClass>();
					if(gameboard.AttackTile(tile)){
						target = tile.transform.position;
						break;
					}
					if(tc.HasWall(TileClass.EAST)){
						target = tile.transform.position;
						break;
					}
				}
				j++;
			}
			break;

		case TileClass.SOUTH:
			projectile.transform.Rotate(new Vector3(0,0,-90));
			if(gameboard.GetTileAtCoordinate(transform.position.x, transform.position.y).GetComponent<TileClass>().HasWall(TileClass.SOUTH)){
				Destroy(projectile);
				break;
			}
			int k = (int) transform.position.y-1;
			while(k>=0){
				GameObject tile = gameboard.GetTileAtCoordinate(transform.position.x, k);
				if(tile != null){
					print ("x:" + transform.position.x + " y:"+ k + "pos:"+ tile.transform.position);
					TileClass tc = tile.GetComponent<TileClass>();
					if(gameboard.AttackTile(tile)){
						print ("2");
						target = tile.transform.position;
						break;
					}
					if(tc.HasWall(TileClass.SOUTH)){
						print ("1");
						target = tile.transform.position;
						break;
					}
				}
				k--;

			}
			break;

		case TileClass.WEST:
			projectile.transform.Rotate(new Vector3(0,0,180));
			if(gameboard.GetTileAtCoordinate(transform.position.x, transform.position.y).GetComponent<TileClass>().HasWall(TileClass.WEST)){
				Destroy(projectile);
				break;
			}
			int l = (int) transform.position.x-1;
			while(l>=0){
				GameObject tile = gameboard.GetTileAtCoordinate(l,transform.position.y);
				if(tile != null){
					TileClass tc = tile.GetComponent<TileClass>();
					if(gameboard.AttackTile(tile)){
						target = tile.transform.position;
						break;
					}
					if(tc.HasWall(TileClass.WEST)){
						target = tile.transform.position;
						break;
					}
				}
				l--;
			}
			break;

		}
//		print (pc.transform.position + " :: " + target);
		if(pc!=null){
			pc.SetTargetLocation(pc.transform.position, target);
		}
		
	}
	
}
