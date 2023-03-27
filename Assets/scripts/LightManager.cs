using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightColor
{
    public int[] black, white, red, orange, yellow, green, cyan, blue, purple; 

    public LightColor()
    {
        black = new int[5] { 0, 0, 0, 0, 0 };
        white = new int[5] { 255, 255, 255, 255, 255 };
        red = new int[5] { 255, 0, 0, 0, 255 };
        orange = new int[5] { 255, 128, 0, 0, 255 };
        yellow = new int[5] { 255, 255, 0, 255, 255 };
        green = new int[5] { 0, 255, 0, 0, 255 };
        cyan = new int[5] { 128, 128, 255, 0, 255 };
        blue = new int[5] { 0, 0, 255, 0, 255 };
        purple = new int[5] { 255, 0, 255, 0, 255 };
    }
}

public class LightManager : MonoBehaviour
{
    
    [HideInInspector]
    public LightColor colors = new LightColor();

    private OSCController osc;
    private string message = "";

    // Start is called before the first frame update
    void Start()
    {
        osc = GameObject.Find("OSCMain").GetComponent<OSCController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Blackout()
    {
        osc.SendOSCMessage("/lighting operations blackout");
    }

    public void SetLights(string function, string group, int[] color)
    {
        osc.SendOSCMessage("/lighting " + function + " " + group + " " + color[0] + " " + color[1] + " " + color[2] + " " + color[3] +" " + color[4]);
    }

    public void SetCue(string act, int cue)
    {
        osc.SendOSCMessage("/lighting cue " + act + " " + cue.ToString());
    }
}
