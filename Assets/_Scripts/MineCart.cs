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
    private float MaxSway = 3.0f;
    private float RightLastHeight;
    private float LeftLastHeight;
    private float RightCurrentHeight;
    private float LeftCurrentHeight;

    private Vector3 lastPosition;
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
        //floor = FloorController.curr;
        //// if we can't to the floor for some reason, quit
        //if (floor == null)
        //{
        //    print("can't connect to the floor!!");
        //    return;
        //}

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

    float CalculateVoltage(float heightVal, float maxVal)
    {
        heightVal = Remap(Mathf.Abs(heightVal), 0, maxVal, 0, 10);
        heightVal = heightVal > 10.0f ? 10.0f : heightVal;
        return heightVal;

    }


    //TODO: MLKomar: Issues with raiseFloor()
    // If left and right are almost equal than keep the previous state/turning values.
    // Sometimes left > right or vice versa and the program still thinks we're turning right. 
    // Make additive platform raising i.e. be tilted up and to the left slightly. 


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
                float leftHeight = Mathf.Ceil(LeftCurrentHeight - LeftLastHeight);
                float rightHeight = Mathf.Ceil(RightCurrentHeight - RightLastHeight);
                float backVoltage = 0.0f, frontVoltage = 0.0f, leftVoltage = 0.0f, rightVoltage = 0.0f;

                var direction = transform.position - lastPosition;
                var localDirection = transform.InverseTransformDirection(direction) * 100.0f;
                
                Screenlog5 = "dir: " + localDirection.x + " , " + localDirection.y + " , " + localDirection.z;

                /* if (frontHeight == backHeight && frontHeight == 0.0f && leftHeight == 0.0f && leftHeight == rightHeight) {
                     //floor.raiseAll(0);
                     Screenlog1 = "Going Straight";
                 }
                 //if you're going down, lower front and don't touch it
                 //else if (backHeight > frontHeight || (frontHeight < 0 && backHeight > 0))
                 else if (localDirection.y > 0 && localDirection.x < localDirection.y) {
                     backVoltage = CalculateVoltage(backHeight, MaxHeight);
                     cartState = CartState.down;
                     Screenlog1 = "Going down";
                     //floor.raiseBack(backVoltage);
                 }
                 //if you're going up, lower back and don't touch it. 
                 //else if (frontHeight > backHeight || (frontHeight > 0 && backHeight < 0))
                 else if (localDirection.y < 0 && localDirection.x < localDirection.y) {
                     frontVoltage = CalculateVoltage(frontHeight, MaxHeight);
                     cartState = CartState.up;
                     Screenlog1 = "Going up";
                     //floor.raiseFront(frontVoltage);
                 }*/
                // you're leaning to the right..
                //else if (leftHeight > rightHeight)

                float marginOfError = 0.01f;
                if (Mathf.Abs(localDirection.x-localDirection.y) <= marginOfError)
                {
                    Screenlog1 = "Going Straight";
                }
                else if (localDirection.x < 0)
                {

                    rightVoltage = CalculateVoltage(rightHeight, MaxSway);
                    cartState = CartState.right;
                    Screenlog1 = "Going Right";
                    //floor.raiseLeft(rightVoltage);
                }
                //you're leaning to the left...
                //else if (rightHeight > leftHeight)
                else if (localDirection.x > 0)
                {
                    leftVoltage = CalculateVoltage(leftHeight, MaxSway);
                    cartState = CartState.left;
                    Screenlog1 = "Going Left";
                    //floor.raiseRight(leftVoltage);
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

            lastPosition = transform.position;


            yield return new WaitForSeconds(TimeToWaitBetweenFloorCommands);

        }

    }
}

