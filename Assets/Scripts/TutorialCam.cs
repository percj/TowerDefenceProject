using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCam : MonoBehaviour
{
    [SerializeField] CameraFollower camFollower;

    public void CameraMover(Transform target,float time)
    {
        StartCoroutine(CameraMoverIEnumarator(target, time));
    }

    IEnumerator CameraMoverIEnumarator(Transform target, float time)
    {
        Time.timeScale = 0.5f;
        var tempTarget = camFollower.target;
        var tempSpeed = camFollower.smoothSpeed;
        camFollower.isAnimationStart = true;
        var tempTime = time;
        while (tempTime >= 0)
        {
            tempTime -= Time.deltaTime;
            camFollower.target = target;
            camFollower.smoothSpeed = 0.01f;
            yield return null;
        }
        Time.timeScale = 1;
        while (time >= 0)
        {
            time -= Time.deltaTime;
            camFollower.target = tempTarget;
            camFollower.smoothSpeed = 0.05f;
            yield return null;
        }
        camFollower.isAnimationStart = false;
        camFollower.smoothSpeed = tempSpeed;
    }
}
