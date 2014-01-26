using UnityEngine;
using System.Collections;

public class GameboardController : MonoBehaviour {

	private GameObject[,] tileArray;
	private int width;
	private int height;

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
//					print ("Tile (" + (int)x + "," + ydn + ") add north wall");
					tileArray[(int) x, ydn].GetComponent<TileClass>().SetWall(TileClass.NORTH, o);
				}
				if (yup < height) {
//					print ("Tile (" + (int)x + "," + yup + ") add south wall");
					tileArray[(int) x, yup].GetComponent<TileClass>().SetWall(TileClass.SOUTH, o);
				}
			}
			else if (Mathf.FloorToInt(y) == Mathf.CeilToInt(y)){
				// is a vertical wall
				int xdn = Mathf.FloorToInt(x);
				int xup = Mathf.CeilToInt(x);
				if (xdn >= 0) {
//					print ("Tile (" + xdn + "," + (int)y + ") add east wall");
					tileArray[xdn, (int) y].GetComponent<TileClass>().SetWall(TileClass.EAST, o);
				}
				if (xup < width) {
//					print ("Tile (" + xup + "," + (int)y + ") add west wall");
					tileArray[xup, (int) y].GetComponent<TileClass>().SetWall(TileClass.WEST, o);
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void MoveCharacter(GameObject player, int direction) {
		player.GetComponent<CharacterClass>().Move(direction);
	}

	public void AttackTile(GameObject tile){
		if(tile == null){
			return;
		}
		TileClass tc = tile.GetComponent<TileClass>();
		if(tc != null){
			if(tc.entity != null){
				// Check if the entity on the tile is a character
				CharacterClass cc = tc.entity.GetComponent<CharacterClass>();
				if(cc != null && tc.entity.tag == "Player"){
					cc.Die();
				}
			}
		}
	}

	public void AttackTile(int x, int y){
		AttackTile(GetTileAtCoordinate(x,y));
	}

	// Create a wall
	// (x, y) is the coordinate of the tile
	// direction is the direction from the tile where the wall should be created
	public void CreateWall(int x, int y, int direction) {
		TileClass tile = GetTileAtCoordinate (x, y).GetComponent<TileClass>();
		TileClass otherTile;
		ObjectStore objStore = Camera.main.GetComponent<ObjectStore>();
		GameObject wall;
		float wallX = x;
		float wallY = y;

		switch (direction) {
		case TileClass.NORTH:
			otherTile = GetTileAtCoordinate (x, y+1).GetComponent<TileClass>();
			if (!tile.HasWall (TileClass.NORTH) && !otherTile.HasWall (TileClass.SOUTH)) {
				wallY += 0.5f;
				wall = objStore.CreateHorizontalWall(wallX, wallY);
				tile.SetWall (TileClass.NORTH, wall);
				otherTile.SetWall (TileClass.SOUTH, wall);
			}
			break;
		case TileClass.EAST:
			otherTile = GetTileAtCoordinate (x+1, y).GetComponent<TileClass>();
			if (!tile.HasWall (TileClass.EAST) && !otherTile.HasWall (TileClass.WEST)) {
				wallX -= 0.5f;
				wall = objStore.CreateHorizontalWall(wallX, wallY);
				tile.SetWall (TileClass.EAST, wall);
				otherTile.SetWall (TileClass.WEST, wall);
			}
			break;
		case TileClass.SOUTH:
			otherTile = GetTileAtCoordinate (x, y-1).GetComponent<TileClass>();
			if (!tile.HasWall (TileClass.SOUTH) && !otherTile.HasWall (TileClass.NORTH)) {
				wallY -= 0.5f;
				wall = objStore.CreateHorizontalWall(wallX, wallY);
				tile.SetWall (TileClass.SOUTH, wall);
				otherTile.SetWall (TileClass.NORTH, wall);
			}
			break;
		case TileClass.WEST:
			otherTile = GetTileAtCoordinate (x-1, y).GetComponent<TileClass>();
			if (!tile.HasWall (TileClass.WEST) && !otherTile.HasWall (TileClass.EAST)) {
				wallX += 0.5f;
				wall = objStore.CreateHorizontalWall(wallX, wallY);
				tile.SetWall (TileClass.WEST, wall);
				otherTile.SetWall (TileClass.EAST, wall);
			}
			break;
		}
	}

	// Create a wall
	// (x, y) is the coordinate of the tile
	// direction is the direction from the tile where the wall should be destroyed
	public void destroyWall(int x, int y, int direction) {
		TileClass tile = GetTileAtCoordinate (x, y).GetComponent<TileClass>();
		TileClass otherTile = GetNeighbouringTile (x, y, direction).GetComponent<TileClass>();
		GameObject wall = null;

		int opposite = TileClass.getOppositeDirection (direction);
		if (otherTile.HasWall (opposite)) {
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

	public GameObject GetTileAtCoordinate(int x, int y){
		if (x < 0 || y < 0 || x >= width || y >= height) {
			return null;
		}
		return tileArray [x, y];
	}
	public GameObject GetTileAtCoordinate(float x, float y){
		return GetTileAtCoordinate ((int)x, (int)y);
	}
}
