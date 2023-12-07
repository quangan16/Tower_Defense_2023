using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyDiamond : MonoBehaviour
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
        BuyEnoughDiamond();
    }

    public void BuyEnoughDiamond()
    {
        effectCollection.amountCoin = effectAmount;
        effectCollection.RewardCoin();
        DividePriceToAdd();
    }

    public void DividePriceToAdd()
    {
        float timeDelay = 1;
        for (int i = 0; i < 10; i++)
        {
            Invoke(nameof(DelayUpdateGoldUi), timeDelay);
            timeDelay += 0.1f;
        }
    }
    public void DelayUpdateGoldUi()
    {
        DataPersist.playerData.AddAmountDiamond(amount / 10);
        UiManager.instance.UpdateDiamondText();
    }

    public void BuyNotEnoughDiamond()
    {
        notEnoughGems.SetActive(true);
        Invoke(nameof(DeActivePopUp), 1);
    }

    public void DeActivePopUp()
    {
        notEnoughGems.SetActive(false);
    }
}
