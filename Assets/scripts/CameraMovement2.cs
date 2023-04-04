using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement2 : MonoBehaviour {

    public AudioClip moveSFX;
    public AudioClip noMoveSFX;
    [Header("Don't change these")]
    public FloorController floor;
    public GameObject node;         // camera rig node
    public TriggerControl triggerControl;
    public AudioSource audio;

    private bool useFloor = false;   // rcc - set by GameController

    [Header("For testing only")]
    [SerializeField] private bool p1Enabled = false;
    [SerializeField] private bool p2Enabled = false;
    [SerializeField] private bool canTurn = true;
    private bool canRise = false;
    private bool canFall = false;
    [SerializeField] private float left;
    [SerializeField] private float right;
    [SerializeField] private float forward = 0.0f;

    private bool rightfirst = false;
    private bool leftfirst = false;
    private bool risefirst = false;
    private bool fallfirst = false;
    private bool rise = false;   
    private bool fall = false;   
    private float up = 0.0f;
    private float turn = 0.0f;
    private float forwardThresh = 0.3f;
    private float upwardSpeed = 0.1f;  // rcc - rate of verticle movement
    private bool started = false;
    private string floorStatus = "down";
    private float subHighCutoff = 15.0f;

    // Use this for initialization
    void Start() {

        InvokeRepeating("Position", 0.1f, 0.1f);

        // just test how many joystick are seen
        string[] joys = Input.GetJoystickNames();
        foreach (string j in joys)
        {
            print(j);
        }
  
    }

    public void StartButton() {
        // this is called by TriggerControl when the start button on the puppet screen is clicked by the tour guide
        if (!started) {
            started = true;
            if (useFloor) SetupFloor();
        }
    }


    // Update is called once per frame
    void Update() {

        //Debug.Log(-Input.GetAxisRaw("P1-VerticalLeft") + "   " + -Input.GetAxisRaw("P2-VerticalLeft"));
        //Debug.Log(-Input.GetAxis("P1-VerticalLeft") + "   " + -Input.GetAxis("P2-VerticalLeft"));

        // check for joystick or keyboard input => store the input 
        if (p1Enabled == true) 
        {
            right = -Input.GetAxis("P1-VerticalLeft");
            if (Input.GetKey(KeyCode.UpArrow)) right = 1.0f;        // rcc - add keyboard control for testing
            if (Input.GetKeyUp(KeyCode.UpArrow)) right = 0.0f;      // rcc - add keyboard control for testing
            if (Input.GetKey(KeyCode.DownArrow)) right = -1.0f;     // rcc - add keyboard control for testing
            if (Input.GetKeyUp(KeyCode.DownArrow)) right = 0.0f;    // rcc - add keyboard control for testing
        }
        if (p2Enabled == true) 
        {
            left = -Input.GetAxis("P2-VerticalLeft");
            if (Input.GetKey(KeyCode.W)) left = 1.0f;               // rcc - add keyboard control for testing
            if (Input.GetKeyUp(KeyCode.W)) left = 0.0f;             // rcc - add keyboard control for testing
            if (Input.GetKey(KeyCode.S)) left = -1.0f;              // rcc - add keyboard control for testing
            if (Input.GetKeyUp(KeyCode.S)) left = 0.0f;             // rcc - add keyboard control for testing
        }

        /// RCC - commented out because we nolonger use vertical movment
        /*if (canRise == true) {
            rise = Input.GetButton("P1-A"); // rcc - changed to bool since Input returns bool
            if (Input.GetKey(KeyCode.UpArrow)) rise = true; // rcc - add keyboard control for testing
        }
        if (canFall == true) {
            fall = Input.GetButton("P2-A"); // rcc - changed to bool since Input returns bool
            if (Input.GetKey(KeyCode.S)) fall = true; // rcc - add keyboard control for testing
        }*/

        if(p1Enabled == true && p2Enabled == true)
        {
            // update the position of the node
            node.transform.Translate(Vector3.forward * forward * Time.deltaTime * 3.0f);
        }

        if (canTurn)
        {
            ///node.transform.Translate(Vector3.up * up * Time.deltaTime * 15.0f);     // rcc - add in rise and fall
            node.transform.Rotate(Vector3.up, turn * Time.deltaTime * 25.0f);
        }
    }

    void Position()
    {
        // how far off are the sticks from each other
        float dif = left - right;
        // move forward if sticks are within a threshold of each other
        if (Mathf.Abs(dif) <= forwardThresh)
        {
            forward = left;
            turn = 0.0f;
        }
        // else turn 
        else
        {
            forward = 0.0f;
            turn = dif;
        }
        // rcc - add in upward movement
        up = 0.0f;
        if (forward > 0.0f || turn != 0.0f) {
            // moving so play move sound
            if (audio.clip != moveSFX) {
                audio.clip = moveSFX;
                audio.Play();
            }
        } else if (audio.clip != noMoveSFX) {
            // not moving so play not moving sound
            audio.clip = noMoveSFX;
            audio.Play();
        }

        RepositionFloor();
    }

    public void RepositionFloor() {
        if (!useFloor) return;
        
        // rcc - position the floor
        if (turn == 0.0f && floorStatus != "down") {
            // moving so drop the floor
            floorStatus = "down";
            floor.moveAll(5.0f);
        } else if (turn < 0.0f && floorStatus != "right" && p1Enabled && p2Enabled) {
            // turning right so tilt the floor
            floor.raiseRight(5.0f);
            floorStatus = "right";
        } else if (turn > 0.0f && floorStatus != "left" && p1Enabled && p2Enabled) {
            // turning left so tilt the floor
            floor.raiseLeft(5.0f);
            floorStatus = "left";
        }
    }


    public void StopMovement()
    {
        // called by TriggerControl to stop the player 
        print("STOPPING MOVEMENT");
        p1Enabled = false;
        p2Enabled = false;
        right = 0;
        left = 0;
        if (useFloor) floor.moveAll(5.0f);
    }

    public void StartMovement()
    {
        // called by TriggerControl to start the player 
        print("STARTING MOVEMENT");
        //Allow turning
        canTurn = true;
        p1Enabled = true;
        p2Enabled = true;
        right = 0;
        left = 0;
        if (useFloor) floor.moveAll(5.0f);
    }

    void SetupFloor()
    {
        if (useFloor)
        {
            floor.SetupFloor();
            floor.enable();
        }
        floorStatus = "down";
    }

    public void SetUseFloor(bool value) {
        // called by GameController
        useFloor = value;
    }

    // Parse input from Controller
    public void ButtonHit(string name) {
        
        switch (name) {
            case "P1-A":
                // vertical movement - RCC no longer using
                ///if (canRise) rise = true;
                break;
            case "P1-B":
                // do something
                break;
            case "P1-Y":
                // do something
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
                // vertical movement - RCC no longer using
                break;
            case "P2-B":
                // do something
                break;
            case "P2-Y":
                // do something
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

    // **********************************************************************************
    IEnumerator Delay(float delay)
    {
        // wait if needed
        print("WAIT");
        yield return new WaitForSeconds(delay);
    }

    public void SinusoidMovement()
    {
        // called by TriggerControl to start the player 
        print("SINUSOID MOVEMENT");
        //Allow turning

        canTurn = true;
        p1Enabled = true;
        p2Enabled = true;
        right = 0;
        left = 0;

        float front_voltage = 5.0f;
        float back_voltage = 5.0f;
        while (front_voltage > 0.0f)
        {
            print(front_voltage);
            if (useFloor) floor.SinFloor(front_voltage--, back_voltage++);
            Delay(5.0f);
        }
        while (back_voltage > 0.0f)
        {
            print(front_voltage);
            if (useFloor) floor.SinFloor(front_voltage++, back_voltage--);
            Delay(5.0f);
        }
        while (front_voltage > 0.0f)
        {
            print(front_voltage);
            if (useFloor) floor.SinFloor(front_voltage--, back_voltage++);
            Delay(5.0f);
        }
    }
}
