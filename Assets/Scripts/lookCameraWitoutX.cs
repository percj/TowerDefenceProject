using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookCameraWitoutX : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(new Vector3(transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z));
    }
}
