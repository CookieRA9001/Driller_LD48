using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Camera mainCam;
    public float shakeStrength;

    private void Awake()
    {
        if (mainCam == null) mainCam = Camera.main;
    }

    public void Shake( float shakeDuration, float hz){
        InvokeRepeating("startShake", 0, hz);
        Invoke("endShake", shakeDuration);
    }

    void startShake(){
        Vector3 cameraPos = mainCam.transform.position;

        float shakeX = (Random.value * 2 - 1) * shakeStrength;
        float shakeY = (Random.value * 2 - 1) * shakeStrength;
        cameraPos.x += shakeX;
        cameraPos.y += shakeY;
        mainCam.transform.position = cameraPos;
    }

    void endShake(){
        CancelInvoke("startShake");
        mainCam.transform.localPosition = Vector3.zero;
    }
}
