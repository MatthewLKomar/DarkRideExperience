using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineCart : MonoBehaviour
{
    // Start is called before the first frame update
    private FloorController floor = null;
    public Transform Front;
    public Transform Back;

    private Vector3 FrontLastPosition;
    private Vector3 BackLastPosition;
    private Vector3 FrontCurrentPosition;
    private Vector3 BackCurrentPosition;
    private float oldAngle = 0;
    void Start()
    {
        floor = FloorController.curr;
        floor.SetupFloor();
//      floor.raiseAll(5.0f);
    }

    // Update is called once per frame
    void FixedUpdate() {
        RaycastHit hit;

        // this should shoot a ray downwards... 
        Debug.DrawRay(Front.position, transform.TransformDirection(Vector3.down));
        if (Physics.Raycast(Front.position, transform.TransformDirection(Vector3.down), out hit, 10.0f)) {
            FrontCurrentPosition = hit.point;
        }

        Debug.DrawRay(Back.position, transform.TransformDirection(Vector3.down));
        if (Physics.Raycast(Back.position, transform.TransformDirection(Vector3.down), out hit, 10.0f)) {
            BackCurrentPosition = hit.point;
        }

        var Angle = Vector3.Angle(BackCurrentPosition, FrontCurrentPosition);
        if (Angle > oldAngle)
        {
            //wait what if we look at it from height instead? Just Z value??
            print("we're going up, Angle:  " + Angle + " old Angle" + oldAngle);
            //lower back
            //raise front
        }
        else if (Angle < oldAngle)
        {
            print("we're going down, Angle:  " + Angle + " old Angle" + oldAngle);
            //raise back
            //lower front
            //what if we map Angle (which will be in degrees) to (0, 5)??
        }

        oldAngle = Angle;
    }
}
