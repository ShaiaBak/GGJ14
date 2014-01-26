﻿using UnityEngine;
using System.Collections;

public class CharacterClass : MonoBehaviour {

	[SerializeField]
	private GameboardController gameboard;

	// Use this for initialization
	void Start () {
		gameboard = GameObject.FindGameObjectWithTag("Gameboard").GetComponent<GameboardController>();
		// Set the reference of the tile to the entity on top
		GameObject tile = gameboard.GetTileAtCoordinate(transform.position.x, transform.position.y);
		if(tile != null){
			tile.GetComponent<TileClass>().entity = gameObject;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("P1_Choose2")){	// up
			Move (TileClass.NORTH);
		}else if(Input.GetButtonDown("P1_Choose4")){	// left
			Move (TileClass.WEST);
		}else if(Input.GetButtonDown("P1_Choose5")){	// down
			Move (TileClass.SOUTH);
		}else if(Input.GetButtonDown("P1_Choose6")){	// right
			Move (TileClass.EAST);
		}
	}

	public void Move(int direction) {
		int x = (int) transform.position.x;
		int y = (int) transform.position.y;

		switch(direction) {
		case TileClass.NORTH:
			y++;
			break;
		case TileClass.EAST:
			x++;
			break;
		case TileClass.SOUTH:
			y--;
			break;
		case TileClass.WEST:
			x--;
			break;
		}

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
		
		if (currentTile != null && !currentTile.HasWall (direction) && nextTile != null && !nextTile.HasEntity()) {
			transform.position = new Vector2 (x,y);
			currentTile.entity = null;
			nextTile.entity = gameObject;
		}
	}

	public void Die() {
		//Animator animator = GetComponent<Animator>();
		//animator.SetTrigger("Die");

		Destroy (gameObject);
	}
}
