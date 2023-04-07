using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CartState
{
    left,
    right,
    down,
    up
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

    //height Up/Down
    private float MaxHeight = 20.0f;
    private float FrontLastHeight;
    private float BackLastHeight;
    private float FrontCurrentHeight;
    private float BackCurrentHeight;


    private CartState cartState;
    //height left/right
    private float MaxSway = 10.0f;
    private float RightLastHeight;
    private float LeftLastHeight;
    private float RightCurrentHeight;
    private float LeftCurrentHeight;
    
    private int Count = 0 ;
    
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
 /*       floor = FloorController.curr;
        // if we can't to the floor for some reason, quit
        if (floor == null)
        {
            print("can't connect to the floor!!");
            return;
        }*/

        //floor.SetupFloor();

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

    }

    float CalculateVoltage(float heightVal)
    {

    }

    // Update is called once per frame
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
                float backVoltage = 0.0f, frontVoltage = 0.0f, leftVoltage = 0.0f, rightVoltage = 0.0f;
                
                if (frontHeight == backHeight && leftHeight == rightHeight) {
                    //floor.raiseAll(0);
                    Screenlog1 = "Going Straight";
                }
                // you're leaning to the right..
                else if (leftHeight > rightHeight)
                {
                    rightVoltage = Remap(Mathf.Abs(rightHeight), 0, MaxSway, 0, 10);
                    rightVoltage = rightVoltage > 10.0f ? 10.0f : rightVoltage;
                    cartState = CartState.right;
                    Screenlog1 = "Going Right";
                    //floor.raiseFront(rightVoltage);
                }
                //you're leaning to the left...
                else if (rightHeight > leftHeight)
                {
                    leftVoltage = Remap(Mathf.Abs(leftHeight), 0, MaxSway, 0, 10);
                    leftVoltage = leftVoltage > 10.0f ? 10.0f : leftVoltage;
                    cartState = CartState.left;
                    Screenlog1 = "Going Left";
                    //floor.raiseFront(leftVoltage);
                }
                //if you're going down, lower front and don't touch it
                else if (backHeight > frontHeight || (frontHeight < 0 && backHeight > 0))
                {
                    backVoltage = Remap(Mathf.Abs(backHeight), 0, MaxHeight, 0, 10);
                    backVoltage = backVoltage > 10.0f ? 10.0f : backVoltage;
                    cartState = CartState.down;
                    Screenlog1 = "Going down";
                    //floor.raiseBack(backVoltage);
                }
                //if you're going up, lower back and don't touch it. 
                else if (frontHeight > backHeight || (frontHeight > 0 && backHeight < 0))
                {
                    frontVoltage = Remap(Mathf.Abs(frontHeight), 0, MaxHeight, 0, 10);
                    frontVoltage = frontVoltage > 10.0f ? 10.0f : frontVoltage;
                    cartState = CartState.up;
                    Screenlog1 = "Going up";
                    //floor.raiseFront(frontVoltage);
                }

                



                Screenlog2 = "Back Height: " + backHeight + " Front Height: " + frontHeight;
                Screenlog3 = "back Voltage: " + backVoltage + " front voltage: " + frontVoltage;
                Screenlog4 = "Left Height: " + leftHeight + " Right Height: " + rightHeight;
                Screenlog3 = "left Voltage: " + leftVoltage + " right voltage: " + rightVoltage;

                print(Screenlog2);
                print(Screenlog3);

                //yield return new WaitForSeconds(0.1f);

            }
            else {
                Screenlog1 = "Waiting to get clean values...";
                Count++;
            }

            FrontLastHeight = FrontCurrentHeight;
            BackLastHeight = BackCurrentHeight;

            RightLastHeight = RightCurrentHeight;
            LeftLastHeight = LeftCurrentHeight;

            yield return new WaitForSeconds(TimeToWaitBetweenFloorCommands);

        }

    }
}

