using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Phidgets;
using Phidgets.Events;

public class FloorController : MonoBehaviour {

    // the motion floor uses four air bladders, in the corners of the floor, to raise it which are enumerated as follows:
    // 2 - front left		3 - front right
    // 0 - back left		1 - back right
    public static FloorController curr;

    private float deltaTime = .003f;
    private Analog motionFloor;
    private Vector4 currentPos;
    private Vector4 targetPos;
    private Vector4 dir;            // should be set to 0f = dont move; 1f = move up; -1f = move down
    private float maxVoltage = 10.0f;
    private float minVoltage = 0.0f;
    private float delta = 0.2f;


    // Use this for initialization
    void Start () {
        //SetupFloor();
    }

    public void SetupFloor() {
        ConnectToFloor();
        enable();
    }

    public void ConnectToFloor()
    {
        // attach the phidget that controls the floor
        motionFloor = new Analog();
        motionFloor.open();
        motionFloor.waitForAttachment(1000);
        // lower the floor at start - just in case a previous program left it up
        resetFloor();
    }

    private void Awake() {
        if (curr == null) {
            curr = this;
        }
        else { Destroy(gameObject); }
    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnDisable() {
        // drop the floor
        Debug.Log("resetting floor in OnDisable");
        resetFloor();
        motionFloor.close();    // if you don't close the phidget then this phidget hangs
    }

    public void enable() {
        // the air bladders have to be enabled before they can be used
        for (int i = 0; i < 4; i++) {
            motionFloor.outputs[i].Enabled = true;
        }
        // start updating the floor position
        InvokeRepeating("UpdateFloor", deltaTime, deltaTime);
    }

    public void disable() {
        // when done using the floor, and after it has been lowered, disable the bladders
        for (int i = 0; i < 4; i++) {
            motionFloor.outputs[i].Enabled = false;
        }
        CancelInvoke("UpdateFloor");
    }

    public void resetFloor() {
        // lowers the floor *** for safty, should be called at the start and end of all games ***
        enable();
        lowerFloor();
        disable();
    }

    public float getVoltage(int index) {
        // return the current voltage level of the provided air bladder
        return ((float)motionFloor.outputs[index].Voltage);
    }

    public void lowerFloor() {
        // lower the floor by setting all bladders voltages to zero - should be used at the start and end of games
        for (int i = 0; i < 4; i++) {
            motionFloor.outputs[i].Voltage = 0.0f;
        }
        currentPos = Vector4.zero;
        targetPos = Vector4.zero;
        dir = Vector4.zero;
    }

    public void moveOne(int index, float voltage) { 
        if (voltage > 10) voltage = 10.0f;
        // set the voltage (translates to floor height) of the provided bladder to the provided voltage
        // voltage = 0.0F is fully lowered
        // voltage = 5.0F is raised halfway
        // voltage = 10.0F is raised fully
        //motionFloor.outputs[index].Voltage = voltage;
        targetPos[index] = voltage;
        SetDir();
    }
    public void moveOneQuad(int index, float voltage) {
        // set the voltage (translates to floor height) of the provided bladder to the provided voltage
        // voltage = 0.0F is fully lowered
        // voltage = 5.0F is raised halfway
        // voltage = 10.0F is raised fully
        //motionFloor.outputs[index].Voltage = voltage;
        if (index == 3) {
            index = 2;
        }
        if (index == 2) {
            index = 3;
        }
        targetPos[index] = voltage;
        SetDir();
    }

    public void raiseAll(float voltage) {
        // for all four bladders set the provided voltage (translates to height) in a range of 0.0F - 10.0F
        if (voltage > 10) voltage = 10.0f;
        for (int i = 0; i < 4; i++) {
            targetPos[i] = voltage;
        }
        SetDir();
    }

    public void raiseFront(float voltage) {
        // raise the bladders on the front side of the floor to the provided voltage and fully lower the bladders on the back side
        if (voltage > 10) voltage = 10.0f;
        targetPos = new Vector4(0f, 0f, voltage, voltage);
        SetDir();
    }

    public void raiseBack(float voltage) {
        // raise the bladders on the back side of the floor to the provided voltage and fully lower the bladders on the front side
        if (voltage > 10) voltage = 10.0f;
        targetPos = new Vector4(voltage, voltage, 0f, 0f);
        SetDir();
    }

    public void raiseRight(float voltage) {
        // raise the bladders on the right side of the floor to the provided voltage and fully lower the bladders on the left side
        if (voltage > 10) voltage = 10.0f;
        targetPos = new Vector4(0f, voltage, 0f, voltage);
        print("right");
        SetDir();
    }

    public void raiseLeft(float voltage) {
        // raise the bladders on the left side of the floor to the provided voltage and fully lower the bladders on the right side
        if (voltage > 10) voltage = 10.0f;
        targetPos = new Vector4(voltage, 0f, voltage, 0f);
        print("left");
        SetDir();
    }
    
    public void SinFloor(float front_voltage, float back_voltage)
    {
        targetPos = new Vector4(front_voltage, front_voltage, back_voltage, back_voltage);
        SetDir();
    }
    /*
    public void SinFloor()
    {
        //float front_voltage = 9.0f;
        //float back_voltage = 0.0f;
        //while (front_voltage > 0.0f)
        //{
        targetPos = new Vector4(5.0f, 5.0f, 0.0f, 0.0f);
        SetDir();
        //yield return new WaitForSeconds(5);
        //}
        /*
        System.Threading.Thread.Sleep(500);
        targetPos = new Vector4(0f, 0f, voltage, voltage);
        SetDir();
        System.Threading.Thread.Sleep(500);
        lowerFloor();
        */
    //}

    void SetDir() {
        dir = Vector4.zero;
        if (targetPos.x > currentPos.x) dir.x = 1f;
        else if (targetPos.x < currentPos.x) dir.x = -1f;
        if (targetPos.y > currentPos.y) dir.y = 1f;
        else if (targetPos.y < currentPos.y) dir.y = -1f;
        if (targetPos.z > currentPos.z) dir.z = 1f;
        else if (targetPos.z < currentPos.z) dir.z = -1f;
        if (targetPos.w > currentPos.w) dir.w = 1f;
        else if (targetPos.w < currentPos.w) dir.w = -1f;
    }

    public void zeroVoltage() {
        // backwards compatability
        lowerFloor();
    }

    public void move(int index, float voltage) {
        // backwards compatability
        moveOne(index, voltage);
    }

    void UpdateFloor() {
        if (dir != Vector4.zero) { 
            //print("in UpdateFloor");
            //print(currentPos);
            //print(targetPos);
            //print(dir);
        }
        //print("in");
        // x
        if ((dir.x > 0 && currentPos.x <= targetPos.x) || (dir.x < 0 && currentPos.x >= targetPos.x)) {
            currentPos.x = Mathf.Clamp(currentPos.x + (delta * dir.x), minVoltage, maxVoltage);
        }
        else dir.x = 0;
        // y
        if ((dir.y > 0 && currentPos.y <= targetPos.y) || (dir.y < 0 && currentPos.y >= targetPos.y)) {
            currentPos.y = Mathf.Clamp(currentPos.y + (delta * dir.y), minVoltage, maxVoltage);
        }
        else dir.y = 0;
        // z
        if ((dir.z > 0 && currentPos.z <= targetPos.z) || (dir.z < 0 && currentPos.z >= targetPos.z)) {
            currentPos.z = Mathf.Clamp(currentPos.z + (delta * dir.z), minVoltage, maxVoltage);
        }
        else dir.z = 0;
        // w
        if ((dir.w > 0 && currentPos.w <= targetPos.w) || (dir.w < 0 && currentPos.w >= targetPos.w)) {
            currentPos.w = Mathf.Clamp(currentPos.w + (delta * dir.w), minVoltage, maxVoltage);
        }
        else dir.w = 0;

        // move the floor
        motionFloor.outputs[0].Voltage = currentPos.x;
        motionFloor.outputs[1].Voltage = currentPos.y;
        motionFloor.outputs[2].Voltage = currentPos.z;
        motionFloor.outputs[3].Voltage = currentPos.w;
    }
}
