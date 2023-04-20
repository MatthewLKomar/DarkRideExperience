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
    public float rotationStrengthX = 5.0f;
    public float rotationStrengthY = 5.0f;
    void Update()
    {

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        // add an aditional axis with a different sweet spot
        
        Rotateable.transform.Rotate(-mouseY * rotationStrengthY * Time.deltaTime, mouseX * rotationStrengthX * Time.deltaTime,0.0f);
        var x = Input.mousePosition.x;
        var y = Input.mousePosition.y;
        
        oldMouseX = x;
        oldMouseY = y;
    }
}
