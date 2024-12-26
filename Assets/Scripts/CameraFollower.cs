using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform target;
    [Range(0,1)] public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public bool isAnimationStart;
    private void Start()
    {
        AudioListener.volume = 1;
    }
    private void LateUpdate()
    {
        Vector3 camPos = target.position + offset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, camPos, smoothSpeed);
        transform.position = smoothedPos;
        if(!isAnimationStart)transform.LookAt(target);
    }

    public bool isMoveFinished()
    {
        Vector3 camPos = target.position + offset;
        if (Vector3.Distance(camPos,transform.position) <0.1f)
            return true;
        return false;
    }
}
