using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet03 : Bullet
{
    public Ease a;
    [SerializeField] private float radius;
    private Vector3 newTarget;
    private Vector3 direction;
    public void OnEnable()
    {
        isSetted = false;
    }
    //public override void SetTarget(Transform _target)
    //{
    //    if (!isSetted && _target != null)
    //    {
    //        base.SetTarget(_target);
           
    //    }
    //}
    private void Update()
    {
       
            FollowTarget();
    }

    public void SetDirection(Vector3 startPos, Vector3 targetPos)
    {
        direction = targetPos - startPos;
    }

    public override void FollowTarget()
    {
        rb.velocity = direction.normalized * speed;
    }

    public override void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealth>().TakeDamage(atk, 1);
            target = null;
            //Explosion();
            //DOVirtual.DelayedCall(1f, () =>
            //{
            //isSetted = false;
            //ObjectPool.instance.Return(gameObject);
            //});

        }
        if (other.gameObject.CompareTag("Bound"))
        {
            DOVirtual.DelayedCall(1f, () =>
            {
                isSetted = false;
                ObjectPool.instance.Return(gameObject);
            });
        }
    }
    //private void Explosion()
    //{
    //    Collider[] hitObjects = Physics.OverlapSphere(transform.position, radius);

    //    for (int i = 0; i < hitObjects.Length; i++)
    //    {
    //        if (hitObjects[i].CompareTag("Enemy"))
    //            hitObjects[i].GetComponent<EnemyHealth>().TakeDamage(atk, 1);
    //    }
    //}
}