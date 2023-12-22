using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataPersist
{
    public const string HEART = "HEART";
    public const string GOLD = "GOLD";
    public const string DIAMOND = "DIAMOND";
    public static PlayerData playerData = new PlayerData();
    public static int volumeMusic;
    public static int volumeSound;
    public static bool onMusic;
    public static bool onSfx;
    public static void LoadDataTutorial()
    {
        int value;
        if (PlayerPrefs.HasKey("COMPLETETUTORIAL"))
        {
            value = PlayerPrefs.GetInt("COMPLETETUTORIAL");
            if (value == 1)
            {
                playerData.startedTutorial = true;
                playerData.tutorialing = true;
                playerData.endedTutorial = true;
            }
            else
            {
                playerData.startedTutorial = false;
            }
        }
    }

    public static void LoadData()
    {
        DelayLoadData();
        if (PlayerPrefs.HasKey(HEART))
        {
            playerData.SetAmountHeart(PlayerPrefs.GetInt(HEART));
            playerData.SetAmountDiamond(PlayerPrefs.GetInt(DIAMOND));
            playerData.SetAmountGold(PlayerPrefs.GetInt(GOLD));
            playerData.LoadCardCollection();
            // volumeMusic = PlayerPrefs.GetInt("ONMUSIC");
            // volumeSound = PlayerPrefs.GetInt("ONSOUND");
        }
        else
        {
            playerData = new PlayerData();
            onMusic = true;
            onSfx = true;
            SaveData();
        }

        onMusic = (PlayerPrefs.GetInt("ONMUSIC") == 1);
        onSfx = (PlayerPrefs.GetInt("ONSOUND") == 1);
    }

    //Logo delay.
    public static void DelayLoadData()
    {
        UiManager.instance.panelLogo.SetActive(true);
        DOVirtual.DelayedCall(3, () => { UiManager.instance.panelLogo.SetActive(false); });
    }
    //change energy.
    public static void SetHeart()
    {
        PlayerPrefs.SetInt(HEART, playerData.GetAmountHeart());
    }

    //change diamond.
    public static void SetDiamond()
    {
        PlayerPrefs.SetInt(DIAMOND, playerData.GetAmountDiamond());
    }
    //change gold.
    public static void SetGold()
    {
        PlayerPrefs.SetInt(GOLD, playerData.GetAmountGold());
    }

    public static void UpdateDataCardCollection(string id, int level, int atk, float attackSpeed, float range)
    {
        foreach (Card card in playerData.cardsCollection)
        {
            if(card.id == id)
            {
                card.SetData(level, atk, attackSpeed, range);
            }
        }
    }
    public static void LevelUpCardCollection(string id)
    {
        foreach (Card card in playerData.cardsCollection)
        {
            if(card.id == id)
            {
                card.LevelUp();
            }
        }
    }

    public static void SaveData()
    {
        PlayerPrefs.SetInt("ONMUSIC", onMusic ? 1 : 0);
        PlayerPrefs.SetInt("ONSOUND", onSfx? 1 : 0);
        playerData.SaveHeart();
        playerData.SaveDiamond();
        playerData.SaveGold();
        playerData.SaveCardCollection();
    }

    public static void ResetCardCollection()
    {
        foreach (Card card in playerData.cardsCollection)
        {
            card.attackSpeed = 2;
            card.range = 2;
            card.atk = 100;
            card.level = 1;
            card.SaveData();
        }
    }
}
