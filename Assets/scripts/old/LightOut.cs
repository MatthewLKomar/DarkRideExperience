using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOut : MonoBehaviour {
    public GameObject [] torch;

	// Use this for initialization
	void Start () {
		
	}

    public void FoundSomething(string name)
    {
        for (int i = 0; i < torch.Length; i++)
        {
            torch[i].SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.K))
        {
            for (int i = 0; i < torch.Length; i++)
            {
                torch[i].SetActive(false);
            }
                
        }	
	}
}
