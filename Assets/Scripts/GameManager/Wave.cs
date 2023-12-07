using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Wave
{
    public int[] enemies;
    public bool[] gates;

    public Wave(int[] waves, bool[] gates)
    {
        this.enemies = waves;
        this.gates = gates;
    }
}