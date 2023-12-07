using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Plant03 : PlantBase
{
    private void Update()
    {
        UpdateRotateGunHead();
        RateAttack();
        AddEnemy();
        RemoveEnemy();
        DeActiveUpdateTarget();
    }
    public override void Attack()
    {
        GameObject objBullet = ObjectPool.instance.GetFromObjectPool(ObjectPool.instance.bullets[indexBulletPrefab], firePoint.position);
        Bullet bullet = objBullet.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.SetTarget(target.transform);
            bullet.atk = atk;
        }
    }
}