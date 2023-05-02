using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CanvasManager : MonoBehaviour
{
    public static CanvasManager canvasManager;
    public TextMeshProUGUI healthStatus = null;
    public Image image1 = null;
    public Image image2 = null;
    public Image image3 = null;
    private void Awake()
    {
        if (canvasManager != null && canvasManager != this)
        {
            Destroy(canvasManager);
        }

        canvasManager = this;
    }

    public void ShowDamage(float health)
    {
        healthStatus.text = health.ToString();
        if (health > 70.0f)
        {
            image1.color = new Color(1, 1, 1, 0);
            image2.color = new Color(1, 1, 1, 0);
            image3.color = new Color(1, 1, 1, 0);
        }
        else if (health is > 50.0f and <= 70.0f)
        {
            image1.color = new Color(1, 1, 1,  .15f);
            image2.color = new Color(1, 1, 1,  .15f);
            image3.color = new Color(1, 1, 1,  .15f);
        }
        else if (health is < 50.0f and >= 30.0f)
        {
            image1.color = new Color(1, 1, 1,  .45f);
            image2.color = new Color(1, 1, 1,  .45f);
            image3.color = new Color(1, 1, 1,  .45f);

        }
        else if (health <= 30.0f)
        {
            image1.color = new Color(1, 1, 1,  .75f);
            image2.color = new Color(1, 1, 1,  .75f);
            image3.color = new Color(1, 1, 1,  .75f);
        }


    }
}
