using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeSphere : MonoBehaviour {

    public Material fadeOutMat; //Materials that will be faded out
    public Material fadeInMat;  //Material that will be faded in

    private bool fadeIn = false;
    private bool fadeOut = false;

    // Use this for initialization
    void Start () {
        this.gameObject.GetComponent<Renderer>().material = fadeOutMat;
        fadeOutMat.color = new Vector4(1f, 1f, 1f, 1f);
        fadeInMat.color = new Vector4(1f, 1f, 1f, 0f);
    }
	
	// Update is called once per frame
	void Update () {
        if (fadeOut == true)
        {
            Color c = fadeOutMat.color;
            c.a -= .01f;
            fadeOutMat.color = c;
            if (c.a <= 0.0f)
            {
                fadeOut = false;
            }
        }

        if (fadeIn == true)
        {
            Color c = fadeInMat.color;
            c.a += .01f;
            fadeInMat.color = c;
            if (c.a >= 1.0f)
            {
                fadeIn = false;
            }
        }
    }

    public void FadeIn()
    {
        this.gameObject.GetComponent<Renderer>().material = fadeInMat;
        fadeIn = true;
    }

    public void FadeOut()
    {
        fadeOut = true;
    }
}
