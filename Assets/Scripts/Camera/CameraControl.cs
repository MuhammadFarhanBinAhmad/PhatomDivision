using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    private CinemachineBasicMultiChannelPerlin cinemachinePerlin;
    private float shakeTimer;
    public float SlowdownTime;

    private void Start()
    {
        // Ensure that the component is correctly referenced
        cinemachinePerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        if (cinemachinePerlin == null)
        {
            Debug.LogError("CinemachineBasicMultiChannelPerlin component not found on the virtual camera!");
        }
    }

    public void ShakeCamera(float intensity, float duration)
    {
        if (cinemachinePerlin != null)
        {
            Debug.Log("ShakeCamera called with intensity: " + intensity + " and duration: " + duration);
            cinemachinePerlin.m_AmplitudeGain = intensity;
            cinemachinePerlin.m_FrequencyGain = intensity;
            shakeTimer = duration;
        }
    }
    public void SlowDownEffect()
    {
        Time.timeScale = 0.25f;
        StartCoroutine("ResetTimescale");
    }
    IEnumerator ResetTimescale()
    {
        yield return new WaitForSeconds(SlowdownTime);
        Time.timeScale = 1f;
        FindObjectOfType<FlashScreen>().FlashingScreen();

    }
    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                // Reset the shake effect
                Debug.Log("Resetting camera shake.");
                cinemachinePerlin.m_AmplitudeGain = 0f;
                cinemachinePerlin.m_FrequencyGain = 0f;
            }
        }

    }
}
