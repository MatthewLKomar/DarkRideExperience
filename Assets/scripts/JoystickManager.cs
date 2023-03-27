using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoystickManager : MonoBehaviour
{

	public GameObject[] notifyObj;
	public bool trackButtonDown = true;
	public bool trackButtonHeld = false;
	public bool trackButtonRelease = false;

	private string s;

	void Update() {
		// check for button events 
		if (trackButtonDown) {
			if (Input.GetButtonDown("P1-A")) { Send("ButtonHit", "P1-A"); }
			if (Input.GetButtonDown("P1-B")) { Send("ButtonHit", "P1-B"); }
			if (Input.GetButtonDown("P1-Y")) { Send("ButtonHit", "P1-Y"); }
			if (Input.GetButtonDown("P1-X")) { Send("ButtonHit", "P1-X"); }
			if (Input.GetButtonDown("P1-LeftBumper")) { Send("ButtonHit", "P1-LeftBumper"); }
			if (Input.GetButtonDown("P1-RightBumper")) { Send("ButtonHit", "P1-RightBumper"); }
			if (Input.GetButtonDown("P1-Back")) { Send("ButtonHit", "P1-Back"); }
			if (Input.GetButtonDown("P1-Start")) { Send("ButtonHit", "P1-Start"); }
			if (Input.GetButtonDown("P1-LeftStick")) { Send("ButtonHit", "P1-LeftStick"); }
			if (Input.GetButtonDown("P1-RightStick")) { Send("ButtonHit", "P1-RightStick"); }

			if (Input.GetButtonDown("P2-A")) { Send("ButtonHit", "P2-A"); }
			if (Input.GetButtonDown("P2-B")) { Send("ButtonHit", "P2-B"); }
			if (Input.GetButtonDown("P2-Y")) { Send("ButtonHit", "P2-Y"); }
			if (Input.GetButtonDown("P2-X")) { Send("ButtonHit",  "P2-X"); }
			if (Input.GetButtonDown("P2-LeftBumper")) { Send("ButtonHit", "P2-LeftBumper"); }
			if (Input.GetButtonDown("P2-RightBumper")) { Send("ButtonHit", "P2-RightBumper"); }
			if (Input.GetButtonDown("P2-Back")) { Send("ButtonHit", "P2-Back"); }
			if (Input.GetButtonDown("P2-Start")) { Send("ButtonHit", "P2-Start"); }
			if (Input.GetButtonDown("P2-LeftStick")) { Send("ButtonHit", "P2-LeftStick"); }
			if (Input.GetButtonDown("P2-RightStick")) { Send("ButtonHit", "P2-RightStick"); }
		}

		if (trackButtonHeld) {
			if (Input.GetButton("P1-A")) { Send("ButtonHeld", "P1-A"); }
			if (Input.GetButton("P1-B")) { Send("ButtonHeld", "P1-B"); }
			if (Input.GetButton("P1-Y")) { Send("ButtonHeld", "P1-Y"); }
			if (Input.GetButton("P1-X")) { Send("ButtonHeld", "P1-X"); }
			if (Input.GetButton("P1-LeftBumper")) { Send("ButtonHeld", "P1-LeftBumper"); }
			if (Input.GetButton("P1-RightBumper")) { Send("ButtonHeld", "P1-RightBumper"); }
			if (Input.GetButton("P1-Back")) { Send("ButtonHeld", "P1-Back"); }
			if (Input.GetButton("P1-Start")) { Send("ButtonHeld", "P1-Start"); }
			if (Input.GetButton("P1-LeftStick")) { Send("ButtonHeld", "P1-LeftStick"); }
			if (Input.GetButton("P1-RightStick")) { Send("ButtonHeld", "P1-RightStick"); }

			if (Input.GetButton("P2-A")) { Send("ButtonHeld", "P2-A"); }
			if (Input.GetButton("P2-B")) { Send("ButtonHeld", "P2-B"); }
			if (Input.GetButton("P2-Y")) { Send("ButtonHeld", "P2-Y"); }
			if (Input.GetButton("P2-X")) { Send("ButtonHeld", "P2-X"); }
			if (Input.GetButton("P2-LeftBumper")) { Send("ButtonHeld", "P2-LeftBumper"); }
			if (Input.GetButton("P2-RightBumper")) { Send("ButtonHeld", "P2-RightBumper"); }
			if (Input.GetButton("P2-Back")) { Send("ButtonHeld", "P2-Back"); }
			if (Input.GetButton("P2-Start")) { Send("ButtonHeld", "P2-Start"); }
			if (Input.GetButton("P2-LeftStick")) { Send("ButtonHeld", "P2-LeftStick"); }
			if (Input.GetButton("P2-RightStick")) { Send("ButtonHeld", "P2-RightStick"); }
		}

		if (trackButtonRelease) {
			if (Input.GetButtonUp("P1-A")) { Send("ButtonReleased", "P1-A"); }
			if (Input.GetButtonUp("P1-B")) { Send("ButtonReleased", "P1-B"); }
			if (Input.GetButtonUp("P1-Y")) { Send("ButtonReleased", "P1-Y"); }
			if (Input.GetButtonUp("P1-X")) { Send("ButtonReleased", "P1-X"); }
			if (Input.GetButtonUp("P1-LeftBumper")) { Send("ButtonReleased", "P1-LeftBumper"); }
			if (Input.GetButtonUp("P1-RightBumper")) { Send("ButtonReleased", "P1-RightBumper"); }
			if (Input.GetButtonUp("P1-Back")) { Send("ButtonReleased", "P1-Back"); }
			if (Input.GetButtonUp("P1-Start")) { Send("ButtonReleased", "P1-Start"); }
			if (Input.GetButtonUp("P1-LeftStick")) { Send("ButtonReleased", "P1-LeftStick"); }
			if (Input.GetButtonUp("P1-RightStick")) { Send("ButtonReleased", "P1-RightStick"); }

			if (Input.GetButtonUp("P2-A")) { Send("ButtonReleased", "P2-A"); }
			if (Input.GetButtonUp("P2-B")) { Send("ButtonReleased", "P2-B"); }
			if (Input.GetButtonUp("P2-Y")) { Send("ButtonReleased", "P2-Y"); }
			if (Input.GetButtonUp("P2-X")) { Send("ButtonReleased", "P2-X"); }
			if (Input.GetButtonUp("P2-LeftBumper")) { Send("ButtonReleased", "P2-LeftBumper"); }
			if (Input.GetButtonUp("P2-RightBumper")) { Send("ButtonReleased", "P2-RightBumper"); }
			if (Input.GetButtonUp("P2-Back")) { Send("ButtonReleased", "P2-Back"); }
			if (Input.GetButtonUp("P2-Start")) { Send("ButtonReleased", "P2-Start"); }
			if (Input.GetButtonUp("P2-LeftStick")) { Send("ButtonReleased", "P2-LeftStick"); }
			if (Input.GetButtonUp("P2-RightStick")) { Send("ButtonReleased", "P2-RightStick"); }
		}
	}

	void Send(string function, string message)
	{
		for (int i = 0; i < notifyObj.Length; i++)
		{
			notifyObj[i].SendMessage(function, message);
		}
	}

	public float GetAxis(string name) {
		// return the value of the given axis
		return (Input.GetAxis(name));
	}

}
