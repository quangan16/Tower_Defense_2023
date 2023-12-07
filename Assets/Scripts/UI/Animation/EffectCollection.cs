using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EffectCollection : MonoBehaviour
{
    [SerializeField] private GameObject coinParent;
    public AnimationCollection animationCollection;

    public Ease ease;
    private Vector3[] InitialPos;
    public int boundOfIndex = 11;
    public Vector2 target;
    public float speedMove = 0;
    public int amountCoin;
    void Start()
    {
        InitialPos = new Vector3[boundOfIndex];
        for (int i = 0; i < amountCoin; i++) {
            InitialPos[i] = coinParent.transform.GetChild(i).position;
        }
        
    }
    public void ResetCoin()
    {
        for (int i = 0; i < amountCoin; i++)
        {
            coinParent.transform.GetChild(i).position = InitialPos[i];
            coinParent.transform.GetChild(i).localScale = Vector3.zero;
        }
    }
    [Button()]
    public void RewardCoin()
    {
        ResetCoin();
        float delayTime = 0;
        coinParent.SetActive(true);
        for (int i = 0; i < amountCoin; i++)
        {
            coinParent.transform.GetChild(i).DOScale(1f, Random.Range(0.2f, 0.5f)).SetEase(ease);
            coinParent.transform.GetChild(i).GetComponent<RectTransform>().DOAnchorPos(target, speedMove).SetDelay(delayTime).SetEase(Ease.InBack);
            coinParent.transform.GetChild(i).DOScale(0f, speedMove).SetDelay(delayTime + 0.8f).SetEase(ease);
            Invoke(nameof(DelayAnimationCollection), delayTime + 1);
            delayTime += 0.1f;
        }
    }

    public void DelayAnimationCollection()
    {
        animationCollection.CollectionAnim();
    }
}
