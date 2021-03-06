﻿using UnityEngine;
using System.Collections;

public class CharacterClass : MonoBehaviour {
	
	private GameboardController gameboard;
	private Animator anim;
	TileClass currentTile;
	TileClass nextTile;
	float t = 0;
	private bool isDead = false;
	public AudioClip deathSound;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		gameboard = GameObject.FindGameObjectWithTag("Gameboard").GetComponent<GameboardController>();
		// Set the reference of the tile to the entity on top
		GameObject tile = gameboard.GetTileAtCoordinate(transform.position.x, transform.position.y);
		if(tile != null){
			tile.GetComponent<TileClass>().entity = gameObject;
		}
		TileClass tc = gameboard.GetTileAtCoordinate(transform.position.x,transform.position.y).GetComponent<TileClass>();
		if(tc!=null){
			currentTile = tc;
			nextTile = tc;
		}
		// Set order in layer
		renderer.sortingOrder = (int) transform.position.y * -2;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector2.Lerp(currentTile.transform.position, nextTile.transform.position, t);
		t+=Time.deltaTime;
	}

	public void Move(int direction) {
		if (isDead) {
			// no zombies plz
			return;
		}

		t=0;
		int x = (int) transform.position.x;
		int y = (int) transform.position.y;
		anim.SetTrigger("Walk");
		switch(direction) {
		case TileClass.NORTH:
			y++;
			break;
		case TileClass.EAST:
			transform.localScale = new Vector3(1,1,1);
			x++;
			break;
		case TileClass.SOUTH:
			y--;
			break;
		case TileClass.WEST:
			transform.localScale = new Vector3(-1,1,1);
			x--;
			break;
		}

		// Reset the tiles to current position
		TileClass tc = gameboard.GetTileAtCoordinate(transform.position.x,transform.position.y).GetComponent<TileClass>();
		if(tc!=null){
			currentTile = tc;
			nextTile = tc;
		}

		GameObject obj = gameboard.GetTileAtCoordinate(transform.position.x, transform.position.y);
		TileClass tempCurrentTile = null;
		if (obj != null) {
			tempCurrentTile = obj.GetComponent<TileClass>();
		}
		obj = gameboard.GetTileAtCoordinate(x, y);
		TileClass tempNextTile = null;
		if (obj != null) {
			tempNextTile = obj.GetComponent<TileClass>();
		}
		
		if (tempCurrentTile != null && !tempCurrentTile.HasWall (direction) && tempNextTile != null && !tempNextTile.HasEntity()) {
			currentTile = tempCurrentTile;
			nextTile = tempNextTile;
			currentTile.entity = null;
			nextTile.entity = gameObject;
			// Set order in layer
			renderer.sortingOrder = (int) nextTile.transform.position.y * -2;
		}
	}

	public void Die() {
		anim.SetTrigger("Death");
		audio.PlayOneShot (deathSound);
		isDead = true;
		gameboard.GetTileAtCoordinate(transform.position.x, transform.position.y).GetComponent<TileClass>().entity = null;
		nextTile.entity = null;
		gameboard.characterDied();
		renderer.sortingOrder = -20;

		if(tag == "P1"){
			gameboard.player1 = null;
		}else if(tag == "P2"){
			gameboard.player2 = null;
		}

		tag = "Untagged";
		foreach(MonoBehaviour mb in GetComponents<MonoBehaviour>()){
			Destroy(mb);
		}
	}
}
