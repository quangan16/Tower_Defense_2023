using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDetail : MonoBehaviour
{
    private float timer;
    public string popular;
    public string description;
    public Sprite iconSprite;
    public PlantBase plantBase;
    public string namePlant;
    private void OnMouseDown()
    {
        timer = Time.time;
    }
    private void OnMouseUp()
    {
        if(Time.time - timer < 0.2f)
        {
            UiManagerInGame.instance.ShowDetailPanel(plantBase, namePlant, description, iconSprite);
        }
    }
}
