using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineCart : MonoBehaviour
{
    // Start is called before the first frame update
    private FloorController floor = null;

    public float LengthThreshold = 2.0f;
    public Transform Front;
    public Transform Back;

    private float FrontLastHeight;
    private float BackLastHeight;
    private float FrontCurrentHeight;
    private float BackCurrentHeight;
    private float LengthOfMinecart;
    float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    void Start()
    {
        floor = FloorController.curr;
        if (floor == null)
        {
            floor.SetupFloor();
            floor.raiseAll(5.0f);
        }
        

        FrontCurrentHeight = Front.position.y;
        BackCurrentHeight = Back.position.y;

        FrontLastHeight = FrontCurrentHeight;
        BackLastHeight = BackCurrentHeight;

        LengthOfMinecart = Vector3.Distance(Front.position, Back.position);
        LengthOfMinecart /= LengthThreshold;
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (floor == null)
            return;
        FrontCurrentHeight = Front.position.y;
        BackCurrentHeight = Back.position.y;
        
        var frontHeight = Mathf.Abs(FrontCurrentHeight - FrontLastHeight);
        var lastHeight = Mathf.Abs(BackCurrentHeight - BackLastHeight);




        //compare height from front and back to last frame's
        //then remap it 
        var frontVoltage = Remap(frontHeight, 0, LengthOfMinecart, 0 , 10);
        var backVoltage = Remap(lastHeight, 0, LengthOfMinecart, 0, 10);

        //print("front voltage: " + frontVoltage + " back Voltage: " + backVoltage + "front height " + frontHeight);
        floor.raiseBack(backVoltage);
        floor.raiseFront(frontVoltage);

        FrontLastHeight = FrontCurrentHeight;
        BackLastHeight = BackCurrentHeight;
    }
}
