using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchBulletBezier : MonoBehaviour
{
    public bulletBezier bulletRule;
    public float speed;
    public float sampleTime;

    private void Start()
    {
        sampleTime = 0f;
    }

    void Update()
    {
        sampleTime += Time.deltaTime * speed;
        transform.position = bulletRule.Evaluate(sampleTime);

        if(sampleTime >= 1)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealth>().TakeDamage(10, 1);
            Explosion();
            // if (gameObject != null)
            // {
            //     DOVirtual.DelayedCall(1f, () => { ObjectPool.instance.Return(gameObject); });
            // }
          

        }
    }

    private void Explosion()
    {
        Collider[] hitObjects = Physics.OverlapSphere(transform.position, 2);

        for (int i = 0; i < hitObjects.Length; i++)
        {
            if (hitObjects[i].CompareTag("Enemy"))
                hitObjects[i].GetComponent<EnemyHealth>().TakeDamage(10, 1);
        }
    }
}
