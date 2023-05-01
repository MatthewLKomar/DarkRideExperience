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

    public GameObject Gun = null;
    public GameObject Shield = null;
    public float player1RotationStrengthX = 5.0f;
    public float player1RotationStrengthY = 5.0f;

    public float player2RotationStrengthX = 5.0f;
    public float player2RotationStrengthY = 5.0f;

    public bool isMultiplePlayers = true; 
    void Update()
    {

        float P1MouseX = 0.0f, P1MouseY = 0.0f, P2MouseX = 0.0f, P2MouseY = 0.0f;
        //player 1 movement... 
        
            P1MouseX = Input.GetAxis("Mouse X");
            P1MouseY = Input.GetAxis("Mouse Y");
        if (isMultiplePlayers) {
            P2MouseX = 0.0f;
            P2MouseY = -Input.GetAxis("P2-VerticalLeft");
        }
        else {

        }

        //player 2 movement... 
        Shield.transform.Rotate(-P1MouseY * player1RotationStrengthY * Time.deltaTime, P1MouseX * player1RotationStrengthX * Time.deltaTime,0.0f);
    }
}
