using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerControl : MonoBehaviour {

    [System.Serializable]
    public class AudioClips
    {
        public string name;
        public AudioClip clip;
    }
    public AudioClips[] audioClips;

    [System.Serializable]
    public class LightCueData
    {
        public string function;     // the function to be sent to the showControl
        public string group;        // the group name to be sent to the ShowControl
        [Range(0, 255)]
        public int red;             // the red value to be sent to the ShowControl
        [Range(0, 255)]
        public int green;           // the green value to be sent to the ShowControl
        [Range(0, 255)]
        public int blue;            // the blue value to be sent to the ShowControl
        [Range(0, 255)]
        public int amber;           // the amber value to be sent to the ShowControl
        [Range(0, 255)]
        public int dimmer;          // the dimmer value to be sent to the ShowControl
    }

    [System.Serializable]
    public class LightCue
    {
        public string name;         // a discriptive cue name for organization in inspector
        public LightCueData cue;     // the function to be sent to the showControl
    }

    public LightCue[] lightCues;

    [Header("Don't change these")]
	public AudioSource audio;
    public CameraMovement2 move;
    public GameObject titleScreen;
    public RideGameController gc;
    public GameObject tutorialVideo;

    private OSCController osc;
    private Dictionary<string, AudioClip> aClips;
    private Dictionary<string, LightCueData> lCues;
    private bool startPressed = false;

	// Use this for initialization
	void Start () {
        titleScreen.SetActive (true);
        osc = GameObject.Find("OSCMain").GetComponent<OSCController>();
        // build the audio dictionary from the public variables
        aClips = new Dictionary<string, AudioClip>();
        for (int i = 0; i < audioClips.Length; i++)
        {
            aClips.Add(audioClips[i].name, audioClips[i].clip);
        }
        // build a dictionary of lighting cues to make the code easier 
        lCues = new Dictionary<string, LightCueData>();
        for (int i = 0; i < lightCues.Length; i++)
        {
            lCues.Add(lightCues[i].name, lightCues[i].cue);
        }
    }

	// Update is called once per frame
	void Update () {

    }

	public void TriggerEvent(string eventName) {
		Debug.Log("in TriggerEvent with : " + eventName);
		switch (eventName)
		{
            case "Trigger #0 - Begin":
                // add lighting that will assist with letting the guests know where to stand 
                osc.SendOSCMessage(BuildLightMessage("guestLeftBlue"));  // guestLeftBlue
                StartCoroutine(DelayLights(BuildLightMessage("guestRightYellow"), 0.5f));
                break;
            case "Trigger #1 - start":
                if (!startPressed)
                {
                    Debug.Log("Started");
                    startPressed = true;
                    // let movement know that we are starting
                    move.StartButton();
                    // remove the title screen
                    titleScreen.GetComponent<FadeSphere>().FadeOut();
                    // setup lighting
                    osc.SendOSCMessage(BuildLightMessage("guestPaleOrange"));  // guestPaleOrange
                    // play audio file title - welcome 
                    StartCoroutine(PlayFile(aClips["welcome"], 0.1f));
                    // advance to next trigger - Start tutorial
                    //StartCoroutine(WaitingToTrigger("Trigger #2 - start tutorial", aClips["welcome"].length + 0.01f));  
                    // REMOVE after tutorial is added
                    move.StartMovement();
                }
                break;

            //case "Trigger #1 - skip tutorial":
            //    Debug.Log("Started but skipped the tutorial");
            //    startPressed = true;
            //    // let movement know that we are starting
            //    move.StartButton();
            //    // remove the title screen
            //    titleScreen.GetComponent<FadeSphere>().FadeOut();
            //    // remove the tutorial collider
            //    Destroy(tutorialVideo);
            //    // Guests are allowed to move throttle (joystick)
            //    move.StartMovement();
            //    // advance to next trigger - tutorial done
            //    StartCoroutine(WaitingToTrigger("Trigger #2 - tutorial done", 0.01f));
            //    break;

            //case "Trigger #2 - start tutorial":
            //    // start tutorial
            //    tutorialVideo.SendMessage("FoundSomething", "");
            //    Destroy(tutorialVideo, 32);
            //    // Guests are allowed to move throttle (joystick)
            //    move.StartMovement();
            //    // Guests are allowed to play tones
            //    gc.StartTutorial();
            //    break;

            case "Trigger #2 - tutorial done":
                gc.DoneTutorial();
                gc.StartTime();
                break;

            case "Trigger #3 - hit first trigger":
                // halt motion to hear the clip
                //move.StopMovement();
                // play audio file #2 - firstTrigger
                StartCoroutine(PlayFile(aClips["firstTrigger"], 0.01f));
                // light change
                osc.SendOSCMessage(BuildLightMessage("guestGreen"));  // guestGreen
                StartCoroutine(DelayLights(BuildLightMessage("guestPaleOrange"), aClips["firstTrigger"].length));
                //re-enable motion
                //Invoke("StartMovement", aClips["firstTrigger"].length + 0.01f);
                break;

            




            case "Trigger - Times Up":
                // halt motion to hear the clip
                move.StopMovement();
                // play audio file - endingMusic
                StartCoroutine(PlayFile(aClips["endingMusic"], 0.01f));
                // light change
                osc.SendOSCMessage(BuildLightMessage("guestRed"));  // guestRed
                StartCoroutine(ThemeLights(10f));   // wait 10 seconds to read credits and then bring up the theme lights for guests to exit
                //show credit screen
                titleScreen.GetComponent<FadeSphere>().FadeIn();
                break;
            case "Trigger - All Done":
                // halt motion to hear the clip
                move.StopMovement();
                // play audio file - failEnding
                StartCoroutine(PlayFile(aClips["failEnding"], 0.01f));
                // light change
                osc.SendOSCMessage(BuildLightMessage("guestWhite"));  // guestWhite
                StartCoroutine(ThemeLights(10f));   // wait 10 seconds to read credits and then bring up the theme lights for guests to exit
                //show credit screen
                titleScreen.GetComponent<FadeSphere>().FadeIn();
                break;

        }
	}

    void StartTime()
    {
        gc.StartTime();
    }

    void StartMovement()
    {
        // used when this call must be delayed with an Invoke
        move.StartMovement();
    }

    IEnumerator PlayFile(AudioClip clip, float delay) {
		// wait if needed
		yield return new WaitForSeconds(delay);
		// play an audio clip
		audio.clip = clip;
		audio.Play ();
	}

	IEnumerator SendObjMessage(GameObject obj, string func, float delay) {
		// wait if needed 
		yield return new WaitForSeconds(delay);
		// tell the supplied obj to run the supplied function
		obj.SendMessage(func);
	}

	IEnumerator WaitingToTrigger(string trigger, float delay) {
		// wait if needed 
		yield return new WaitForSeconds(delay);
		// trigger the supplied event
		TriggerEvent(trigger);
	}

    string BuildLightMessage(string name)
    {
        string s = "/lighting " +
            lCues[name].function + " " +
            lCues[name].group + " " +
            lCues[name].red.ToString() + " " +
            lCues[name].green.ToString() + " " +
            lCues[name].blue.ToString() + " " +
            lCues[name].amber.ToString() + " " +
            lCues[name].dimmer.ToString();
        return (s);
    }

    IEnumerator DelayLights(string message, float delay)
    {
        // delay the lighting change and then set the lights
        yield return new WaitForSeconds(delay);
        osc.SendOSCMessage(message);  
    }

    IEnumerator ThemeLights(float delay)
    {
        // delay the lighting change and then set the theme lights
        yield return new WaitForSeconds(delay);
        osc.SendOSCMessage("/lighting cue Tour 3");
    }

    IEnumerator BlinkingLights(string cue1, string cue2, float delay)
	{
		// flashing colored lights - CUES SHOULD USE FUNCTION: color
		float endTime = Time.time + delay;
		while (Time.time < endTime) 
		{
            osc.SendOSCMessage(BuildLightMessage(cue1));
            yield return new WaitForSeconds(0.2f);
            osc.SendOSCMessage(BuildLightMessage(cue2));
            yield return new WaitForSeconds(0.2f);
		}
	}

}
