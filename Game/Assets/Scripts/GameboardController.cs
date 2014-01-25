using UnityEngine;
using System.Collections;

public class GameboardController : MonoBehaviour {

	private GameObject[,] tileArray;

	// Use this for initialization
	void Start () {
		tileArray = new GameObject[1000,1000];
		GameObject[] array = GameObject.FindGameObjectsWithTag("Tile");
		foreach(GameObject o in array){
			int x = (int) o.transform.localPosition.x;
			int z = (int) o.transform.localPosition.z;
			tileArray[x,z] = o;
		}

		array = GameObject.FindGameObjectsWithTag ("Wall");
		foreach(GameObject o in array){
			float x = o.transform.localPosition.x;
			float z = o.transform.localPosition.z;

			if (Mathf.FloorToInt(x) == Mathf.CeilToInt(x)){
				// is a horizontal wall
				int zdn = Mathf.FloorToInt(z);
				int zup = Mathf.CeilToInt(z);
				if (zdn >= 0) {
//					print ("Tile (" + (int)x + "," + zdn + ") add north wall");
					tileArray[(int) x, zdn].GetComponent<TileClass>().SetWall(TileClass.NORTH, o);
				}
				if (zup < 1000) {
//					print ("Tile (" + (int)x + "," + zup + ") add south wall");
					tileArray[(int) x, zup].GetComponent<TileClass>().SetWall(TileClass.SOUTH, o);
				}
			}
			else if (Mathf.FloorToInt(z) == Mathf.CeilToInt(z)){
				// is a vertical wall
				int xdn = Mathf.FloorToInt(x);
				int xup = Mathf.CeilToInt(x);
				if (xdn >= 0) {
//					print ("Tile (" + xdn + "," + (int)z + ") add east wall");
					tileArray[xdn, (int) z].GetComponent<TileClass>().SetWall(TileClass.EAST, o);
				}
				if (xup < 1000) {
//					print ("Tile (" + xup + "," + (int)z + ") add west wall");
					tileArray[xup, (int) z].GetComponent<TileClass>().SetWall(TileClass.WEST, o);
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
}
