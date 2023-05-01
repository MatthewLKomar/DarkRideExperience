using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public GameObject player = null;
    public static GameState gameState;

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
}
