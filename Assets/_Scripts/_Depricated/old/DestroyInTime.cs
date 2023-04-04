using UnityEngine;
using System.Collections;

public class DestroyInTime : MonoBehaviour {

    public float time = 3.0f;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, time);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
