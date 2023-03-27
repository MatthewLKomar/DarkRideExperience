using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour {

    public GameObject fadePlane;
    public Material mat;

    private bool fadeOut = false;
    private bool fadeIn = false;
    private bool fadeInQuick = false;
    //private Material mat;
    private Color color = Color.black;

	// Use this for initialization
	void Start () {
        //mat = fadePlane.GetComponent<Renderer>().material;
        mat.color = color;
	}
	
	// Update is called once per frame
	void Update () {
		 if (fadeOut)
        {
            // fade the plane out so that we can see
            color.a -= 0.1f * Time.deltaTime;
            mat.color = color;
            //print(mat.color);
            if ( color.a <= 0f)
            {
                fadeOut = false;
            }
        } else if (fadeIn)
        {
            // fade the plane in so that we cannot see
            color.a += 0.1f * Time.deltaTime;
            mat.color = color;
            print(mat.color);
            if (color.a >= 1f)
            {
                fadeIn = false;
            }
        } else if (fadeInQuick)
        {
            // fade the plane in so that we cannot see
            color.a += 0.2f;
            mat.color = color;
            //print(mat.color);
            if (color.a >= 1f)
            {
                fadeInQuick = false;
            }
        }


    }

    public void FadeIn()
    {
        fadeIn = true;
        color = Color.clear;

    }

    public void FadeInQuick()
    {
        fadeInQuick = true;
        color = Color.clear;

    }

    public void FadeOut()
    {
        fadeOut = true;
        color = Color.black;

    }
}
