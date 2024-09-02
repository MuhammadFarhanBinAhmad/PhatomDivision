using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public CameraControl cameraShake; // Assign this in the Inspector
    public float Intesity;
    public float Duration;
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
            TriggerShake();
    }

    void TriggerShake()
    {
        print("hit");
        cameraShake.ShakeCamera(Intesity, Duration); // Intensity of 2, duration of 0.5 seconds
    }
    
}
