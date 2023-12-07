using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet01 : Bullet
{
    private void FixedUpdate()
    {
        if(enemyMovement != null)
        FollowTarget();
    }
}
