using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMove : MonoBehaviour {

    public Transform moveNode;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // MOVE THE object
        if (Input.GetKey(KeyCode.W))
        {
            //camNode.position = new Vector3(camNode.position.x + .01f,0f,0f);
            moveNode.Translate(Vector3.forward * 0.1f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            //camNode.position = new Vector3(camNode.position.x - .01f, 0f, 0f);
            moveNode.Translate(Vector3.forward * -0.1f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            //camNode.position = new Vector3(camNode.position.x + .01f,0f,0f);
            //camNode.Translate(Vector3.up * 0.1f);
            moveNode.Rotate(Vector3.up * -0.5f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            //camNode.position = new Vector3(camNode.position.x - .01f, 0f, 0f);
            //camNode.Translate(Vector3.up * -0.1f);
            moveNode.Rotate(Vector3.up * 0.5f);
        }

        if (Input.GetKey(KeyCode.R))
        {
            //camNode.position = new Vector3(camNode.position.x + .01f,0f,0f);
            moveNode.Translate(Vector3.up * 0.1f);
        }
        if (Input.GetKey(KeyCode.F))
        {
            //camNode.position = new Vector3(camNode.position.x - .01f, 0f, 0f);
            moveNode.Translate(Vector3.up * -0.1f);
        }
    }
}
