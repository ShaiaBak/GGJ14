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

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
}
