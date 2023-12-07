using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms;
using DG.Tweening;
using UnityEngine.UI;

public class ScrollVertical : MonoBehaviour,IEndDragHandler, IDragHandler
{
    [SerializeField] ScrollViewUI scrollViewUi;
    public RectTransform contentScrollRectTest;
    public float speed;
    public bool isDirectionHorizontal;
    public bool isDirectionVertical;
    public float valueDirectionPosY;

    public ScrollRect scrollRect;
    private void Start()
    {
        valueDirectionPosY = 85;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        valueDirectionPosY = contentScrollRectTest.localPosition.y;
        isDirectionHorizontal = true;
        isDirectionVertical = true;
        scrollRect.vertical = true;
        CheckSwipeFast(eventData);
    }
    public void CheckSwipeFast(PointerEventData eventData)
    {
        if (DataPersist.playerData.endedTutorial)
            scrollViewUi.CheckSwipeFast(eventData);
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (DataPersist.playerData.endedTutorial)
        {
            float valueX = Mathf.Abs(eventData.position.x - eventData.pressPosition.x);
            float valueY = Mathf.Abs(eventData.position.y - eventData.pressPosition.y);
            if (valueX > valueY && isDirectionHorizontal)
            {
                scrollRect.vertical = false;
                scrollViewUi.contentScrollRect.DOAnchorPos(new Vector3(scrollViewUi.valuePosContent + eventData.position.x - eventData.pressPosition.x, 0, 0), 0);
                isDirectionVertical = false;
            }
            else if (valueY > valueX && isDirectionVertical)
            {
                isDirectionHorizontal = false;
            }
        }
    }
}
