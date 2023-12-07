using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRange : MonoBehaviour
{
    public Material green;
    public Material red;
    public SpriteRenderer spriteRenderer;
    public GameObject LightBlock;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeMaterialRed()
    {
         spriteRenderer.sharedMaterial = red;
    }
    public void ChangeMaterialGreen()
    {
          spriteRenderer.sharedMaterial = green;
    }
    public void OnMaterial()
    {
          spriteRenderer.enabled = true;
          //LightBlock.SetActive(true);
    }
    public void OffMaterial()
    {
          spriteRenderer.enabled = false;
          //LightBlock.SetActive(false);
    }
}
