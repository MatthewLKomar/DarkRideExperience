using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointTrigger : MonoBehaviour {

    public GameObject good;
    public GameObject bad;
    public GameObject trans;
    public GameObject hitObj;
    public bool destroyThis = false;
    public bool playSFX = false;
    public GameObject notObj;

    private bool triggered = false;

    // Use this for initialization
    void Start () {
        good.SetActive(false);
        bad.SetActive(true);
        trans.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		//if( Input.GetKeyDown(KeyCode.P))
  //      {
  //          Flip();
  //      }
	}

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject == hitObj && !triggered)
        {
            print("Waypoint Found");
            notObj.SendMessage("FoundWaypoint", gameObject.name);
        }



    }

    public void Flip()
    {
        if (!triggered) // trigger the waypoint only once
        {
            StartCoroutine("Flipping");
            triggered = true;
        }
        
    }

    IEnumerator Flipping()
    {
        trans.SetActive(true);
        if (playSFX) {
            AudioSource a = GetComponent<AudioSource>();
            a.Play();
        }
        yield return new WaitForSeconds(0.5f);
        bad.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        good.SetActive(true);
        if (destroyThis) { Destroy(this, 1f); }
    }
}
