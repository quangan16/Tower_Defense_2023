using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class testUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    GameObject newPlant;
    public void OnPointerDown(PointerEventData eventData)
    {
        newPlant = ObjectPool.instance.GetFromObjectPool(ObjectPool.instance.plants[0].array[0], transform.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (newPlant != null)
        {
            FollowMousePos followMousePos = newPlant.GetComponent<FollowMousePos>();
            if (!followMousePos.rightPlace || followMousePos.inBound)
            {
                ObjectPool.instance.Return(newPlant);
            }
        }
        

    }
}
