using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UiPlantSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
/*        if(transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            UiPlant createObjectOnUI = dropped.GetComponent<UiPlant>();
            createObjectOnUI.parentAfterDrag = transform;
        }*/
        
    }
}
