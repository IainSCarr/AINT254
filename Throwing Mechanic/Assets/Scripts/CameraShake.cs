using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Camera cam;

    float shakeAmount = 0;

    void Awake()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
    }

    public void Shake(float amount, float length)
    {
        shakeAmount = amount;
        InvokeRepeating("StartShake", 0, 0.01f);
        Invoke("StopShake", length);
    }

    private void StartShake()
    {
        if (shakeAmount > 0)
        {
            Vector3 camPos = cam.transform.position;

            float offsetX = Random.value * shakeAmount * 2 - shakeAmount;
            float offsetY = Random.value * shakeAmount * 2 - shakeAmount;

            camPos.x += offsetX;
            camPos.y += offsetY;

            cam.transform.position = camPos;
        }
    }

    private void StopShake()
    {
        CancelInvoke("StartShake");
        cam.transform.localPosition = Vector3.zero;
    }
}