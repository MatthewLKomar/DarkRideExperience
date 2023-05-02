using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    //Gets defined manually.  
    public GameObject player = null;
    
    public static GameState gameState;
    
    [Tooltip("Amount we damage the player by. Used in DecreaseHealth")]
    public float DamagePlayerAmount = 35.0f;
    public float CartHealth = 100.0f;

    public AudioClip win;
    public AudioClip lose;
    private AudioSource source; 
    private void Awake()
    {
        if (gameState != null && gameState != this)
        {
            Destroy(this);
        }
        else
        {
            gameState = this;
        }

        source = GetComponent<AudioSource>();
    }

    //MLKomar: Are ya winning son?
    public void EndGame(bool didWin)
    {
        if (didWin)
        {
            source.PlayOneShot(win);
        }
        else
        {
            source.PlayOneShot(lose);
        }
    }
    
    public void DecreaseHealth()
    {
        //update the canvases... 
        if (CartHealth > 0.0f)
        {
            CartHealth -= DamagePlayerAmount;
            CanvasManager.canvasManager.ShowDamage(CartHealth);
            
        } else
        {
            PathFollower.follower.Speed = 0.0f;
            EndGame(false); 
        }
    }
}
