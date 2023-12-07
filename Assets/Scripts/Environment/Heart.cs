using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GameManager gameManager = GameManager.Instance;
            if(gameManager.live > 1)
            {
                gameManager.live--;
                UiManagerInGame.instance.UpdateLiveText(gameManager.live);
                gameManager.EnemyCount(1);
            }
            else if(gameManager.live == 1)
            {
                gameManager.live--;
                GameObject[] plants = GameObject.FindGameObjectsWithTag("Hero");
                foreach (GameObject p in plants)
                {
                    p.transform.Translate(0, 20, 0);
                    DOVirtual.DelayedCall(0.5f, () => { ObjectPool.instance.Return(p); });
                }
                UiManagerInGame.instance.UpdateLiveText(gameManager.live);
                UiManagerInGame.instance.SetActiveGameOver(true);
            }
            
        }
    }

}
