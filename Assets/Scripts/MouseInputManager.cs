using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RawMouseDriver;
using RawInputSharp;

public class MouseInputManager : MonoBehaviour {

    RawMouseDriver.RawMouseDriver mousedriver;
    private RawMouse[] mice;
    [SerializeField] private Vector2[] move;
    private const int NUM_MICE = 4;

    private int mouse_index = 0;
    private RawMouse mouse1;

    // Use this for initialization
    void Start()
    {
        mousedriver = new RawMouseDriver.RawMouseDriver();
        mice = new RawMouse[NUM_MICE];
        move = new Vector2[NUM_MICE];

        // null ref error on mouse1  
        mousedriver.GetMouse(mouse_index, ref mouse1);
        print(mouse1);
    }

    void Update()
    {
        // Loop through all the connected mice
        for (int i = 0; i < mice.Length; i++)
        {
            try
            {
                mousedriver.GetMouse(i, ref mice[i]);
                // Cumulative movement
                move[i] += new Vector2(mice[i].XDelta, -mice[i].YDelta);
            }
            catch { }
        }
    }

    void OnGUI()
    {
        GUILayout.Label("Connected Mice:");
        for (int i = 0; i < mice.Length; i++)
        {
            if (mice[i] != null)
                GUILayout.Label("Mouse[" + i.ToString() + "] : " + move[i] + mice[i].Buttons[0] + mice[i].Buttons[1]);
        }
    }

    void OnApplicationQuit()
    {
        // Clean up
        mousedriver.Dispose();
    }
}
