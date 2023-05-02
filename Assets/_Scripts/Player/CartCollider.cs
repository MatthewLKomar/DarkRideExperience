using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CartCollider : MonoBehaviour
{
    public bool transitionlevel = true;
    public bool transitionAfterLoop = false;
    public int HowManyLoops = 2;
    private int loopAmount = 0;
    public float speedModifier = 0.0f;
    public bool isEndGame = false;

    private void OnTriggerEnter(Collider other) {

        if (other.transform.tag == "Player") {
            if (transitionlevel) {
                if (transitionAfterLoop)
                {
                    loopAmount += 1;
                    if (loopAmount == HowManyLoops)
                    {
                        SceneManager.LoadScene("scenes/Map2");
                    }
                }
                else
                    SceneManager.LoadScene("scenes/Map2");

            }
            else {
                //stop the cart!
                PathFollower.follower.Speed = speedModifier;
                if (isEndGame)
                    GameState.gameState.EndGame(true);
            }

        }
    }
}
