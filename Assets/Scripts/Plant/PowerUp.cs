using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PlantBase plantBase;
    public List<CardPowerUpMove> cardPowerUpAvailable = new List<CardPowerUpMove>();
    public void BuffDame(float percent)
    {
        plantBase.atk = (int)(plantBase.atk * percent);
    }
    public void BuffAttackSpeed(float percent)
    {
        plantBase.attackSpeed *= percent;
    }
    public void BuffRange(float percent)
    {
        plantBase.range *= percent;
    }

    public void Buff(float percent, string buff)
    {
        switch (buff)
        {
            case "dame":
                BuffDame(percent);
                break;
            case "attackspeed":
                BuffAttackSpeed(percent);
                break;
            case "range":
                BuffRange(percent);
                break;
            default:
                break;

        }
    }
}
