using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static SoundManager SM;

    public AudioSource audioSource;
    public AudioClip engageSFX;
    public AudioClip sequence1;
    public AudioClip sequence2;
    public AudioClip sequence3;
    public AudioClip sequence4;
    public AudioClip sucessSFX;
    public AudioClip failSFX;

    public AudioClip intercomIntro;
    public AudioClip intercomRowTut;
    public AudioClip intercomGetToOrb;
    public AudioClip intercomOrbTut;
    public AudioClip intercomStatic;
    public AudioClip creepyVoice;

    public CameraMovement2 moveFromSound;
    public TriggerControl tc2;

    void Awake() {
        if (SM != null)
            GameObject.Destroy(SM);
        else
            SM = this;

        DontDestroyOnLoad(this);
    }

    private void Start() {
        audioSource = GetComponent<AudioSource>();



    }
    public void StartIntro() {
        StartCoroutine(IntercomIntro());
    }
    IEnumerator IntercomIntro() {

        yield return new WaitForSeconds(1.5f);
        tc2.TriggerEvent("Trigger #2 - start tutorial");
        PlayIntercomIntroSFX();
        yield return new WaitForSeconds(intercomIntro.length +1f);
        tc2.TriggerEvent("Trigger #2 - tutorial done");


    }


    public void PlayEngageSFX() {
        audioSource.PlayOneShot(engageSFX);


    }

    public void PlayIntercomIntroSFX() {
        audioSource.PlayOneShot(intercomIntro);
    }
    public void PlayIntercomRowTutSFX() {
        audioSource.PlayOneShot(intercomRowTut);
    }
    public void PlayIntercomGetToOrbSFX() {
        audioSource.PlayOneShot(intercomGetToOrb);
    }
    public void PlayIntercomOrbTutSFX() {
        audioSource.PlayOneShot(intercomOrbTut);
    }
    public void PlayIntercomStaticSFX() {
        audioSource.PlayOneShot(intercomStatic);
    }

    public void PlayCreepyVoice() {
        audioSource.PlayOneShot(creepyVoice);
    }
}
