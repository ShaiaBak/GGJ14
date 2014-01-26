using UnityEngine;
using System.Collections;

public class MainMenuMusic : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad(this.gameObject);
	}
}
