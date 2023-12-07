using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimationPopUp : MonoBehaviour
{
    public Vector3 localScale;
    public float duration;
    public Ease ease;
    private void OnEnable()
    {
        transform.localScale = localScale;
        transform.DOScale(Vector3.one, duration).SetEase(ease);
    }
    private void OnDisable()
    {
        transform.DOKill();
    }
}
