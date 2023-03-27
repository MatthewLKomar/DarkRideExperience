using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSFX : MonoBehaviour {

	public AudioClip sfx;
	public GameObject hitObj;
	public AudioSource audio;
	public bool destroyThis = false;
    public bool destroyHit = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider other) {

		if (other.gameObject == hitObj) {
			print("Found - " + hitObj.name);
			HandleTrigger(other.gameObject);
		}
	}

	void HandleTrigger(GameObject other) {

		if (!audio.isPlaying) {
			audio.clip = sfx;
			audio.Play ();

            if (destroyHit && other != null) { Destroy(other); }
            if (destroyThis) { Destroy(this.gameObject, 20); }
        }
	}

}
