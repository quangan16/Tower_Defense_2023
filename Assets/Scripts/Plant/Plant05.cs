using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Plant05 : PlantBase
{
    public float slowRate;
    public float slowDuration;
    private void Update()
    {
        UpdateRotateGunHead();
        RateAttack();
        AddEnemy();
        RemoveEnemy();
        DeActiveUpdateTarget();
    }
    public override void DelayAnimationAttack()
    {
        
        base.DelayAnimationAttack();
        objBullet.TryGetComponent(out Bullet04 bullet);
        bullet.GetComponent<Bullet04>().slowRate = slowRate;
        bullet.GetComponent<Bullet04>().slowDuration = slowDuration;
    }

  
}