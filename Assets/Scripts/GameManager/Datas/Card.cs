using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    public string id {  get; set; }
    public string name { get; set; }
    public int level { get; set; }
    public int atk { get; set; }
    public float attackSpeed { get; set; }
    public float range { get; set; }
    public string popular { set; get; }
    public string description { set; get; }

    public Card() { }
    public Card(string id, int level, int atk, float attatckSpeed, float range, string name, string popular, string description)
    {
        this.id = id;
        this.level = level;
        this.atk = atk;
        this.attackSpeed = attatckSpeed;
        this.range = range;
        this.name = name;
        this.popular = popular;
        this.description = description;
    }

    public void SetData(int level, int atk, float _attatckSpeed, float range)
    {
        this.level = level;
        this.atk = atk;
        this.attackSpeed = _attatckSpeed;
        this.range = range;
        SaveData();
    }
    public void LevelUp()
    {
        atk = (int)(atk * 1.2f);
        attackSpeed *= 1.1f;
        range *= 1.1f;
        level++;
        SaveData();
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt(id + "-level", level);
        PlayerPrefs.SetInt(id + "-atk", atk);
        PlayerPrefs.SetFloat(id + "-attackSpeed", attackSpeed);
        PlayerPrefs.SetFloat(id + "-range", range);
    }
    public void LoadData()
    {
        level = PlayerPrefs.GetInt(id + "-level");
        atk = PlayerPrefs.GetInt(id + "-atk");
        attackSpeed = PlayerPrefs.GetFloat(id + "-attackSpeed");
        range = PlayerPrefs.GetFloat(id + "-range");
    }


}
