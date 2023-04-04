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
        if (Physics.Raycast(Front.position, transform.TransformDirection(Vector3.down), out hit, 10.0f)) {
            FrontCurrentPosition = hit.point;
        }
        if (Physics.Raycast(Back.position, transform.TransformDirection(Vector3.down), out hit, 10.0f)) {
            BackCurrentPosition = hit.point;
        }

        //do calculations accordingly... 
        // if the value to raie the floor hasn't changed since last frame
            //return
        // if we're going up, we'll have to move raise the ass end of the floor down, the front up. 
        // if we're going down, we'll have to raise the ass end of the floor up, the front down. 
        
        
    
    }
}
