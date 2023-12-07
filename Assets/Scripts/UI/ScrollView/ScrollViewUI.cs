using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollViewUI : MonoBehaviour, IEndDragHandler
{
    [SerializeField] private int maxPage;
    [SerializeField] private int currentPage;
    [SerializeField] private Vector3 stepPage;
    [SerializeField] private Vector3 stepTabOption;
    public RectTransform contentScrollRect;
    [SerializeField] private RectTransform backGroundTab;
    [SerializeField] private RectTransform shopTab;
    [SerializeField] private RectTransform[] iconTabs;
    [SerializeField] private RectTransform[] panelScrolls;
    [SerializeField] private float timeMove;
    [SerializeField] private Ease tweenType;
    [SerializeField] private Vector3 targetPos;
    [SerializeField] private Vector3 targetPosTab;

    public float valuePosContent;
    public float value;
    public float timeSwipe;

    public void Awake()
    {
        timeSwipe = 0;
        currentPage = 2;
        targetPos = contentScrollRect.localPosition;
        targetPosTab = new Vector3(360, -45, 0);
    }
    public void NextPage()
    {
        if (currentPage < maxPage)
        {
            currentPage++;
            targetPos += stepPage;
            targetPosTab -= stepTabOption;
            MovePage();
            valuePosContent += stepPage.x;
        }
    }

    public void PreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            targetPos -= stepPage;
            targetPosTab += stepTabOption;
            MovePage();
            valuePosContent -= stepPage.x;
        }
    }
    public void NextPageByIndex(int index)
    {
        targetPos = new Vector3(index * 1000, 0, 0);
        int value = -72;
        int _currentpage = 5;
        for (int i = 2; i >= index; i--)
        {
            value += 144;
            _currentpage -= 1;
        }
        currentPage = _currentpage;
        targetPosTab = new Vector3(value, -45, 0);
        valuePosContent = targetPos.x;
        MovePage();
    }
    public void MovePage()
    {
        if (currentPage <= maxPage && currentPage >= 0)
        {
            contentScrollRect.DOAnchorPos(targetPos, timeMove).SetEase(tweenType);
            backGroundTab.DOAnchorPos(targetPosTab, timeMove).SetEase(tweenType);
            iconTabs[currentPage].DOAnchorPos(new Vector2(iconTabs[currentPage].localPosition.x, 65), timeMove).SetEase(tweenType);
            for (int i = 0; i < 5; i++)
            {
                if (i != currentPage)
                {
                    iconTabs[i].DOAnchorPos(new Vector2(iconTabs[i].localPosition.x, 0), timeMove).SetEase(tweenType);
                }
            }
        }
        AudioManager.instance.PlayShot("Ui", 0);
    }
    public void MovePageOriginalPos()
    {
        contentScrollRect.DOAnchorPos(targetPos, timeMove).SetEase(tweenType);
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            timeSwipe += Time.deltaTime;
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (DataPersist.playerData.endedTutorial)
            CheckSwipeFast(eventData);
    }
    public void CheckSwipeFast(PointerEventData eventData)
    {
        float valueX = Mathf.Abs(eventData.position.x - eventData.pressPosition.x);
        float valueY = Mathf.Abs(eventData.position.y - eventData.pressPosition.y);
        if (timeSwipe > value)
        {
            if (valueX > Screen.width / 2 && contentScrollRect.localPosition.x <= 2000 && contentScrollRect.localPosition.x >= -2000)
            {
                if (eventData.position.x > eventData.pressPosition.x)
                {

                    NextPage();
                    timeSwipe = 0;
                }
                else
                {
                    timeSwipe = 0;
                    PreviousPage();
                }
            }
            else
            {
                MovePageOriginalPos();
                timeSwipe = 0;
            }

        }
        else
        {
            if (valueX > valueY)
            {
                if (eventData.position.x > eventData.pressPosition.x)
                {

                    NextPage();
                    timeSwipe = 0;
                }
                else
                {
                    timeSwipe = 0;
                    PreviousPage();
                }
            }
            MovePageOriginalPos();
            timeSwipe = 0;
        }
    }

    public void SetPosNavigatorFromGold()
    {
        shopTab.localPosition = new Vector3(shopTab.transform.localPosition.x, 560, shopTab.transform.localPosition.z);
        NextPageByIndex(2);
    }
    public void SetPosNavigatorFromDiamond()
    {
        shopTab.localPosition = new Vector3(shopTab.transform.localPosition.x,260 , shopTab.transform.localPosition.z);
        NextPageByIndex(2);
    }
    public void SetPosNavigatorFromChest()
    {
        shopTab.localPosition = new Vector3(shopTab.transform.localPosition.x,0 , shopTab.transform.localPosition.z);
        NextPageByIndex(2);
    }
}
