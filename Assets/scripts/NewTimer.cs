using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTimer : MonoBehaviour
{
    public int maxTime;

    private int time;
    private bool isStarted; 

    // Start is called before the first frame update
    void Start()
    {
        time = maxTime;
        //StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTimer()
    {
        // if the timer has not been started then start it
        if (isStarted == false)         // (!isStarted)            
        {
            isStarted = true;
            Invoke("Tick", 1.0f);
        }
    }

    void Tick()
    {
        // dec time
        time -= 1;
        print(time);
        // have we run out of time 
        if (time < 0)
        {
            // do something
            SendMessage("TimerUp");
        }
        else Invoke("Tick", 1.0f);
    }
}
