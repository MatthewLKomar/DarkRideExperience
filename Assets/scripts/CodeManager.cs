using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeManager : MonoBehaviour
{
    public AudioClip[] audioclips;
    public AudioClip starterClip;
    public string[] codes;  // list of strings of clip indexs 
    [Header("Don't change these")]
    public GameObject notObj;   // who to tell when a code has been solved
    public AudioSource audioSource;
    public CameraMovement2 move;

    private bool[] solved;
    private bool isPlayingCode;
    private bool isWaitingCode;
    private int activeCode;         // what code was last played and waiting for to be played back
    private int activeIndex;        // the index within the active code that we are wainting for
    private int[] activeTones;      // the indexs of the tones within the active code 

    //private bool intercomGuide = false;     // check to see if intercom has already spoken about the orb

    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Reset()
    {
        solved = new bool[codes.Length];
        for (int i = 0; i < solved.Length; i += 1) solved[i] = false;
        isPlayingCode = false;
        isWaitingCode = false;
        activeCode = -1;
        activeIndex = -1;
    }

    private bool AllSolved()
    {
        bool done = true;
        for (int i = 0; i < solved.Length; i += 1)
        {
            if (!solved[i])
                done = false;
            if (!done)
                break;
        }
        return done;
    }

    public void PlayTone(int index)
    {
        audioSource.clip = audioclips[index];
        audioSource.Play();
    }

    public void CheckTone(int index)
    {
        // called by TriggerControl via the joysticks
        PlayTone(index);
        if (isWaitingCode)
        {
            //PlayTone(index);
            if (index == activeTones[activeIndex])
            {
                // got a match
                activeIndex += 1;   // goto the next tone
                // are we done
                if (activeIndex >= activeTones.Length)
                {
                    solved[activeCode] = true;      // mark this code solved
                    if (notObj != null) notObj.SendMessage("GotCorrectCode", activeCode);   // tell TrackWaypoints.cs 
                    isWaitingCode = false;
                    move.StartMovement(); // allow guests to move again

                    
                }
            }
            else
            {
                // wrong tone - try again
                isWaitingCode = false;
                StartCoroutine(PlayCode(activeCode));
            }
        }
    }

    private int[] ConvertCode(int index)
    {
        int[] indexs;
        string code = codes[index];
        print(code);
        indexs = new int[code.Length];
        
        for (int i = 0; i < code.Length; i += 1)
            indexs[i] = int.Parse(code[i].ToString());

        return indexs;
    }

    public void StartPlayCode(int index)
    {
        if (!solved[index])
        {
            activeCode = index;     // store which code are we solving
            move.StopMovement();    // prevent guests from moving
            activeTones = ConvertCode(index);   // translate string code to array of tone indexs to play and test for
            StartCoroutine(PlayCode(index));
        }
    }

    IEnumerator PlayCode(int index)
    {
        isPlayingCode = true;
        audioSource.clip = starterClip; // to mark that the playback of the code is about to begin
        audioSource.Play();
        yield return new WaitForSeconds(starterClip.length + 0.3f);    // clip length + pad
        for (int i = 0; i < activeTones.Length; i += 1)
        {
            Debug.Log("playing tone: " + activeTones[i].ToString());
            PlayTone(activeTones[i]);
            yield return new WaitForSeconds(audioclips[activeTones[i]].length + 0.3f);    // clip length + pad
        }
        //yield return new WaitForSeconds(0.5f);    // pad
        isPlayingCode = false;
        isWaitingCode = true;
        activeIndex = 0;
        yield return null;

    } 
}
