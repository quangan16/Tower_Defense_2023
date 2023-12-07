using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet03 : Bullet
{
    public Ease a;
    [SerializeField] private float radius;
    private Vector3 newTarget;
    public override void SetTarget(Transform _target)
    {
        if (!isSetted && _target != null)
        {
            base.SetTarget(_target);
            newTarget = new Vector3(_target.position.x, _target.position.y, _target.position.z);
            transform.DOJump(newTarget, 3f, 1, speed, false).SetEase(a);
            isSetted = true;
        }
    }

    public override void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealth>().TakeDamage(atk, 1);
            //Explosion();
            DOVirtual.DelayedCall(1f, () =>
            {
                isSetted = false;
                ObjectPool.instance.Return(gameObject);
            });

        }
    }
    private void Explosion()
    {
        Collider[] hitObjects = Physics.OverlapSphere(transform.position, radius);

        for (int i = 0; i < hitObjects.Length; i++)
        {
            if (hitObjects[i].CompareTag("Enemy"))
                hitObjects[i].GetComponent<EnemyHealth>().TakeDamage(atk, 1);
        }
    }
}