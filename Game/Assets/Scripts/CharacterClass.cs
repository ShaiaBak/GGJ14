using UnityEngine;
using System.Collections;

public class CharacterClass : MonoBehaviour {
	
	private GameboardController gameboard;
	private Animator anim;
	TileClass currentTile;
	TileClass nextTile;
	float t = 0;
	private bool isDead = false;
	private float endTimer = 3f;
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
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector2.Lerp(currentTile.transform.position, nextTile.transform.position, t);
		t+=Time.deltaTime;

		if (isDead) {
			endTimer -= Time.deltaTime;
			if (endTimer < 0) {
				EndGame ();
			}
		}
	}

	public void Move(int direction) {
		if (isDead) {
			// no zombies plz
			return;
		}

		t=0;
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
		}
	}

	public void Die() {
		anim.SetTrigger("Death");
		audio.PlayOneShot (deathSound);
		Debug.Log (tag + " Died");
		isDead = true;
		gameboard.GetTileAtCoordinate(transform.position.x, transform.position.y).GetComponent<TileClass>().entity = null;
//		Destroy (gameObject);
	}

	private void EndGame() {
		if(tag == "P1"){
			Application.LoadLevel (4);
		}else if(tag == "P2"){
			Application.LoadLevel (3);
		}
		tag = "Untagged";
		foreach(MonoBehaviour mb in GetComponents<MonoBehaviour>()){
			Destroy(mb);
		}
	}
}
