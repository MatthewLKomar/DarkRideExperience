using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyLookUpDown : MonoBehaviour {

	public bool use = true;

	private Vector3 up;
	private Vector3 down;

	// Use this for initialization
	void Start () {
		up = new Vector3 (-5, 0, 0);
		down = new Vector3 (5, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		if (use) {
			if (Input.GetKeyDown(KeyCode.R)) {
				transform.Rotate(up);
			}
			if (Input.GetKeyDown(KeyCode.F)) {
				transform.Rotate(down);
			}
		}
	}
}
