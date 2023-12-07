using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomButton : MonoBehaviour
{
    public GameObject[] uiPlantSlot;
    public Transform pos;
    public Transform gridPlants;
    public void RanDomPlant()
    {
        for (int i = 0; i < gridPlants.childCount; i++)
        {
            Transform childTransform = gridPlants.transform.GetChild(i);
            if (childTransform.childCount == 0 && GameManager.Instance.coin >= 3)
            {
                GameManager.Instance.coin -= 3;
                UiManagerInGame.instance.coinText.SetText(GameManager.Instance.coin.ToString());
                int index = Random.Range(0, 5);
                GameObject ui = Instantiate(uiPlantSlot[index],pos.transform);
                ui.transform.SetParent(childTransform);
                ui.transform.localScale = Vector3.one;
                return;
            }
        }
    }
}
