using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotDropItem : MonoBehaviour, IDropHandler
{
    public RectTransform rectransform;
    public void OnDrop(PointerEventData eventData)
    {
        GameObject itemDrop = eventData.pointerDrag;
        if (transform.childCount == 0)
        {
            if (itemDrop != null)
            {
                if (itemDrop.GetComponent<CardPowerUpMove>() != null)
                {
                    CardPowerUpMove cardPowerUpMove = itemDrop.GetComponent<CardPowerUpMove>();
                    cardPowerUpMove.oriParent = transform;
                    cardPowerUpMove.prePos = rectransform.anchoredPosition;
                    itemDrop.GetComponent<RectTransform>().anchoredPosition = rectransform.anchoredPosition;
                }
            }
        }
    }
}
