using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPlant : MonoBehaviour
{
    public string id;
    public Button button;
    public Image icon;
    private void Start()
    {
        button.onClick.AddListener(ShowCardInfo);
    }

    public void ShowCardInfo()
    {
        UiManager.instance.ShowPanelCardInfor(id, icon.sprite);
    }

}
