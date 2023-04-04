using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineCart : MonoBehaviour
{
    // Start is called before the first frame update
    private FloorController floor = null;

    void Start()
    {
        floor = FloorController.curr;
        floor.SetupFloor();
        floor.raiseAll(5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
