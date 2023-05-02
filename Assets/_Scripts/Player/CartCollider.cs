using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CartCollider : MonoBehaviour
{
    public bool transitionlevel = true;
    private void OnTriggerEnter(Collider other) {

        if (other.transform.tag == "Player") {
            if (transitionlevel) {
            
                SceneManager.LoadScene("scenes/Map2");
            }
            else {
                //stop the cart!
                PathFollower.follower.Speed = 0.0f;
            }

        }
    }
}
