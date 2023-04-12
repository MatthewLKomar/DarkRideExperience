using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DevicesAssigned()
    {
        // RCC - called by MIAssignDevices to signal that players have been assigned an input device
        Debug.Log("devices are assigned");
        // Do Something ... 
        //TODO
    }
}
