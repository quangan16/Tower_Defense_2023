using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyHeart : MonoBehaviour
{
    public Button buy;
    public int amount;
    public int price;
    [SerializeField] private EffectCollection effectCollection; 
    public GameObject notEnoughGems;
    public int effectAmount;

    private void Awake()
    {
        buy.onClick.AddListener(Buy);
    }

    public void Buy()
    {
        if (DataPersist.playerData.GetAmountDiamond() >= price)
        {
            BuyWithDiamond();
        }
        else
        {
            BuyNotEnoughDiamond();
        }
    }

    public void BuyWithDiamond()
    {
        effectCollection.amountCoin = effectAmount;
        effectCollection.RewardCoin();
        DataPersist.playerData.SubAmountDiamond(price);
        DividePriceToAdd();
    }

    public void BuyNotEnoughDiamond()
    {
        notEnoughGems.SetActive(true);
        Invoke(nameof(DeActivePopUp), 1);
    }
    public void DividePriceToAdd()
    {
        float timeDelay = 1;
        for (int i = 0; i < amount; i++)
        {
            Invoke(nameof(DelayUpdateHeartUi), timeDelay);
            if(amount == 25)
            {
                timeDelay += 0.04f;
            }
            else
            {
                timeDelay += 0.1f;
            }
            
        }
    }
    public void DelayUpdateHeartUi()
    {
        DataPersist.playerData.AddAmountHeart(1);
        UiManager.instance.UpdateHeartText();
    }

    public void DeActivePopUp()
    {
        notEnoughGems.SetActive(false);
    }
}
