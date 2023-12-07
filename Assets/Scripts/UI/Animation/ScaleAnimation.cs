using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
public class ScaleAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IPointerExitHandler
{
    public RectTransform rectTransform;
    public float min, max;
    public float duration;
    public Ease ease;
    public void OnPointerDown(PointerEventData eventData)
    {
        ScaleMin();
    }
    public void OnPointerUp(PointerEventData eventData)
    {

        
    }
    private void OnDisable()
    {
        rectTransform.DOKill();
    }
    public void ScaleMin()
    {
        if (rectTransform != null)
            rectTransform.transform.DOScale(min, duration);
        
    }
    public void ScaleMax()
    {
        if (rectTransform != null)
            rectTransform.transform.DOScale(max, duration).SetEase(ease);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.instance.PlayShot("Ui", 0);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ScaleMax();
    }
}
