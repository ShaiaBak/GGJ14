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
		if(Input.GetButtonDown("P1_Choose2")){	// up

			transform.position = new Vector2(transform.position.x, transform.position.y + 1);
		}else if(Input.GetButtonDown("P1_Choose4")){	// left
			transform.position = new Vector2(transform.position.x - 1, transform.position.y);
		}else if(Input.GetButtonDown("P1_Choose5")){	// down
			transform.position = new Vector2(transform.position.x, transform.position.y - 1);
		}else if(Input.GetButtonDown("P1_Choose6")){	// right
			transform.position = new Vector3(transform.position.x + 1,transform.position.y);
		}

	}
}
