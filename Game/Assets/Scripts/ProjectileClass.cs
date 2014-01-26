using UnityEngine;
using System.Collections;

public class ProjectileClass : MonoBehaviour {

	public Vector2 targetLocation;
	public Vector2 originalLocation;
	public float timer = 0;

	// Use this for initialization
	void Awake () {
		originalLocation = transform.position;
		targetLocation = transform.position;
		Destroy(gameObject, 2f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector2.Lerp(originalLocation, targetLocation, timer);
		timer += Time.deltaTime * 5f / Vector2.Distance(originalLocation, targetLocation); ;
	}

	public void SetTargetLocation(Vector2 original, Vector2 target){
		originalLocation = original;
		targetLocation = target;
		timer = 0;
	}
}
