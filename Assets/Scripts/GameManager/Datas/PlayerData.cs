using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    //tutorial.
    public bool startedTutorial = false;
    public bool tutorialing = false;
    public bool endedTutorial = false;
    //curencies.
    private int amountHeart;
    private int amountGold;
    private int amountDiamond;

    public List<Card> cardsBattle;
    public List<Card> cardsCollection;
    public Dictionary<string, Card> cardsCollectionDic;

    public PlayerData()
    {
        InitCardsCollection();
        InitCardsBattle();
        amountHeart = 20;
        amountGold = 1000;
        amountDiamond = 1000;
    }

    public void InitCardsBattle()
    {
        cardsBattle = new List<Card>();
        for (int i = 0; i < 3; i++)
        {
            cardsBattle.Add(cardsCollection[i]);
        }
    }
    public void InitCardsCollection()
    {
        cardsCollection = new List<Card>();
        Card plant01 = new Card("plant1", 1, 100, 2, 2.5f, "PeaShooter", "Common", "Single target");
        Card plant02 = new Card("plant2", 1, 50, 1, 3.5f, "Shroom", "Common", "Five direction");
        Card plant03 = new Card("plant3", 1, 200, 1, 2.5f, "Apple", "Common", "Go through enemies");
        Card plant04 = new Card("plant4", 1, 300, 1.5f, 2.5f, "Cannon Boom", "Common", "Explode with radius");
        Card plant05 = new Card("plant5", 1, 300, 1.5f, 2.5f, "FreezeMachine", "Common", "Slow sown target");

        cardsCollection.Add(plant01);
        cardsCollection.Add(plant02);
        cardsCollection.Add(plant03);
        cardsCollection.Add(plant04);
        cardsCollection.Add(plant05);
    }

    //get and set heart.
    public void SaveHeart()
    {
        ActionUi.changeHeart?.Invoke();
        DataPersist.SetHeart();
    }
    public void SetAmountHeart(int amount)
    {
        amountHeart = amount;
        SaveHeart();
    }

    public void SubAmountHeart(int amount)
    {
        amountHeart -= amount;
        SaveHeart();
    }
    public void AddAmountHeart(int amount)
    {
        amountHeart += amount;
        SaveHeart();
    }
    public int GetAmountHeart()
    {
        return amountHeart;
    }


    //get and set diamond.
    public void SaveDiamond()
    {
        ActionUi.changeDiamond?.Invoke();
        DataPersist.SetDiamond();
    }
    public void SetAmountDiamond(int amount)
    {
        amountDiamond = amount;
        SaveDiamond();
    }

    public void SubAmountDiamond(int amount)
    {
        amountDiamond -= amount;
        SaveDiamond();
    }
    public void AddAmountDiamond(int amount)
    {
        amountDiamond += amount;
        SaveDiamond();
    }
    public int GetAmountDiamond()
    {
        return amountDiamond;
    }



    //get and set gold.
    public void SaveGold()
    {
        ActionUi.changeGold?.Invoke();
        DataPersist.SetGold();
    }
    public void SetAmountGold(int amount)
    {
        amountGold = amount;
        SaveGold();
    }

    public void SubAmountGold(int amount)
    {
        amountGold -= amount;
        SaveGold();
    }
    public void AddAmountGold(int amount)
    {
        amountGold += amount;
        SaveGold();
    }
    public int GetAmountGold()
    {
        return amountGold;
    }

    //get and set card data.
    public void SaveCardCollection()
    {
        foreach (Card card in cardsCollection)
        {
            card.SaveData();
        }
    }
    public void LoadCardCollection()
    {
        foreach (Card card in cardsCollection)
        {
            card.LoadData();
        }
    }
}
