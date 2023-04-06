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
    private float oldSlope = 0;
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

        //compare distance from front and back
        //then remap it 
    
    }
}
