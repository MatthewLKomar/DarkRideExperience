using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public int maxTime = 180;       // in seconds
    [Tooltip( "set to false when not in the cave")]
    public bool useFloor;           // set to false when not in the Cave
    [Header("Don't change these")]
    public TriggerControl tc;
    public Text timerText;
    public CameraMovement2 move;
    public CodeManager codeManager;
    public Text portTextY;
    public Text portTextA;
    public Text starboardTextY;
    public Text starboardTextA;

    private int timer = -1;
    private bool started = false;
    private bool tutorial = false;
    

    // Use this for initialization
    void Start () {
        // setup UI
        timerText.text = "Time: " + timer.ToString();
        ClearStarboardText();
        ClearPortText();
        move.SetUseFloor(useFloor);
        // delay to give everything time to run its start functions and then send the trigger to begin
        Invoke("Begin", 2.0f);
    }
	
	// Update is called once per frame
	void Update () {
        // test a trigger
        if (Input.GetKeyDown(KeyCode.Q))
		{
            //FoundSomething("mem1");   // trigger to test
        }

        // KEYBOARD TESTING FOR BUTTONS (RCC)
        if (Input.GetKeyDown(KeyCode.G)) ButtonHit("P1-A");
        if (Input.GetKeyDown(KeyCode.T)) ButtonHit("P1-Y");
        if (Input.GetKeyDown(KeyCode.F)) ButtonHit("P2-A");
        if (Input.GetKeyDown(KeyCode.R)) ButtonHit("P2-Y");

    }

    void Begin()
    {
        // called by start funtion after a delay - send the begin trigger
        FoundSomething("Control-Begin");
    }

    public void StartTime()
    {
        if (timer == -1)    // RCC - don't start the timer twice
        {
            Debug.Log("timer started");
            timer = maxTime;
            StartCoroutine("Timer");
        }
    }

    public void AddTime()
    {
        timer += 30;
    }

    IEnumerator Timer()
    {
        while (timer > 0)
        {
            yield return new WaitForSeconds(1f);
            // dec time
            timer -= 1;
            // update timer UI
            timerText.text = "Time: " + timer.ToString();
        }
        // times up
        // FAILURE - 
        Debug.Log("times up");
        tc.TriggerEvent("Trigger - Times Up");
        yield return null;
    }

    public void StartTutorial()
    {
        // called from GameController - allow button to play tones freely
        tutorial = true;
    }

    public void DoneTutorial()
    {
        // called from GameController 
        tutorial = false;
    }

    public void FoundSomething(string name)
    {
        Debug.Log("GC- " + name);
        switch (name)
        {
            case "Control-Begin":
                tc.TriggerEvent("Trigger #0 - Begin");
                break;
            case "Control-Start":
                // UI Start Button has been pressed
                started = true;
                // don't tell Trigger because it already knows
                break;
            case "Trigger-StartTimer":
                tc.TriggerEvent("Trigger #2 - tutorial done");
                break;
            case "Trigger-First":
                tc.TriggerEvent("Trigger #3 - hit first trigger");
                break;
            case "Waypoints-All Done":
                tc.TriggerEvent("Trigger - All Done");
                break;

        }
    }

    public void ButtonHit(string name)
    {
        // a joystick button has been hit
        Debug.Log("GC- Joy Hit: " + name);
        switch (name)
        {
            case "P1-A":
                // help the tour guide to know this is the P1 stick
                if (!started)
                {
                    starboardTextA.text = "Starboard A Clicked";
                    Invoke("ClearStarboardText", 0.5f); print("Here");
                }
                else codeManager.CheckTone(0);
                break;
            case "P1-B":
                // do something

                break;
            case "P1-Y":
                if (!started)
                {
                    starboardTextY.text = "Starboard Controller : Y Clicked";
                    Invoke("ClearStarboardText", 0.5f);
                }
                // do something
                else codeManager.CheckTone(1);
                break;
            case "P1-X":
                // do something

                break;
            case "P1-LeftBumper":
                // do something

                break;
            case "P1-RightBumper":
                // do something

                break;
            case "P1-Back":
                // do something

                break;
            case "P1-Start":
                // do something

                break;
            case "P1-LeftStick":
                // do something

                break;
            case "P1-RightStick":
                // do something

                break;

            case "P2-A":
                // help the tour guide to know this is the P1 stick
                if (!started)
                {
                    portTextA.text = "Port Controller: A Clicked";
                    Invoke("ClearPortText", 0.5f);
                }
                else codeManager.CheckTone(2);
                break;
            case "P2-B":
                // do something

                break;
            case "P2-Y":
                if (!started)
                {
                    portTextY.text = "Port Controller: Y Clicked ";
                    Invoke("ClearStarboardText", 0.5f);
                }
                // do something
                else codeManager.CheckTone(3);
                break;
            case "P2-X":
                // do something

                break;
            case "P2-LeftBumper":
                // do something

                break;
            case "P2-RightBumper":
                // do something

                break;
            case "P2-Back":
                // do something

                break;
            case "P2-Start":
                // do something

                break;
            case "P2-LeftStick":
                // do something

                break;
            case "P2-RightStick":
                // do something

                break;

        }
    }

    void ClearStarboardText()
    {
        // help the facilatator tell what joystick is which
        starboardTextA.text = "Starboard Controller - ";
        starboardTextY.text = "Starboard Controller - ";

    }
    void ClearPortText()
    {
        // help the facilatator tell what joystick is which
        portTextA.text = "Port Controller - ";
        portTextY.text = "Port Controller - ";

    }

}
