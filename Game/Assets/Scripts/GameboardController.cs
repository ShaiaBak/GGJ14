using UnityEngine;
using System.Collections;

public class GameboardController : MonoBehaviour {

	private GameObject[,] tileArray;
	private int width;
	private int height;

	// Use this for initialization
	void Start () {
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
				if (yup < 1000) {
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
				if (xup < 1000) {
//					print ("Tile (" + xup + "," + (int)y + ") add west wall");
					tileArray[xup, (int) y].GetComponent<TileClass>().SetWall(TileClass.WEST, o);
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void moveCharacter(GameObject player, int direction) {
		player.GetComponent<CharacterClass>().Move(direction);
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
