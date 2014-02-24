using UnityEngine;
using System.Collections;

public class AudioTrackSequence : MonoBehaviour {

	public AudioSource audioStart;
	public AudioSource audioLoop;
	public float trackTransitionTime = -.15f;

	// Use this for initialization
	void Start () {
		AudioSource[] tracks = GetComponents<AudioSource>();
		audioStart = tracks[0];
		audioLoop = tracks[1];
		StartCoroutine(MusicSequence());
	}

	IEnumerator MusicSequence(){
		audioStart.Play();
		yield return new WaitForSeconds(audioStart.clip.length + trackTransitionTime);
		print ("loop");
		audioLoop.Play();
	}
}
