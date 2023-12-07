using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardEndBattle : MonoBehaviour
{
    public Button continute;
    public int amount;
    [SerializeField] private EffectCollection effectCollection;
    private void Awake()
    {
        continute.onClick.AddListener(Buy);
    }

    public void Buy()
    {
        BuyEnoughDiamond();
    }

    public void BuyEnoughDiamond()
    {
        effectCollection.amountCoin = 10;
        effectCollection.RewardCoin();
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
        DataPersist.playerData.AddAmountGold(amount / 10);
        UiManager.instance.UpdateGoldTextFloat();
    }
}
