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
        AddEnemy();
        RemoveEnemy();
        DeActiveUpdateTarget();
        RateAttack();
    }
    public override void DelayAnimationAttack()
    {
        
        GameObject objBullet = ObjectPool.instance.GetFromObjectPool(ObjectPool.instance.bullets[indexBulletPrefab], firePoint.position);
        Bullet03 bullet = objBullet.GetComponent<Bullet03>();
        if (bullet != null)
        {
            
            bullet.SetDirection(transform.position, target.transform.position);
           
            bullet.enemyController = target.GetComponent<EnemyController>();
            bullet.atk = atk;
        }

        Invoke(nameof(DelayIdleAnimation), 0.5f / attackSpeed);
    }
}