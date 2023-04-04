using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayVideo : MonoBehaviour {

	public long startFrame = 0;

	private UnityEngine.Video.VideoPlayer vp;
	private AudioSource audio;

	// Use this for initialization
	void Start () {
		vp = GetComponent<UnityEngine.Video.VideoPlayer> ();
		audio = vp.GetTargetAudioSource (0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void FoundSomething(string name) {
		Play();
	}

	public void Play()
	{
		vp.frame = startFrame;
		vp.time = (double)startFrame / 30f;
		//audio.time = startFrame / 30f;
		Debug.Log("Starting Video");
		vp.Play();
	}

	public double GetLength()
	{
		return (vp.clip.length);
	}
}
