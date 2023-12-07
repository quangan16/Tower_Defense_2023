using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTestBulletCanon : MonoBehaviour
{
    public GameObject prefab;
    [Button()]
    public void SpawnBulletCannon()
    {
        GameObject bullet = Instantiate(prefab, transform.position, Quaternion.identity);
        demoBulletCannon bulletScripts =  bullet.GetComponent<demoBulletCannon>();
        bulletScripts.transform.position = transform.position;
        bulletScripts.player = gameObject;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SpawnBulletCannon();
        }
    }
}
