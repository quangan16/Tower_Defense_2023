using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
public class demoBulletCannon : MonoBehaviour
{
    public GameObject player;
    public GameObject target;
    public float speed = 10f;
    public float launchHeight = 2;

    private Vector3 movePosition;
    private float nextX;
    private float nextZ;

    private float dist;
    private float height;

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = GameObject.FindGameObjectWithTag("Enemy");
    }
    void Update()
    {
        Vector3 posCurrent = Vector3.MoveTowards(transform.position, target.transform.position,speed * Time.deltaTime);
        nextX = posCurrent.x;
        nextZ = posCurrent.z;

        dist = Mathf.Abs(Vector3.Distance(player.transform.position, target.transform.position));

        float baseY_Vector3 = Mathf.Lerp(player.transform.position.y, target.transform.position.y, Mathf.Abs(Vector3.Distance(new Vector3(nextX, 0, nextZ), player.transform.position)) / dist);
        
        height = launchHeight * Vector3.Distance(new Vector3(nextX, 0, nextZ), new Vector3(player.transform.position.x, 0, player.transform.position.z)) 
        * Mathf.Abs(Vector3.Distance(new Vector3(nextX, 0, nextZ), new Vector3(target.transform.position.x, 0, target.transform.position.z))) / (0.25f * dist * dist);
        movePosition = new Vector3(nextX, 0, nextZ);
        transform.position = movePosition;

    }

    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealth>().TakeDamage(10, 1);
            //Explosion();
            DOVirtual.DelayedCall(1f, () =>
            {
                ObjectPool.instance.Return(gameObject);
            });

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
