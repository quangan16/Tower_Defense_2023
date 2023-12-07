using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletBezier : MonoBehaviour
{
    public Transform A;
    public Transform B;
    public Transform control;
    public float Y;

    public Vector3 Evaluate(float t)
    {
        control.transform.position = new Vector3((A.position.x + B.position.x)/ 2,Y, (A.position.z + B.position.z) / 2);
        Vector3 ac = Vector3.Lerp(A.position, control.position, t);
        Vector3 cb = Vector3.Lerp(control.position, B.position, t);
        return Vector3.Lerp(ac, cb, t);
    }

    // private void OnDrawGizmos()
    // {
    //     if(A == null || B == null || control == null) return;
    //
    //     for(int i = 0; i < 20; i++)
    //     {
    //         Gizmos.DrawWireSphere(Evaluate(i / 20f), 0.5f);
    //     }
    //     
    // }
}
