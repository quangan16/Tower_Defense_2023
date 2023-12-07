using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demoBulletBezier : MonoBehaviour
{
    public GameObject p0;
    public GameObject p1;
    public GameObject p2;
    public float t;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = CalculateCubicBezierPoint(t, p0.transform.position, p1.transform.position, p2.transform.position);
    }

    Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;

        return p;
    }
}
