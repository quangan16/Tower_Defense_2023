using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Enemy01 : EnemyBase
{
    public override void Update()
    {
        base.Update();
        if (ismoved && !dead)
        {
            Move();
        }
    }
    public void Move()
    {
        if (index <= path.Length - 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, path[index], moveSpeed * Time.deltaTime);
            LookAtArrow();
            if (transform.position == path[index])
            {
                index++;
            }
        }
    }

    public void LookAtArrow()
    {
        if (index > 1)
        {
            Vector3 direction = path[index] - transform.position;
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
}