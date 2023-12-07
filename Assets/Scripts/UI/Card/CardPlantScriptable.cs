using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "NewCardPlant", fileName = "CardPlant")]
public class CardPlantScriptable : ScriptableObject
{
    public string namePlant;
    public string popularLevel;
    public string description;
    public Sprite icon;
    public int level;
    public int atk;
    public float attackSpeed;
    public float range;
}
