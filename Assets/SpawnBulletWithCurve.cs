using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBulletWithCurve : MonoBehaviour
{
    public GameObject prefab;
    public bulletBezier bulletBezier;
    public GameObject target;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), 0, 0.5f);
    }
    public void Spawn()
    {
        GameObject newBullet = Instantiate(prefab);
        bulletBezier.B = target.transform;
        newBullet.GetComponent<LaunchBulletBezier>().bulletRule = bulletBezier;
    }
}
