using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class EnemyController : MonoBehaviour, IMovable
{
    protected int index = 0;
    public float moveSpeed = 1;
    public float currentSpeed;
    public bool ismoved = false;
    public float turnSpeed = 10f;
    public Vector3[] paths;
    public bool dead = false;
    private Tween delayedTween;
    public void OnEnable()
    {
        SetValue();
    }
    public void Update()
    {
        if (ismoved && !dead)
        {
            Move();
        }
    }

    public void SetValue()
    {
        dead = false;
        index = 0;
        currentSpeed = moveSpeed;
        GetPathMove();
        ismoved = true;
    }

    public virtual void GetPathMove()
    {
        PathFinding pathFinding = PathFinding.Instance;
        GameManager gameManager = GameManager.Instance;
        for (int j = 0; j < gameManager.gatePositions.Length; j++)
        {
            if (transform.position == gameManager.gatePositions[j].position)
            {
                int lenghth = pathFinding.shortestPathList[j].points.Count;
                paths = new Vector3[lenghth];
                for (int i = 0; i < lenghth; i++)
                {
                    float offset = 0;
                    for (int k = 0; k < pathFinding.shortestPathList[j].points[i].x; k++)
                    {
                        offset += 1.4f;
                    }
                    float x = offset;
                    float y = pathFinding.shortestPathList[j].points[i].y;
                    paths[i] = new Vector3(x, 1, y);
                }
            }
        }

    }
    public void SetDead()
    {
        GameManager.Instance.EnemyCount(2);
        ObjectPool.instance.Return(gameObject);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Heart"))
        {
            dead = true;
            ObjectPool.instance.Return(gameObject);
        }
    }

    public virtual void Move()
    {
        if (index <= paths.Length - 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, paths[index], currentSpeed * Time.deltaTime);
            LookAtArrow();
            if (transform.position == paths[index])
            {
                index++;
            }
        }
    }

    public void LookAtArrow()
    {
        if (index > 1)
        {
            Vector3 direction = paths[index] - transform.position;
            if (direction != Vector3.zero)
            {
                RotateDelay(direction, turnSpeed);
            }
        }
    }
    public void RotateDelay(Vector3 direction, float turnSpeed)
    {
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0, rotation.y, 0);
    }

   public void GetSlowed(float slowPercent, float duration)
    {
        if(delayedTween != null && delayedTween.IsActive())
        {
            delayedTween.Kill();
        }
      
        SetSpeed(moveSpeed *(1- slowPercent));
        delayedTween =  DOVirtual.DelayedCall(duration, () => { currentSpeed = moveSpeed; });
    }

    public void SetSpeed(float speed)
    {
        currentSpeed = speed;
    }
}
