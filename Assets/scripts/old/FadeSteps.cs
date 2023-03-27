using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeSteps : MonoBehaviour {

    public Material mat;
    public float step;
    public float delay;
    public Vector4 startColor;

    private bool fade = false;
    private int dir = 1;
    private Color c;


    // Use this for initialization
    void Start () {
        mat.color = startColor;
    }
	
	// Update is called once per frame
	void Update () {
        /*if (Input.GetKeyDown(KeyCode.Q))
        {
            FadeOut();
            //print("got it");
        }*/
    }

    IEnumerator Fade()
    {
        Color c = mat.color;
        while (fade)
        {
            c.a += step * dir;
            mat.color = c;
            print(mat.color);
            if (c.a <= 0.0f || c.a >= 1.0f)
            {
                fade = false;
            }
            else
            {
                yield return new WaitForSeconds(delay);
            }
        }
    }

    public void FadeOut()
    {
        fade = true;
        dir = -1;
        StartCoroutine(Fade());
    }

    public void FadeIn()
    {
        fade = true;
        dir = 1;
        StartCoroutine(Fade());
    }
}
