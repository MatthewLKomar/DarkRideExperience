using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public AudioClip audio; 
    private AudioSource audioSource = null;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Debug.DrawLine(transform.position, transform.TransformDirection(Vector3.left) * 500.0f + transform.position);
        if (/*Input.GetAxis("P1-RightBumper") != 0 || */Input.GetMouseButtonDown(0)) {
            bool hit = Physics.Raycast(transform.position,transform.TransformDirection(Vector3.left), out RaycastHit impact);
            if (hit && impact.transform.tag == "Enemy") {
                audioSource.PlayOneShot(audio);
                Destroy(impact.transform.gameObject);
            }
        }
    }
}
