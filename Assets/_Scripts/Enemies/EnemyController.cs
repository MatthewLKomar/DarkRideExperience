using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private ThrowData CalculateThrowData(Vector3 TargetPosition, Vector3 StartPosition)
    {
        // v == initial velociy, assumes max speed for now
        // x = distance to travel on x/z plane only
        // y = difference in altiudes from thrown point to target hit point
        // g = gravity
    }
}
