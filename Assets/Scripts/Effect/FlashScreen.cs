using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FlashScreen : MonoBehaviour
{
    public Image white_Screen;
    public float flashTime;
    public void FlashingScreen()
    {

        white_Screen.color = new Color(.6f, .6f, .6f, 0.75f);
        Invoke("ClearScreen", flashTime);

    }
    public void ClearScreen()
    {
        //Single Flash
        white_Screen.color = Color.clear;
        //FindObjectOfType<CameraControl>().ShakeCamera(5f, .15f);
    }
}
