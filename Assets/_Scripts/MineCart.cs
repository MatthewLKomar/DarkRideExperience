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

    public Transform Front;
    public Transform Back;
    public Transform Right;
    public Transform Left;



    private FloorController floor = null;
    private CartState cartState;

    //MLKomar: Used to calculate if we're going straight or not
    private float marginOfErrorStraight = 0.001f;
    private float marginOfErrorDirection = 0.001f;
    private Vector3 lastPosition;
    private int Count = 0;


    public float MaxHeight = 2.0f;
    public float MaxSway = 0.2f;

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
    
    
    private string Screenlog1 = "";
    private string Screenlog2 = "";
    private string Screenlog3 = "";
    private string Screenlog4 = "";
    private string Screenlog5 = "";

    float backVoltage = 0.0f, frontVoltage = 0.0f, leftVoltage = 0.0f, rightVoltage = 0.0f;

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

    float CalculateVoltage(float heightVal, float maxVal)
    {
        heightVal = Remap(Mathf.Abs(heightVal), 0, maxVal, 0, 10);
        heightVal = heightVal > 10.0f ? 10.0f : heightVal;
        return heightVal;

    }


    IEnumerator sampleVoltage()
    {
        while (true)
        {
            switch (cartState)
            {
                case CartState.left:
                    if (floor) floor.raiseLeft(leftVoltage);
                    break;
                case CartState.right:
                    if (floor) floor.raiseRight(rightVoltage);
                    break;
                case CartState.down:
                    if (floor) floor.raiseBack(backVoltage);
                    break;
                case CartState.up:
                    if (floor) floor.raiseFront(frontVoltage);
                    break;
                case CartState.straight:
                    break;
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

                float frontHeight = FrontCurrentHeight - FrontLastHeight;
                float backHeight = BackCurrentHeight - BackLastHeight;
                float leftHeight = LeftCurrentHeight - LeftLastHeight;
                float rightHeight = RightCurrentHeight - RightLastHeight;

                var direction = transform.position - lastPosition;
                var localDirection = transform.InverseTransformDirection(direction);
                
                //going right
                if (localDirection.x < 0 && rightHeight != 0.0f && Mathf.Abs(transform.position.y - lastPosition.y) < 0.6f)
                {
                    rightVoltage = CalculateVoltage(rightHeight, MaxSway);
                    cartState = CartState.right;
                    Screenlog1 = "Going Right";
                    floor.raiseLeft(rightVoltage);
                }
                //you're leaning to the left...
                else if (localDirection.x > 0 && leftHeight != 0.0f && Mathf.Abs(transform.position.y - lastPosition.y) < 0.6f)
                {
                    leftVoltage = CalculateVoltage(leftHeight, MaxSway);
                    cartState = CartState.left;
                    Screenlog1 = "Going Left";
                    floor.raiseRight(leftVoltage);
                }
                //going up
                else if (localDirection.y < 0 || transform.position.y > lastPosition.y)
                {
                    frontVoltage = CalculateVoltage(frontHeight, MaxHeight);
                    cartState = CartState.up;
                    Screenlog1 = "Going up";
                    floor.raiseFront(frontVoltage);
                }
                //going down
                else if (localDirection.y > 0 || transform.position.y < lastPosition.y)
                {
                    backVoltage = CalculateVoltage(backHeight, MaxHeight);
                    cartState = CartState.down;
                    Screenlog1 = "Going down";
                    floor.raiseBack(backVoltage);
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

            FrontLastHeight = FrontCurrentHeight;
            BackLastHeight = BackCurrentHeight;

            RightLastHeight = RightCurrentHeight;
            LeftLastHeight = LeftCurrentHeight;

            lastPosition = transform.position;


            yield return new WaitForSeconds(0.1f);

        }

    }
}

