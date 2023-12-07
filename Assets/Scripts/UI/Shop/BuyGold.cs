using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyGold : MonoBehaviour
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
            BuyEnoughDiamond();
        }
        else
        {
            BuyNotEnoughDiamond();
        }
    }
 
    public void BuyEnoughDiamond()
    {
        effectCollection.amountCoin = effectAmount;
        effectCollection.RewardCoin();
        DataPersist.playerData.SubAmountDiamond(price);
        DividePriceToAdd();
        DOVirtual.DelayedCall(2f, () =>
        {
            UiManager.instance.UpdateGoldTextInt();
        });
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
        DataPersist.playerData.AddAmountGold(amount/10);
        UiManager.instance.UpdateGoldTextFloat();
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
