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
        foreach  (Card card in cardsCollection)
        {
            cardsBattle.Add(card);
        }
    }
    public void InitCardsCollection()
    {
        cardsCollection = new List<Card>()
        {
            new Card("plant1.0", 1, 50, 0.8f, 2.0f, "PeaShooter", "Common", "Single target"),
            new Card("plant1.1", 2, 80, 1.0f, 2.25f, "PeaShooter", "Common", "Single target"),
            new Card("plant1.2", 3, 160, 1.2f, 2.25f, "PeaShooter", "Common", "Single target"),
            new Card("plant1.3", 4, 220, 1.6f, 2.5f, "PeaShooter", "Common", "Single target"),
            new Card("plant1.4", 5, 320, 2.0f, 2.5f, "PeaShooter", "Common", "Single target"),
            
            new Card("plant2.0", 1, 30, 0.8f, 2.0f, "Shroom", "Common", "Five direction"),
            new Card("plant2.1", 2, 50, 1, 2.0f, "Shroom", "Common", "Five direction"),
            new Card("plant2.2", 3, 80, 1.2f, 2.0f, "Shroom", "Common", "Five direction"),
            new Card("plant2.3", 4, 140, 1.2f, 2.5f, "Shroom", "Common", "Five direction"),
            new Card("plant2.4", 5, 200, 1.4f, 2.5f, "Shroom", "Common", "Five direction"),
            
            new Card("plant3.0", 1, 40, 0.8f, 2.5f, "Apple", "Common", "Go through enemies"),
            new Card("plant3.1", 2, 80, 0.8f, 2.5f, "Apple", "Common", "Go through enemies"),
            new Card("plant3.2", 3, 100, 1, 2.5f, "Apple", "Common", "Go through enemies"),
            new Card("plant3.3", 4, 120, 1.2f, 2.75f, "Apple", "Common", "Go through enemies"),
            new Card("plant3.4", 5, 260, 1.5f, 3.0f, "Apple", "Common", "Go through enemies"),
            
            new Card("plant4.0", 1, 80,  0.6f, 2.5f, "Cannon Boom", "Common", "Explode with radius"),
            new Card("plant4.1", 2, 120, 0.7f, 2.5f, "Cannon Boom", "Common", "Explode with radius"),
            new Card("plant4.2", 3, 160, 0.8f, 2.5f, "Cannon Boom", "Common", "Explode with radius"),
            new Card("plant4.3", 4, 280, 0.9f, 3f, "Cannon Boom", "Common", "Explode with radius"),
            new Card("plant4.4", 5, 420, 1.0f, 3.25f, "Cannon Boom", "Common", "Explode with radius"),
            
            new Card("plant5.0", 1, 40, 1.0f, 2.5f, "FreezeMachine", "Common", "Slow sown target"),
            new Card("plant5.1", 2, 60, 1.3f, 2.5f, "FreezeMachine", "Common", "Slow sown target"),
            new Card("plant5.2", 3, 120, 1.5f, 2.5f, "FreezeMachine", "Common", "Slow sown target"),
            new Card("plant5.3", 4, 180, 1.5f, 2.5f, "FreezeMachine", "Common", "Slow sown target"),
            new Card("plant5.4", 5, 260, 1.8f, 2.5f, "FreezeMachine", "Common", "Slow sown target"),
        };
        // Card plant10 = new Card("plant1.0", 1, 100, 2, 2.0f, "PeaShooter", "Common", "Single target");
        // Card plant11 = new Card("plant1.0", 1, 100, 2, 2.0f, "PeaShooter", "Common", "Single target");
        // Card plant12 = new Card("plant1.0", 1, 100, 2, 2.0f, "PeaShooter", "Common", "Single target");
        // Card plant13 = new Card("plant1.0", 1, 100, 2, 2.0f, "PeaShooter", "Common", "Single target");
        // Card plant14 = new Card("plant1.0", 1, 100, 2, 2.0f, "PeaShooter", "Common", "Single target");
        //
        // Card plant20 = new Card("plant2.0", 1, 50, 1, 3.5f, "Shroom", "Common", "Five direction");
        // Card plant21 = new Card("plant2.0", 1, 50, 1, 3.5f, "Shroom", "Common", "Five direction");
        // Card plant22 = new Card("plant2.0", 1, 50, 1, 3.5f, "Shroom", "Common", "Five direction");
        // Card plant23 = new Card("plant2.0", 1, 50, 1, 3.5f, "Shroom", "Common", "Five direction");
        // Card plant24 = new Card("plant2.0", 1, 50, 1, 3.5f, "Shroom", "Common", "Five direction");
        //
        // Card plant30 = new Card("plant3.0", 1, 200, 1, 2.5f, "Apple", "Common", "Go through enemies");
        // Card plant40 = new Card("plant4.0", 1, 300, 1.5f, 2.5f, "Cannon Boom", "Common", "Explode with radius");
        // Card plant50 = new Card("plant5.0", 1, 300, 1.5f, 2.5f, "FreezeMachine", "Common", "Slow sown target");

        
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
