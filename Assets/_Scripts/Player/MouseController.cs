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
        var x = Input.mousePosition.x;
        var y = Input.mousePosition.y;
        if (oldMouseX - x != 0) {
            var vec = new Vector3(-x, -y, 0.0f);
            Rotateable.transform.Rotate(vec * Time.deltaTime * rotationStrength);
        }
        
        oldMouseX = x;
        oldMouseY = y;
    }
}
