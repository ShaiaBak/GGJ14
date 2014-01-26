using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameboardController : MonoBehaviour {

	private GameObject[,] tileArray;
	public int width;
	public int height;
	private bool gameEnded = false;
	private float endGameTimer = 3f;
	private int winner = -1;

	// Use this for initialization
	void Awake () {
		width = 0;
		height = 0;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Tile");
		foreach (GameObject o in array) {
			int x = (int) o.transform.position.x;
			int y = (int) o.transform.position.y;
			if (width < x) {
				width = x;
			}
			if (height < y) {
				height = y;
			}
		}
		// width = max index + 1
		width++;
		height++;

		tileArray = new GameObject[width, height];
		foreach(GameObject o in array){
			int x = (int) o.transform.position.x;
			int y = (int) o.transform.position.y;
			tileArray[x,y] = o;
		}

		array = GameObject.FindGameObjectsWithTag("Wall");
		foreach(GameObject o in array){
			float x = o.transform.position.x;
			float y = o.transform.position.y;


			if (Mathf.FloorToInt(x) == Mathf.CeilToInt(x)){
				// is a horizontal wall
				int ydn = Mathf.FloorToInt(y);
				int yup = Mathf.CeilToInt(y);
				if (ydn >= 0) {
					tileArray[(int) x, ydn].GetComponent<TileClass>().SetWall(TileClass.NORTH, o);
				}
				if (yup < height) {
					tileArray[(int) x, yup].GetComponent<TileClass>().SetWall(TileClass.SOUTH, o);
				}
			}
			else if (Mathf.FloorToInt(y) == Mathf.CeilToInt(y)){
				// is a vertical wall
				int xdn = Mathf.FloorToInt(x);
				int xup = Mathf.CeilToInt(x);
				if (xdn >= 0) {
					tileArray[xdn, (int) y].GetComponent<TileClass>().SetWall(TileClass.EAST, o);
				}
				if (xup < width) {
					tileArray[xup, (int) y].GetComponent<TileClass>().SetWall(TileClass.WEST, o);
				}
			}
		}
		RandomizePlayerLocations();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.LoadLevel("MainMenu");
		}

		if (gameEnded) {
			endGameTimer -= Time.deltaTime;
			if (endGameTimer < 0) {
				switch(winner) {
				case 1:
					Application.LoadLevel (4);
					break;
				case 2:
					Application.LoadLevel (3);
					break;
				}
			}
		}
	}

	private void RandomizePlayerLocations(){
		List<GameObject> characterList = new List<GameObject>();
		characterList.AddRange(GameObject.FindGameObjectsWithTag("Character"));
		characterList.AddRange(GameObject.FindGameObjectsWithTag("P1"));
		characterList.AddRange(GameObject.FindGameObjectsWithTag("P2"));
		// Get 2 different index
		int i = Random.Range(0,characterList.Count);
		int j = 0;
		do {
			j = Random.Range (0, characterList.Count);
		} while (j == i);

		// Retag everything
		for(int x=0; x<characterList.Count; x++){
			if(x == i){
				characterList[x].tag = "P1";
			}else if(x == j){
				characterList[x].tag = "P2";
			}else{
				characterList[x].tag = "Character";
			}
		}

		// Place them randomly on the board
		for (int k = 0; k < characterList.Count; k++) {
			i = Random.Range (0, width);
			j = Random.Range (0, height);
			characterList[k].transform.position = new Vector3(i,j,0);

			for (int l = 0; l < k; l++) {
				float dx = Mathf.Abs (characterList[k].transform.position.x - characterList[l].transform.position.x);
				float dy = Mathf.Abs (characterList[k].transform.position.y - characterList[l].transform.position.y);
				if (dx < 1.1f && dy < 1.1f) {
					k--; // redo iteration of outer loop
					break;
				}
			}
		}
	}

	/// <summary>
	/// Moves the character one space in a direction.
	/// </summary>
	/// <param name="character">Character.</param>
	/// <param name="direction">Direction. eg. (MoveCharacter(player0, TileClass.NORTH))</param>
	public void MoveCharacter(GameObject character, int direction) {
		CharacterClass cc = character.GetComponent<CharacterClass>();
		if(cc!=null){
			cc.Move(direction);
		}
	}

	/// <summary>
	/// Makes the character attack in all four adjacent spaces.
	/// </summary>
	/// <param name="character">Character.</param>
	public void CharacterSwipe(GameObject character){
		CharacterActionsClass cac = character.GetComponent<CharacterActionsClass>();
		if(cac!=null){
			cac.Swipe();
		}
	}

	/// <summary>
	/// Makes the character shoot in a direction. Bullets will stop on collision.
	/// </summary>
	/// <param name="character">Character.</param>
	/// <param name="direction">Direction. eg. (CharacterShoot(player0, TileClass.NORTH))</param>
	public void CharacterShoot(GameObject character, int direction){
		CharacterActionsClass cac = character.GetComponent<CharacterActionsClass>();
		if(cac!=null){
			cac.Shoot(direction);
		}
	}

	public bool AttackTile(GameObject tile){
		if(tile == null){
			return false;
		}
		TileClass tc = tile.GetComponent<TileClass>();
		if(tc != null){
			if(tc.entity != null){
				// Check if the entity on the tile is a character
				CharacterClass cc = tc.entity.GetComponent<CharacterClass>();
				if(cc != null && (tc.entity.tag == "P1" || tc.entity.tag == "P2" || tc.entity.tag == "Character")){
					cc.Die();
					return true;
				}
			}
		}
		return false;
	}


	public bool AttackTile(int x, int y){
		return AttackTile(GetTileAtCoordinate(x,y));
	}

	public void CreateWall(GameObject character, int direction) {
		CreateWall ((int)character.transform.position.x, (int)character.transform.position.y, direction);
	}

	// Create a wall
	// (x, y) is the coordinate of the tile
	// direction is the direction from the tile where the wall should be created
	public void CreateWall(int x, int y, int direction) {
		TileClass tile = GetTileAtCoordinate (x, y).GetComponent<TileClass>();
		TileClass otherTile = null;
		GameObject obj;
		ObjectStore objStore = Camera.main.GetComponent<ObjectStore>();
		GameObject wall;
		float wallX = x;
		float wallY = y;

		switch (direction) {
		case TileClass.NORTH:
			obj = GetTileAtCoordinate (x, y+1);
			if (obj) {
				otherTile = obj.GetComponent<TileClass>();
			}
			if (!tile.HasWall (TileClass.NORTH) && otherTile != null && !otherTile.HasWall (TileClass.SOUTH)) {
				wallY += 0.5f;
				wall = objStore.CreateHorizontalWall(wallX, wallY);
				tile.SetWall (TileClass.NORTH, wall);
				otherTile.SetWall (TileClass.SOUTH, wall);
			}
			break;
		case TileClass.EAST:
			obj = GetTileAtCoordinate (x+1, y);
			if (obj) {
				otherTile = obj.GetComponent<TileClass>();
			}
			if (!tile.HasWall (TileClass.EAST) && otherTile != null && !otherTile.HasWall (TileClass.WEST)) {
				wallX -= 0.5f;
				wall = objStore.CreateVerticalWall(wallX, wallY);
				tile.SetWall (TileClass.EAST, wall);
				otherTile.SetWall (TileClass.WEST, wall);
			}
			break;
		case TileClass.SOUTH:
			obj = GetTileAtCoordinate (x, y-1);
			if (obj) {
				otherTile = obj.GetComponent<TileClass>();
			}
			if (!tile.HasWall (TileClass.SOUTH) && otherTile != null && !otherTile.HasWall (TileClass.NORTH)) {
				wallY -= 0.5f;
				wall = objStore.CreateHorizontalWall(wallX, wallY);
				tile.SetWall (TileClass.SOUTH, wall);
				otherTile.SetWall (TileClass.NORTH, wall);
			}
			break;
		case TileClass.WEST:
			obj = GetTileAtCoordinate (x-1, y);
			if (obj) {
				otherTile = obj.GetComponent<TileClass>();
			}
			if (!tile.HasWall (TileClass.WEST) && otherTile != null && !otherTile.HasWall (TileClass.EAST)) {
				wallX += 0.5f;
				wall = objStore.CreateVerticalWall(wallX, wallY);
				tile.SetWall (TileClass.WEST, wall);
				otherTile.SetWall (TileClass.EAST, wall);
			}
			break;
		}
	}

	public void DestroyWall(GameObject character, int direction) {
		DestroyWall ((int)character.transform.position.x, (int)character.transform.position.y, direction);
	}

	// Destroy a wall
	// (x, y) is the coordinate of the tile
	// direction is the direction from the tile where the wall should be destroyed
	public void DestroyWall(int x, int y, int direction) {
		TileClass tile = GetTileAtCoordinate (x, y).GetComponent<TileClass>();
		GameObject obj = GetNeighbouringTile (x, y, direction);
		TileClass otherTile = null;
		if (obj != null) {
			otherTile = obj.GetComponent<TileClass>();
		}
		GameObject wall = null;

		int opposite = TileClass.getOppositeDirection (direction);
		if (otherTile != null && otherTile.HasWall (opposite)) {
			wall = otherTile.GetWall(opposite);
			otherTile.SetWall(opposite, null);
		}
		if (tile.HasWall (direction)) {
			wall = tile.GetWall(direction);
			otherTile.SetWall(direction, null);
		}

		if (wall != null) {
			Destroy (wall);
		}
	}

	public GameObject GetNeighbouringTile(int x, int y, int direction) {
		switch (direction) {
		case TileClass.NORTH:
			return GetTileAtCoordinate (x, y+1);
		case TileClass.EAST:
			return GetTileAtCoordinate (x+1, y);
		case TileClass.SOUTH:
			return GetTileAtCoordinate (x, y-1);
		case TileClass.WEST:
			return GetTileAtCoordinate (x-1, y);
		}
		return null;
	}

	public TileClass GetNeighbourTileClass(int x, int y, int direction) {
		GameObject obj = GetNeighbouringTile (x, y, direction);
		if (obj != null) {
			TileClass tc = obj.GetComponent<TileClass>();
			if (tc != null) {
				return tc;
			}
		}
		return null;
	}

	public GameObject GetTileAtCoordinate(int x, int y){
		if (x < 0 || y < 0 || x >= width || y >= height) {
			return null;
		}
		return tileArray [x, y];
	}
	public GameObject GetTileAtCoordinate(float x, float y){
		return GetTileAtCoordinate ((int)x, (int)y);
	}

	public TileClass GetTileClassAtCoordinate(int x, int y) {
		GameObject obj = GetTileAtCoordinate (x, y);
		if (obj != null) {
			TileClass tc = obj.GetComponent<TileClass>();
			if (tc != null) {
				return tc;
			}
		}
		return null;
	}

	public TileClass GetTileClassAtCoordinate(float x, float y) {
		return GetTileClassAtCoordinate ((int)x, (int)y);
	}

	public void EndGame(int player) {
		gameEnded = true;
		winner = player;
	}
}
