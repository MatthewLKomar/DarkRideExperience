using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreen : MonoBehaviour {

    public Material mat1;       //Materials that will be faded out
    public Material mat2;       // black material
    public Material fadeInMat;  //Material that will be faded in
    

    private bool fadeIn = false;
    private bool fadeOut = false;
    private Color c;


    // Use this for initialization
    void Start () {
        mat1.color = new Vector4(1f, 1f, 1f, 1f);
        mat2.color = new Vector4(0f, 0f, 0f, 1f);
        fadeInMat.color = new Vector4(1f, 1f, 1f, 0f);
    }
	
	// Update is called once per frame
	void Update () {
        /*if (Input.GetKeyDown(KeyCode.Q))
        {
            FadeIn();
            print("got it");
        }*/

     
        if (fadeOut == true)
        {
            Color c1 = mat1.color;
            Color c2 = mat2.color;
            c1.a -= .01f;
            c2.a -= .01f;
            mat1.color = c1;
            mat2.color = c2;
            //print(mat1.color);
            if (c1.a <= 0.0f)
            {
                fadeOut = false;
            }
        }

        if (fadeIn == true)
        {
            Color c1 = fadeInMat.color;
            Color c2 = mat2.color;
            c1.a += .01f;
            c2.a += .01f;
            fadeInMat.color = c1;
            mat2.color = c2;
            //Debug.Log(fadeInMat.color);
            if (c1.a >= 1.0f)
            {
                fadeIn = false;
            }
        }


    }

    public void FadeIn()
    {
        fadeIn = true;
    }

    public void FadeOut()
    {
        fadeOut = true;
    }

}
