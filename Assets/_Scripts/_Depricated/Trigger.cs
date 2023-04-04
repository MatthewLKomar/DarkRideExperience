using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {

	public GameObject hitObj;
	public GameObject notObj;

	public bool destroyThis = false;
	public bool destroyHit = false;
	public bool useKey = false;
	public KeyCode key; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (useKey) {
			if (Input.GetKeyDown(key)) {
				HandleTrigger(null);
			}
		}
	}

	void OnTriggerEnter (Collider other) {

		if (other.gameObject == hitObj) {
			print("Found it");
			HandleTrigger(other.gameObject);
		}


	}

	void HandleTrigger(GameObject other) {
		notObj.SendMessage("FoundSomething", this.name);
        Debug.Log("In HandleTrigger");

		if (destroyHit && other != null) { Destroy(other); }
		if (destroyThis) { Destroy(this.gameObject,20); } 
	}

}
