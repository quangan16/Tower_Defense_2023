using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Plant05 : PlantBase
{
    private void Update()
    {
        UpdateRotateGunHead();
        RateAttack();
        AddEnemy();
        RemoveEnemy();
        DeActiveUpdateTarget();
    }
}