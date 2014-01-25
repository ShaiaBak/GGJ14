using UnityEngine;
using System.Collections;

public class TileClass : MonoBehaviour {

	public GameObject entity;
	public GameObject[] walls;

	public const int NORTH = 0, EAST = 1, SOUTH = 2, WEST = 3;

	public int getOppositeDirection(int direction) {
		return (direction + 2) % 4;
	}

	// Use this for initialization
	void Awake () {
		walls = new GameObject[4];
	}

	public void SetWall(int direction, GameObject wall){
		walls[direction] = wall;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool HasWall(int direction) {
		return walls [direction] != null;
	}

	public bool HasEntity() {
		return false;
	}
}
