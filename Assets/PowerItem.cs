using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Video;

public class PowerItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public string description;
    public TextMeshProUGUI displayText;
    public void OnPointerDown(PointerEventData eventData)
    {
        displayText.text = description;
        displayText.gameObject.SetActive(true);
    }
    public void OnPointerUp(PointerEventData eventData){
        displayText.gameObject.SetActive(false);
    }
}
