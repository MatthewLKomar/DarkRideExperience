using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CartCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.transform.tag == "Player") {
            SceneManager.LoadScene("scenes/Map2");
        }
    }
}
