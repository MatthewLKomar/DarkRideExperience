using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    
    private float MaxSway = 10.0f;
    
    private int Count = 0 ;
    
    private string Screenlog1 = "";
    private string Screenlog2 = "";
    private string Screenlog3 = "";


    float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    void OnGUI() {
        GUILayout.Label(Screenlog1);
        GUILayout.Label(Screenlog2);
        GUILayout.Label(Screenlog3);
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

        StartCoroutine(raiseFloor());

    }

    // Update is called once per frame
    IEnumerator raiseFloor() {
        while (true) {
            FrontCurrentHeight = Front.position.y;
            BackCurrentHeight = Back.position.y;

            if (Count >= 3) {

                float frontHeight = FrontCurrentHeight - FrontLastHeight;
                float backHeight = BackCurrentHeight - BackLastHeight;

                float backVoltage = 0.0f, frontVoltage = 0.0f;
                if (frontHeight == backHeight) {
                    floor.raiseAll(0);
                    Screenlog1 = "Going Straight";
                }
                //if you're going down, lower front and don't touch it
                else if (backHeight > frontHeight || (frontHeight < 0 && backHeight > 0)) {
                    backVoltage = Remap(Mathf.Abs(backHeight), 0, MaxHeight, 0, 10);
                    backVoltage = backVoltage > 10.0f ? 10.0f : backVoltage;
                    Screenlog1 = "Going down";
                    floor.raiseBack(backVoltage);
                }
                //if you're going up, lower back and don't touch it. 
                else if (frontHeight > backHeight || (frontHeight > 0 && backHeight < 0)) 
                {
                    frontVoltage = Remap(Mathf.Abs(frontHeight), 0, MaxHeight, 0, 10);
                    frontVoltage = frontVoltage > 10.0f ? 10.0f : frontVoltage;
                    Screenlog1 = "Going up";
                    floor.raiseFront(frontVoltage);
                }
                // you're leaning to the left..
                

                //you're leaning to the right...

                

                Screenlog2 = "Back Height: " + backHeight + " Front Height: " + frontHeight;
                Screenlog3 = "back Voltage: " + backVoltage + " front voltage: " + frontVoltage;

                print(Screenlog2);
                print(Screenlog3);

                //yield return new WaitForSeconds(0.1f);

            }
            else {
                Count++;
            }

            FrontLastHeight = FrontCurrentHeight;
            BackLastHeight = BackCurrentHeight;
            
            yield return new WaitForSeconds(TimeToWaitBetweenFloorCommands);

        }

    }
}
