using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomButtonTutorial : MonoBehaviour
{
    public GameObject[] uiPlantSlot;
    public Transform pos;
    public Transform gridPlants;
    public int countClick = 0;
    public void RanDomPlant()
    {
        for (int i = 0; i < gridPlants.childCount; i++)
        {
            Transform childTransform = gridPlants.transform.GetChild(i);
            if (childTransform.childCount == 0 && GameManager.Instance.coin >= 3)
            {
                GameManager.Instance.coin -= 3;
                UiManagerInGame.instance.coinText.SetText(GameManager.Instance.coin.ToString());
                GameObject ui = null;
                int index = Random.Range(0, 3);
                if (countClick < 3)
                {
                    if(countClick == 0)
                    {
                        ui = Instantiate(uiPlantSlot[1], pos.transform);
                        countClick++;
                    }
                    else if(countClick == 1)
                    {
                        ui = Instantiate(uiPlantSlot[0], pos.transform);
                        countClick++;
                    }
                    else if(countClick == 2)
                    {
                        ui = Instantiate(uiPlantSlot[0], pos.transform);
                        countClick++;
                    }
                }
                else
                {
                    ui = Instantiate(uiPlantSlot[index], pos.transform);
                }
                
                ui.transform.SetParent(childTransform);
                ui.transform.localScale = Vector3.one;
                return;
            }
        }
    }
}
