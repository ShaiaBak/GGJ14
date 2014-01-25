using UnityEngine;
using System.Collections;

public class CharacterClass : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("w")){
			transform.position = new Vector2(transform.position.x, transform.position.y + 1);
		}else if(Input.GetButtonDown("a")){
			transform.position = new Vector2(transform.position.x - 1, transform.position.y);
		}else if(Input.GetButtonDown("s")){
			transform.position = new Vector2(transform.position.x, transform.position.y - 1);
		}else if(Input.GetButtonDown("d")){
			transform.position = new Vector3(transform.position.x + 1,transform.position.y);
		}

	}
}
