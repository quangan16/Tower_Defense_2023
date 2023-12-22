using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Plant04 : PlantBase
{
    public bulletBezier bulletBezier;
    public GameObject bulletPrefab;
    private void Update()
    {
        AddEnemy();
        RemoveEnemy();
        DeActiveUpdateTarget();
        RateAttack2();
    }

    public virtual void RateAttack2()
    {
        if (target != null)
        {
            if (fireCountdown <= 0)
            {
                Attack2();
                fireCountdown = 1 / attackSpeed;
            }
            fireCountdown -= Time.deltaTime;
        }
    }

    public virtual void Attack2()
    {
        animationPlant.ChangeAnimationState("attack", 0.2f, 0, 0.45f);
         //smokeBullet.Play();
        DOVirtual.DelayedCall(0.2f / attackSpeed, () =>
        {
            Shoot();

                DOVirtual.DelayedCall(0.5f / attackSpeed, () => {
                animationPlant.ChangeAnimationState("idle", 0.25f, 0, 0f);
            });
        });
    }
    public void Shoot()
    {
        if(target != null)
        {
            GameObject newBullet = Instantiate(bulletPrefab);
            bulletBezier.B = target.transform;
            newBullet.GetComponent<LaunchBulletBezier>().bulletRule = bulletBezier;
        }

    }

}