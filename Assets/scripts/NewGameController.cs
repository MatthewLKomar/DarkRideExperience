using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameController : MonoBehaviour
{
    public NewTimer timerScript;
    public NewBoxManager boxScript;

    private int boxCounter;

    // Start is called before the first frame update
    void Start()
    {
        boxCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // start the timer
            timerScript.StartTimer();
            print("The max time is : " + timerScript.maxTime.ToString());
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            GotT();
        }
    }

    void GotT()
    {
        // do something with the boxes
        print("Doing something with the boxes");
        boxScript.BoxSomething(boxCounter);
        boxCounter += 1;
    }
}
