using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(menuName = "NewShopCard", fileName = "Gold")]
public class UiShopGoldScriptable : ScriptableObject
{
    public string title;
    public int amount;
    public int price;
    public Sprite icon;
}
