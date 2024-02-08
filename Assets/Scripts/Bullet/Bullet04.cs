using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet04 : Bullet
{
    public float slowRate;
    public float slowDuration;
    private void OnEnable()
    {
        target = null;
    }
    void Update()
    {
        FollowTarget();
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(atk, 1);
            other.gameObject.GetComponent<EnemyHealth>().takeDamage = true;
            if (!other.gameObject.name.Contains("Enemy03"))
            {
                other.gameObject.GetComponent<EnemyController>().GetSlowed(slowRate, slowDuration);
            }
            ObjectPool.instance.Return(gameObject);
        }
    }


}
