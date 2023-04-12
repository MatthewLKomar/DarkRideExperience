using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CartState
{
    left,
    right,
    down,
    up,
    straight
}
public class MineCart : MonoBehaviour
{
    

    public float LengthThreshold = 2.0f;
    public float TimeToWaitBetweenFloorCommands = 0.5f;

    [Header("Transforms of the cart's tips")]
    public Transform Front;
    public Transform Back;
    public Transform Right;
    public Transform Left;

    [Tooltip("this gets the floor controller so we can manipulate the motion simulator")]
    private FloorController floor = null;
    private CartState cartState;

    //MLKomar: Used to calculate if we're going straight or not
    private float marginOfErrorStraight = 0.001f;
    private float marginOfErrorDirection = 0.001f;
    private int Count = 0;


    public float MaxHeight = 2.0f;
    public float MaxSway = 0.2f;

    //Voltage for motion simulator
    float backVoltage = 0.0f,
          frontVoltage = 0.0f,
          leftVoltage = 0.0f,
          rightVoltage = 0.0f;


    //height Up/Down
    private float FrontLastHeight;
    private float BackLastHeight;
    private float FrontCurrentHeight;
    private float BackCurrentHeight;
    //height left/right
    private float RightLastHeight;
    private float LeftLastHeight;
    private float RightCurrentHeight;
    private float LeftCurrentHeight;

    private Vector3 lastPosition;

    //Debug strings which get printed to Screen.
    private string Screenlog1 = "";
    private string Screenlog2 = "";
    private string Screenlog3 = "";
    private string Screenlog4 = "";
    private string Screenlog5 = "";


    float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    void OnGUI() {
        GUILayout.Label(Screenlog1);
        GUILayout.Label(Screenlog2);
        GUILayout.Label(Screenlog4);
        GUILayout.Label(Screenlog3);
        GUILayout.Label(Screenlog5);
    }

    void Start()
    {
        //MLKomar: TODO: add param to make floor optional, in that case don't run
        //               a lot of the floor calculations...
        floor = FloorController.curr;
        // if we can't to the floor for some reason, quit
        if (floor == null)
        {
            print("can't connect to the floor!!");
            return;
        }

        floor.SetupFloor();

        FrontCurrentHeight = Front.position.y;
        BackCurrentHeight = Back.position.y;

        FrontLastHeight = FrontCurrentHeight;
        BackLastHeight = BackCurrentHeight;

        RightLastHeight = Right.position.y;
        LeftLastHeight = Left.position.y;

        RightCurrentHeight = Right.position.y;
        LeftCurrentHeight = Left.position.y;

        RightLastHeight = RightCurrentHeight;
        LeftLastHeight = LeftCurrentHeight;

        StartCoroutine(raiseFloor());
        StartCoroutine(sampleVoltage());
    }


    [Tooltip("Calculate voltage for a given height with a maximum value. Make sure heightVal <= maxVal")]
    float CalculateVoltage(float heightVal, float maxVal)
    {
        heightVal = Remap(Mathf.Abs(heightVal), 0, maxVal, 0, 10);
        //MLKomar: Probably superflous, but the below ternary operation is to make sure
                 //we don't fry the phidgets controlling
                 //the platform if heightVal is > maxVal. 
        heightVal = heightVal > 10.0f ? 10.0f : heightVal;
        return heightVal;
    }


    IEnumerator sampleVoltage()
    {
        while (true)
        {
            if (floor)
            {
                switch (cartState)
                {
                    case CartState.left:
                        floor.raiseLeft(leftVoltage);
                        break;
                    case CartState.right:
                        floor.raiseRight(rightVoltage);
                        break;
                    case CartState.down:
                        floor.raiseBack(backVoltage);
                        break;
                    case CartState.up:
                        floor.raiseFront(frontVoltage);
                        break;
                    case CartState.straight:
                        break;
                }
            }
            
            backVoltage = 0.0f;
            frontVoltage = 0.0f;
            leftVoltage = 0.0f;
            rightVoltage = 0.0f;

            yield return new WaitForSeconds(TimeToWaitBetweenFloorCommands);
        }
    }


    /// <summary>
    /// Calculates which side of the platform to raise. 
    /// </summary>
    /// <returns></returns>
    IEnumerator raiseFloor() {
        while (true) {

            FrontCurrentHeight = Front.position.y;
            BackCurrentHeight = Back.position.y;

            RightCurrentHeight = Right.position.y;
            LeftCurrentHeight = Left.position.y;

            if (Count >= 3) {
                //Calculate change in height from last time we checked.
                float frontHeight = FrontCurrentHeight - FrontLastHeight;
                float backHeight = BackCurrentHeight - BackLastHeight;
                float leftHeight = LeftCurrentHeight - LeftLastHeight;
                float rightHeight = RightCurrentHeight - RightLastHeight;

                //calculate change in local direction from last position.
                var direction = transform.position - lastPosition;
                var localDirection = transform.InverseTransformDirection(direction);
                
                //leaning to the right
                if (localDirection.x < 0 && rightHeight != 0.0f && Mathf.Abs(transform.position.y - lastPosition.y) < 0.6f)
                {
                    rightVoltage = CalculateVoltage(rightHeight, MaxSway);
                    cartState = CartState.right;
                    Screenlog1 = "Going Right";
                }
                //you're leaning to the left...
                else if (localDirection.x > 0 && leftHeight != 0.0f && Mathf.Abs(transform.position.y - lastPosition.y) < 0.6f)
                {
                    leftVoltage = CalculateVoltage(leftHeight, MaxSway);
                    cartState = CartState.left;
                    Screenlog1 = "Going Left";
                }
                //going up
                else if (localDirection.y < 0 || transform.position.y > lastPosition.y)
                {
                    frontVoltage = CalculateVoltage(frontHeight, MaxHeight);
                    cartState = CartState.up;
                    Screenlog1 = "Going up";
                }
                //going down
                else if (localDirection.y > 0 || transform.position.y < lastPosition.y)
                {
                    backVoltage = CalculateVoltage(backHeight, MaxHeight);
                    cartState = CartState.down;
                    Screenlog1 = "Going down";
                }
                else
                {
                    Screenlog1 = "Going Straight";
                    cartState = CartState.straight;
                }
                
                Screenlog2 = "Back Height: " + backHeight + " Front Height: " + frontHeight;
                Screenlog3 = "back Voltage: " + backVoltage + " front voltage: " + frontVoltage;
                Screenlog4 = "Left Height: " + leftHeight + " Right Height: " + rightHeight;
                Screenlog5 = "left Voltage: " + leftVoltage + " right voltage: " + rightVoltage;
            }
            else {
                Screenlog1 = "Waiting to get clean values...";
                Count++;
            }


            //Set old heights to current heights
            FrontLastHeight = FrontCurrentHeight;
            BackLastHeight = BackCurrentHeight;
            RightLastHeight = RightCurrentHeight;
            LeftLastHeight = LeftCurrentHeight;
            //Set old position.
            lastPosition = transform.position;


            yield return new WaitForSeconds(0.1f);

        }

    }
}

