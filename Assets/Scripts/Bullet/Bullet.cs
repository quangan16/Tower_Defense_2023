using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Bullet : MonoBehaviour
{
    public Transform target;
    public float speed;
    public int atk;
    public ParticleSystem explore;
    public bool isSetted = false;
    public Vector3 pointStart;
    public Rigidbody rb;
    [FormerlySerializedAs("enemyControllerBase")] [FormerlySerializedAs("enemyMovement")] public EnemyController enemyController;

    public void HitTarget()
    {
        target = null;
        enemyController = null;
        ObjectPool.instance.Return(gameObject);
    }
    public virtual void SetTarget(Transform _target)
    {
        target = _target;
    }
    public virtual void SetTarget(Transform _target, Transform _player)
    {
    }
    public virtual void SetTarget(Vector3 direction) { }

    public virtual void FollowTarget()
    {
        if (Vector3.Distance(target.position, transform.position) <= 0.1f)
        {
            HitTarget();
        }

        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            rb.velocity = direction.normalized * speed;
        }
        
    }

    private void Update()
    {
        if(target == null)
        {
            HitTarget();
        }
        else if(target != null)
        {
            if (enemyController == null)
            {
                enemyController = target.GetComponent<EnemyController>();
            }
            else if(enemyController.dead)
            {
                HitTarget();
            }
        }
    }
    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(atk, 0);
            HitTarget();
        }
    }
}
