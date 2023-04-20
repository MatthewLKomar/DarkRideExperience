using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    /*
            2200 absolete left
            7600 absolete right
            863 absolete up
            -215 absolete down
     */

    private float oldMouseX = 0.0f;
    private float oldMouseY = 0.0f;


    public GameObject Rotateable = null;
    public float rotationStrength = 5.0f;
    void Update()
    {

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        Rotateable.transform.Rotate(-mouseY * rotationStrength * Time.deltaTime, mouseX * rotationStrength * Time.deltaTime,0.0f);
        var x = Input.mousePosition.x;
        var y = Input.mousePosition.y;
        
        oldMouseX = x;
        oldMouseY = y;
    }
}
