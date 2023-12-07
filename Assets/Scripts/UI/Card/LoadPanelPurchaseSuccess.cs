using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadPanelPurchaseSuccess : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI namePlant;
    public TextMeshProUGUI level;
    public LoadCardInfo loadCardInfo;

    private void OnEnable()
    {
        icon.sprite = loadCardInfo.icon.sprite;
        namePlant.SetText(loadCardInfo.namePlant.text);
        level.SetText(loadCardInfo.level.text);
    }
}
