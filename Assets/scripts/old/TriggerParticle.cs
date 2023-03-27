using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class TriggerParticle : MonoBehaviour
{
    public ParticleSystem effect;

    public string OnlyCollideWithTag;
    public UnityEvent Enter;
    public UnityEvent Exit;

    //private void OnCollisionEnter(Collision collision)
    //{
      //  effect.Play();
        
    //}

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag != OnlyCollideWithTag)
        return;
        Enter.Invoke();
        effect.Play();
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag != OnlyCollideWithTag)
            return;
        Exit.Invoke();
        effect.Stop();
    }
}
