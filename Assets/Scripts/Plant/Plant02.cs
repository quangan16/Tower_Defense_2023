using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Plant02 : PlantBase
{
    public ParticleSystem gas;
    private void Update()
    {
        AddEnemy();
        RemoveEnemy();
        RateAttack();
        DeActiveUpdateTarget();
    }

    public override void RateAttack()
    {
        if (target != null)
        {
            if (fireCountdown <= 0)
            {
                animationPlant.ChangeAnimationState("attack", 0.08f, 0, 0.65f);
                Invoke("Shoot", 0);
                //Invoke("Shoot", 1.2f/attackSpeed);
                
                fireCountdown = 1 / attackSpeed;
            }
            fireCountdown -= Time.deltaTime;
        }
    }
    public override void Attack(){
        Collider[] enemyColliders = Physics.OverlapSphere(transform.position, range, 1 << 8);
        for (int i = 0; i < enemyColliders.Length; i++)
        {
            if (enemyColliders[i].CompareTag("Enemy"))
            {
                gas.Play();
                enemyColliders[i].GetComponent<EnemyHealth>().TakeDamage(atk, 1);
                enemyColliders[i].GetComponent<EnemyHealth>().TakePoisoned1(10, 2, 2.0f);
                enemyColliders[i].gameObject.GetComponent<EnemyHealth>().takeDamage = true;
                enemies.Remove(enemyColliders[i].gameObject);
            }
        }
    }
    //public void Attack(int value, Vector3 direction)
    //{
    //    GameObject objBullet = ObjectPool.instance.Get(ObjectPool.instance.bullets[indexBulletPrefab]);
    //    objBullet.SetActive(true);
    //    Vector3 originalVector = direction;
    //    Quaternion rotation = Quaternion.AngleAxis(value, Vector3.up);
    //    Vector3 rotatedVector = rotation * originalVector;
    //    objBullet.transform.position = firePoint.transform.position + rotatedVector.normalized * 0.6f;
    //    Bullet bullet = objBullet.GetComponent<Bullet>();
    //    if (bullet != null)
    //    {
    //        bullet.pointStart = firePoint.position;
    //        bullet.atk = atk;
    //        AudioManager.instance.PlayShot("Shroom", 0);
    //        bullet.isSetted = false;
    //        bullet.SetTarget(rotatedVector);
    //    }
    //}

  

    public void Shoot()
    {
      
       
        Attack();
    
        animationPlant.ChangeAnimationState("idle", 0.25f, 0, 0f);
    }
}
