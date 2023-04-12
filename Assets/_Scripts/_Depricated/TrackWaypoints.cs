using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackWaypoints : MonoBehaviour
{
    public GameObject[] points;
    public Text counterText;
    public CodeManager codeManager;
    public RideGameController gc;

    private int counter = 0;
    private bool[] found;
    private int foundIndex;

    // Use this for initialization
    void Start()
    {

        found = new bool[points.Length];
        for (int i = 0; i < found.Length; i += 1) found[i] = false;
        foundIndex = -1;
    }

    // Update is called once per frame
    void Update()
    {
        counterText.text = counter.ToString() + " / " + points.Length.ToString();
    }


    public void FoundWaypoint(string name)
    {
        Debug.Log("Waypoint found: " + name);
        // which waypoint was found
        foundIndex = -1;
        for (int i = 0; i < points.Length; i += 1)
        {
            if (name == points[i].name)
            {
                foundIndex = i;
                break;
            }
        }
        Debug.Log("Index = " + foundIndex.ToString());
        // if it has not yet been found
        if (foundIndex > -1 && !found[foundIndex])
        {
            // start the puzzle to unlock the waypoint
            codeManager.StartPlayCode(foundIndex);
        }
    }

    public void GotCorrectCode(int index)
    {
        // called by CodeManager when a code has been solved
        Debug.Log("flipping waypoint #:" + index.ToString());
        found[index] = true;
        points[index].SendMessage("Flip");
        counter += 1;
        // have all of the waypoints been found
        if (AllFound())
        {
            // EXPERIENCE TASK COMPLETE
            gc.FoundSomething("Waypoints-All Done");
        }
    }

    private bool AllFound()
    {
        bool done = true;
        for (int i = 0; i < found.Length; i += 1)
        {
            if (!found[i])
                done = false;
            if (!done)
                break;
        }
        return done;
    }

}
