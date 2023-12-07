using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelInfoPlantInGame : MonoBehaviour
{
    public bool isClicked = true;
    public GameObject panelInfoPlant;

    public TextMeshProUGUI namePlant;
    public TextMeshProUGUI atk;
    public TextMeshProUGUI attackSpeed;
    public TextMeshProUGUI range;
    public TextMeshProUGUI level;
    public TextMeshProUGUI description;
    public Image image;
    private void OnEnable()
    {
        
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && !Utils.IsPointerOverUIElement())
        {
            panelInfoPlant.SetActive(false);
        }
    }



    public void FillDataToPanel(PlantBase plantBase, string name, string _description, Sprite avatar)
    {
        gameObject.SetActive(true);
        atk.SetText(plantBase.atk.ToString());
        attackSpeed.SetText(plantBase.attackSpeed.ToString());
        range.SetText(plantBase.range.ToString());
        level.SetText("Level " + (plantBase.level + 1));
        namePlant.SetText(name);
        description.SetText(_description);
        image.sprite = avatar;
    }

}
