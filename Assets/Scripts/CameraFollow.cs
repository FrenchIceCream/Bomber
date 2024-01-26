using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime =  0.3f;
    public float z_coord = 0;
    public Vector3 offset;
    private Vector3 velocity = Vector3.zero;

    public float leftBound;
    public float rightBound;

    void Start()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position + offset;
            targetPosition.x = leftBound;
            targetPosition.z = z_coord;
            transform.position = targetPosition;
        }
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position + offset;
            targetPosition.z = z_coord;
            if (targetPosition.x < leftBound)
                targetPosition.x = leftBound;
            else if (targetPosition.x > rightBound)
                targetPosition.x = rightBound;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}
