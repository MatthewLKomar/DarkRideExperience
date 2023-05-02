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
    }

    public void DecreaseHealth()
    {
        //update the canvases... 
        if (CartHealth > 0.0f)
        {
            CartHealth -= DamagePlayerAmount;
            CanvasManager.canvasManager.ShowDamage(CartHealth);
            
        } else {
            //dead
        }
    }
}
