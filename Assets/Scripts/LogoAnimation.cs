using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class LogoAnimation : MonoBehaviour
{
    public float pos;
    public float startDuration;
    public float endDuration;
    public Ease ease;
    public Ease easeScale;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            transform.DOScale(new Vector3(1, 1, 1), startDuration).SetEase(easeScale);
            transform.transform.DOMoveY(pos, startDuration);
            DOVirtual.DelayedCall(startDuration, () => { transform.transform.DOMoveY(0, endDuration).SetEase(ease); });
        }

        if(Input.GetKeyUp(KeyCode.P))
        {
            
        }
    }
}
