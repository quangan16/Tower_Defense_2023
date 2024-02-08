using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = System.Random;
using TMPro;

public class Enemy04 : EnemyController
{
    private bool hasAttacked = false;
    private static readonly int SpeedMultiplier = Animator.StringToHash("speedMultiplier");

    public override void OnEnable()
    {
        base.OnEnable();
        hasAttacked = false;
    }
    public override void Update()
    {
        base.Update();
        StartAttack();
    }
    public void StartAttack()
    {
        if (enemyHealth.health <= enemyHealth.maxHealth * 0.5f && hasAttacked == false)
        {
            currentSpeed = 0;
            enemyAnimation.ChangeStateAnimation("attack", 0.3f, 0, 0.0f);
            StartCoroutine(OnAttack());
            hasAttacked = true;
        }
    }

    public IEnumerator OnAttack()
    {
        AnimatorClipInfo currentState = enemyAnimation.animator.GetCurrentAnimatorClipInfo(0)[0];
        Debug.Log(enemyAnimation.animator.runtimeAnimatorController.animationClips[1].name);
        Collider[] plantHits = new Collider[6];
        Physics.OverlapSphereNonAlloc(transform.position, 1.75f, plantHits, 1 << 7);
        yield return new WaitForSeconds(currentState.clip.length);
        Random ran = new Random();
        for (int i = 0; i < 2; i++)
        {
            int randomPlantIndex = ran.Next(plantHits.Length -1);
            if (plantHits[randomPlantIndex] != null && plantHits[randomPlantIndex].gameObject.activeSelf)
            {
                ObjectPool.instance.Return(plantHits[randomPlantIndex].gameObject);
            }
           
        }
        // foreach (Collider plant in plantHits)
        // {
        //     ObjectPool.instance.Return(plant.gameObject);
        // }
        yield return new WaitForSeconds(currentState.clip.length );
       
        currentSpeed = moveSpeed * 1.5f;
        enemyAnimation.ChangeStateAnimation("run", 0.3f, 0, 0.0f);
            
        
        
    }
    
   
    
}