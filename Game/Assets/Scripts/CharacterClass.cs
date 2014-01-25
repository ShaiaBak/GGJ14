using UnityEngine;
using System.Collections;

public class CharacterClass : MonoBehaviour {

	private GameboardController gameboard;

	// Use this for initialization
	void Start () {
		gameboard = GameObject.FindGameObjectWithTag("Gameboard").GetComponent<GameboardController>();
	}
	
	// Update is called once per frame
	void Update () {
		int x = (int) transform.position.x;
		int y = (int) transform.position.y;
		int direction = -1;

		if(Input.GetButtonDown("P1_Choose2")){	// up
			y++;
			direction = TileClass.NORTH;
		}else if(Input.GetButtonDown("P1_Choose4")){	// left
			x--;
			direction = TileClass.WEST;
		}else if(Input.GetButtonDown("P1_Choose5")){	// down
			y--;
			direction = TileClass.SOUTH;
		}else if(Input.GetButtonDown("P1_Choose6")){	// right
			x++;
			direction = TileClass.EAST;
		}

		if (direction >= 0 && direction < 4) {
			GameObject obj = gameboard.GetTileAtCoordinate(transform.position.x, transform.position.y);
			TileClass currentTile = null;
			if (obj != null) {
				currentTile = obj.GetComponent<TileClass>();
			}
			obj = gameboard.GetTileAtCoordinate(x, y);
			TileClass nextTile = null;
			if (obj != null) {
				nextTile = obj.GetComponent<TileClass>();
			}
			
			if (currentTile != null && !currentTile.hasWall (direction)) {
				// todo check other tile for existing character
				transform.position = new Vector2 (x,y);
			}
		}
	}
}
