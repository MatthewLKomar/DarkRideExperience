using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGhost : MonoBehaviour
{
    public KeyCode key;
    public PlayVideo[] videos;
    public Vector2 delayRange;

    private bool use = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key)) use = !use;

        if (use && !IsInvoking()) Invoke("PickVideo", 0.1f);

        if (!use && IsInvoking()) CancelInvoke();
    }

    void PickVideo()
    {
        int i = Random.Range(0,videos.Length*5)/5;
        Debug.Log("Playing video: " + i.ToString());
        videos[i].Play();
        float time = (float)videos[i].GetLength() + Random.Range(delayRange.x, delayRange.y);
        if (use) Invoke("PickVideo", time );
    }
}
