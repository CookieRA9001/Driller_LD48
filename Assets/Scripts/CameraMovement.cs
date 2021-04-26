using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraMovement : MonoBehaviour {

    private Transform player = null;

    public bool freezeX;
    public bool freezeY;
    public bool freezeZ;
    public bool Bounds;
    public bool reverseBounds;
    public float pBoundsX;
    public float pBoundsY;
    public float nBoundsX;
    public float nBoundsY;
    public float pRBoundsX;
    public float pRBoundsY;
    public float nRBoundsX;
    public float nRBoundsY;
    public Vector3 cameraDefault;
    public Vector3 offset;
    public float smoothTime = 0.3F;
    public float smoothTime2 = 0.5F;
    private Vector3 velocity = Vector3.zero;
    private Vector3 targetPosition;
    bool firstTime;

    private void Awake()
    {
        firstTime = true;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().frozen = true;
    }

    void FixedUpdate()
    {

        // Define a target position above and behind the target transform
        if (!freezeX && player != null)
        {
            targetPosition.x = player.position.x + offset.x;
        } else
        {
            targetPosition.x = transform.position.x;
        }
        if (!freezeY && player != null)
        {
            targetPosition.y = player.position.y + offset.y;
        }
        else
        {
            targetPosition.y = transform.position.y;
        }
        if (!freezeZ && player != null)
        {
            targetPosition.z = player.position.z + offset.z;
        }
        else
        {
            targetPosition.z = transform.position.z;
        }

        if (Bounds)
        {
            if (targetPosition.x > pBoundsX)
            {
                targetPosition.x = pBoundsX;
            }
            else if (targetPosition.x < nBoundsX)
            {
                targetPosition.x = nBoundsX;
            }
            if (targetPosition.y > pBoundsY)
            {
                targetPosition.y = pBoundsY;
            }
            else if (targetPosition.y < nBoundsY)
            {
                targetPosition.y = nBoundsY;
            }
        }

        if (reverseBounds)
        {
            if (targetPosition.x < pRBoundsX && targetPosition.x > nRBoundsX)
            {
                targetPosition.x = cameraDefault.x;
            }
            if (targetPosition.y < pRBoundsY && targetPosition.y > nRBoundsY)
            {
                targetPosition.y = cameraDefault.y;
            }
        }


        // Smoothly move the camera towards that target position
        if (firstTime) {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime2);
            if ((float)Vector2.Distance(transform.position, targetPosition) < 0.1f) {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().frozen = false;
                firstTime = false;
            }
        } else {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}
