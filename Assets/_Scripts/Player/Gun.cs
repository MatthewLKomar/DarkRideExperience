using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position, transform.TransformDirection(Vector3.left) * 500.0f);
        if (/*Input.GetAxis("P1-RightBumper") != 0 || */Input.GetMouseButtonDown(0)) {
            bool hit = Physics.Raycast(transform.position,transform.TransformDirection(Vector3.left), out RaycastHit impact);
            if (hit && impact.transform.tag == "Enemy") {
                Destroy(impact.transform.gameObject);
            }
        }
    }
}
