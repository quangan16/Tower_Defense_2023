using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform target;
    public float speed;
    public int atk;
    public ParticleSystem explore;
    public bool isSetted = false;
    public Vector3 pointStart;
    public Rigidbody rb;
    public EnemyMovement enemyMovement;

    public void HitTarget()
    {
        target = null;
        enemyMovement = null;
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
        Vector3 direction = target.position - transform.position;
        rb.velocity = direction.normalized * speed;
    }

    private void Update()
    {
        if(target == null)
        {
            HitTarget();
        }
        else if(target != null)
        {
            if (enemyMovement == null)
            {
                enemyMovement = target.GetComponent<EnemyMovement>();
            }
            else if(enemyMovement.dead)
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
