using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiGoldShop : MonoBehaviour
{
    public BuyGold buyGold;
    public BuyDiamond buyDiamond;
    public UiShopGoldScriptable scriptableCardShop;
    public TextMeshProUGUI title;
    public TextMeshProUGUI amount;
    public TextMeshProUGUI price;
    public Image icon;

    //upgrade.
    public void ShowPopUpAccurate()
    {
        title.SetText(scriptableCardShop.title);
        amount.SetText(scriptableCardShop.amount.ToString());
        price.SetText(scriptableCardShop.price.ToString());
        icon.sprite = scriptableCardShop.icon;
        buyGold.amount = int.Parse(amount.text);
        buyGold.price = int.Parse(price.text);
        buyDiamond.amount = int.Parse(amount.text);
        buyDiamond.price = int.Parse(price.text);
    }
}
