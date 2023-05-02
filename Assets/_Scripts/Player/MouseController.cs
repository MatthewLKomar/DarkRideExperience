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
    public float player1RotationStrengthX = 0.8f;
    public float player1RotationStrengthY = 0.6f;

    public float player2RotationStrengthX = 50.0f;
    public float player2RotationStrengthY = 50.0f;

    public bool isMultiplePlayers = true; 
    void Update()
    {

        float P1MouseX = 0.0f, P1MouseY = 0.0f, P2MouseX = 0.0f, P2MouseY = 0.0f;
        //ring mouse
        P1MouseX = Input.GetAxis("Mouse X");
        P1MouseY = Input.GetAxis("Mouse Y");
        Shield.transform.Rotate(-P1MouseY * player1RotationStrengthY * Time.deltaTime, P1MouseX * player1RotationStrengthX * Time.deltaTime, 0.0f);

        if (isMultiplePlayers) { //second player
            //P2MouseX = Input.GetAxis("P1-HorizontalLeft");
            //P2MouseY = -Input.GetAxis("P1-VerticalLeft");
            P2MouseX = Input.GetAxis("Horizontal");
            P2MouseY = Input.GetAxis("Vertical");
            //-P2MouseY * player2RotationStrengthY * Time.deltaTime
            Gun.transform.Rotate(0.0f, P2MouseX * player2RotationStrengthX * Time.deltaTime, 0.0f);
            var vec = Gun.transform.localPosition;
            vec.y += P2MouseY * 0.05f;
            //Todo instead of using rotations use X and Y movement only and then rotate when we get beyond
            //a certain amount of X value. 
            if (vec.y < 2.0f && vec.y > -2.0f)
                Gun.transform.localPosition = vec;
        }
        
    }
}
