using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimationCollection : MonoBehaviour
{
    public Ease ease;
    public Vector3 scale;
    [Button()]
    public void CollectionAnim()
    {
        transform.DOPunchScale(scale, 0.1f).SetEase(ease);
    }
}
