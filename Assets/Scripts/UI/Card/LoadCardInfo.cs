using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadCardInfo : MonoBehaviour
{
    public string id;
    public TextMeshProUGUI namePlant;
    public Image icon;
    public TextMeshProUGUI popularLevel;
    public TextMeshProUGUI level;
    public TextMeshProUGUI description;
    
    public TextMeshProUGUI atk;
    public TextMeshProUGUI attackSpeed;
    public TextMeshProUGUI range;

    public void LoadCardData(string _id, Sprite image)
    {
        foreach (Card card in DataPersist.playerData.cardsCollection)
        {
            if(card.id == _id)
            {
                id = card.id;
                namePlant.SetText(card.name);
                icon.sprite = image;
                popularLevel.SetText(card.popular);
                description.SetText(card.description);
                LoadCardData();
                return;
            }
        }
    }
    public void LoadCardData()
    {
        foreach (Card card in DataPersist.playerData.cardsCollection)
        {
            if(card.id == id)
            {
                level.SetText("Level " + card.level);
                atk.SetText(card.atk.ToString());
                attackSpeed.SetText(card.attackSpeed.ToString("N2").ToString());
                range.SetText(card.range.ToString("N2").ToString());
                return;
            }
        }
    }
}
