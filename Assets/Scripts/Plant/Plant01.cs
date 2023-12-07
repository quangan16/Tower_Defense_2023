using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Plant01 : PlantBase
{
    private void Update()
    {
        UpdateRotateGunHead();
        AddEnemy();
        RemoveEnemy();
        DeActiveUpdateTarget();
        RateAttack();
    }

}