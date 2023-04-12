using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RavingBots.MultiInput;

public class MIDeviceListener : MonoBehaviour
{
    [System.Serializable]
    public class Listener
    {
        public InputCode key;
        public GameObject notObj;
        public string function;
        public float value;
    }

    
    public Listener[] listen;
    
    // this is set to the device object that we get from InputState
    public IDevice Device;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Device == null)
        {
            return;
        }

        foreach (Listener l in listen)
        {
            var button = Device[l.key];
            if (button != null && button.IsDown)
            {
                // got a button press
                print(l.key);
                l.notObj.SendMessage(l.function,l.value);
            }
        }
    }
}
