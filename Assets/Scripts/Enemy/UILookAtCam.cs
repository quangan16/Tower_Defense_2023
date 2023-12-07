using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookAtCam : MonoBehaviour
{
    private Transform mainCamera;
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").transform;
    }
    private void LateUpdate()
    {
        Quaternion lookRotation = Quaternion.LookRotation(mainCamera.right);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, 5).eulerAngles;
        transform.rotation = Quaternion.Euler(-90 ,rotation.y,0);
    }
}
