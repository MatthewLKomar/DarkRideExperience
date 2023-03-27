using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : MonoBehaviour {

	public GameObject chargeObj;
	public float delayTime;
	public float time;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void FoundSomething(string name) {
		Invoke ("ChargeAhead", delayTime);
	}

	void ChargeAhead() {
		//iTween.MoveTo (this.gameObject, chargeObj.transform.position, time);
	}
}
