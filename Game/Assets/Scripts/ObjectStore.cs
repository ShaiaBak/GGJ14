using UnityEngine;
using System.Collections;

public class ObjectStore : MonoBehaviour {

	public GameObject hWall;
	public GameObject vWall;
	public GameObject projectile;
	public AudioClip shootSound;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public GameObject CreateHorizontalWall(float x, float y) {
		return (GameObject) Instantiate (hWall, new Vector3 (x, y, 0), Quaternion.identity);
	}

	public GameObject CreateVerticalWall(float x, float y) {
		return (GameObject) Instantiate (vWall, new Vector3 (x, y, 0), Quaternion.identity);
	}
}
