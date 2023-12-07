using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpGradeCardPlant : MonoBehaviour
{
    public Button upGrade;
    public TextMeshProUGUI costText;
    public GameObject popUpSuccessPurchase;
    public GameObject popUpFailedPurchase;
    public LoadCardInfo loadCardInfo;
    public ScrollViewUI scrollViewUi;

    private void Awake()
    {
        upGrade.onClick.AddListener(UpGrade);
    }

    public void UpGrade()
    {
        int cost = int.Parse(costText.text);
        if (DataPersist.playerData.GetAmountGold() >= cost)
        {
            DataPersist.LevelUpCardCollection(loadCardInfo.id);
            DataPersist.playerData.SubAmountGold(cost);
            ActionUi.changeGold();
            loadCardInfo.LoadCardData();
            popUpSuccessPurchase.SetActive(true);
            AudioManager.instance.PlayShot("Ui", 1);
        }
        else
        {
            popUpFailedPurchase.SetActive(true);
            scrollViewUi.SetPosNavigatorFromChest();
        }
        
    }
}
