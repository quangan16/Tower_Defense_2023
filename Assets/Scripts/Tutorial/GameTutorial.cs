using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameTutorial : MonoBehaviour
{
    public GameObject tutorialPanel;
    public GameObject popUpTutorial01;
    public GameObject popUpTutorial02;
    public Camera camera;
    public RectTransform canvas;
    public string[] contentTotorials;
    public TextMeshProUGUI contentTextBig;


    public Image imageFight;
    public Image imageButtonRandom;
    public Image imagePanelTutorial;

    public int countStep = 0;
    public int countPlant = 0;
    public int countInStep4 = 0;
    public int countCardPlants = 0;

    public GameObject ObstacleConstrainPlacePlant;
    public GameObject effectBound01;
    public GameObject effectBound02;
    public GameObject effectBound03;

    public BoxCollider effectBound01Box;
    public BoxCollider effectBound02Box;
    public BoxCollider effectBound03Box;


    public GameObject slot01;
    public GameObject slot02;
    public GameObject slot03;

    public GameObject pointStart;
    public GameObject pointStart02;
    public GameObject pointStart03;
    public GameObject pointEnd;
    public GameObject pointEnd02;
    public GameObject pointEnd03;
    public GameObject pointEnd04;

    public GameObject handTutorial;
    public Ease ease;
    public float timeAnimation;

    public GameObject handOnFight;

    public GameObject panelOnGrid;
    public int _waveCount;

    public GameObject blockContrainSecondGate;
    public BlockGround blockScriptDragPlantRequire;
    public BlockGround scriptOfBlockSecondGate;
    public BlockGround scriptOfBlockMerge01;
    public BlockGround scriptOfBlockMerge02;


    private void Start()
    {
        _waveCount = GameManager.Instance.waveCount;
    }
    public void Step01()
    {
        
        if (countStep == 0)
        {
            imageButtonRandom.raycastTarget = true;
            contentTextBig.text = contentTotorials[1];
            popUpTutorial01.SetActive(false);
            popUpTutorial02.SetActive(true);
            imagePanelTutorial.raycastTarget = false;
            imageFight.raycastTarget = false;
            countStep++;
        }
        else if(countStep == 9)
        {
            contentTextBig.text = contentTotorials[3];
            handOnFight.SetActive(true);
            imageFight.raycastTarget = true;
            imagePanelTutorial.raycastTarget = false;
        }
        else if(countStep == 14)
        {
            Step14();
        }
    }

    private Vector2 WorldToCanvasPosition(Camera camera, Transform target)
    {
        Vector3 screenPos = camera.WorldToScreenPoint(target.position);
        return screenPos;
    }

    public void Step02()
    {
        countPlant++;
        if (countPlant == 3)
        {
            contentTextBig.text = contentTotorials[2];
            popUpTutorial02.SetActive(false);
            effectBound01.SetActive(true);
            handTutorial.SetActive(true);
            handTutorial.transform.DOMove(WorldToCanvasPosition(camera, pointEnd.transform), timeAnimation).SetEase(ease).SetLoops(-1, LoopType.Restart);
            panelOnGrid.SetActive(false);
            countStep++;
        }
    }
    public void Step03()
    {
        handTutorial.transform.DOMove(pointStart02.transform.position, 0);
        handTutorial.transform.DOMove(WorldToCanvasPosition(camera, pointEnd02.transform), timeAnimation).SetEase(ease).SetLoops(-1, LoopType.Restart);
        effectBound02Box.gameObject.SetActive(false);
        effectBound02.SetActive(true);
        effectBound01.SetActive(false);
    }
    public void Step04()
    {
        handTutorial.transform.DOMove(pointStart03.transform.position, 0);
        handTutorial.transform.DOMove(WorldToCanvasPosition(camera, pointEnd03.transform), timeAnimation).SetEase(ease).SetLoops(-1, LoopType.Restart);
        effectBound03Box.gameObject.SetActive(false);
        effectBound02.SetActive(false);
        effectBound03.SetActive(true);
    }
    public void Step05()
    {
        effectBound03.SetActive(false);
        contentTextBig.text = contentTotorials[3];
        handTutorial.gameObject.SetActive(false);
        handOnFight.SetActive(true);
        imageFight.raycastTarget = true;
        countStep++;
    }
    public void Step06()
    {
        if(countStep == 6)
        {
            handOnFight.SetActive(false);
            imageFight.raycastTarget = false;
            tutorialPanel.SetActive(false);
            countStep++;
        }
    }
    public void Step07()
    {
        if(countStep == 7)
        {
            tutorialPanel.SetActive(true);
            countStep++;
            handTutorial.SetActive(true);
            contentTextBig.text = contentTotorials[4];
            imageButtonRandom.raycastTarget = false;
            handTutorial.transform.DOMove(WorldToCanvasPosition(camera, pointEnd.transform), 0);
            handTutorial.transform.DOMove(WorldToCanvasPosition(camera, pointEnd04.transform), timeAnimation).SetEase(ease).SetLoops(-1, LoopType.Restart);
        }
    }

    public void Step08()
    {
        countStep++;
        contentTextBig.text = contentTotorials[5];
        handTutorial.SetActive(false);
        imagePanelTutorial.raycastTarget = true;
        blockContrainSecondGate.SetActive(true);
    }
    public void Step09()
    {
        if(countStep == 9)
        {
            countStep++;
            tutorialPanel.SetActive(false);
            imagePanelTutorial.raycastTarget = false;
            handOnFight.SetActive(false);
        }
    }
    public void Step10()
    {
        if(countStep == 10)
        {
            countStep++;
            tutorialPanel.SetActive(true);
            imageFight.raycastTarget = false;
            contentTextBig.text = contentTotorials[6];
            effectBound03Box.gameObject.SetActive(false);
            handTutorial.SetActive(true);
            handTutorial.transform.DOMove(WorldToCanvasPosition(camera, pointEnd02.transform), 0);
            handTutorial.transform.DOMove(WorldToCanvasPosition(camera, pointEnd03.transform), timeAnimation).SetEase(ease).SetLoops(-1, LoopType.Restart);
            DataPersist.playerData.tutorialing = true;
        }
    }
    public void Step11()
    {
        if(countStep == 11)
        {
            countStep++;
            contentTextBig.text = contentTotorials[3];
            handTutorial.SetActive(false);
            handOnFight.SetActive(true);
            imageFight.raycastTarget = true;
        }
    }
    public void Step12()
    {
        if(countStep == 12)
        {
            countStep++;
            tutorialPanel.SetActive(false);
            handOnFight.SetActive(false);
        }
    }
    public void Step13()
    {
        if(countStep == 13)
        {
            countStep++;
            tutorialPanel.SetActive(true);
            contentTextBig.text = contentTotorials[7];
            imagePanelTutorial.raycastTarget = true;
        }
    }
    public void Step14()
    {
        if(countStep == 14)
        {
            countStep++;
            imageButtonRandom.raycastTarget = true;
            tutorialPanel.SetActive(false);
            imagePanelTutorial.raycastTarget = false;
            ObstacleConstrainPlacePlant.SetActive(false);
            effectBound01Box.gameObject.SetActive(false);
            effectBound02Box.gameObject.SetActive(false);
            effectBound03Box.gameObject.SetActive(false);
            DataPersist.playerData.endedTutorial = true;
            PlayerPrefs.SetInt("COMPLETETUTORIAL", 1);
        }
    }

    private void Update()
    {

        if (Input.GetMouseButtonUp(0) && countStep >= 2)
        {
            if (countStep == 2)
            {
                if (slot01.transform.childCount == 0)
                {
                    Step03();
                    countStep++;
                }
            }
            else if (countStep == 3)
            {
                if (slot02.transform.childCount == 0)
                {
                    Step04();
                    countStep++;
                }
            }
            else if (countStep == 4)
            {
                if (slot03.transform.childCount == 0)
                {
                    Step05();
                    countStep++;
                }
            }
        }

        if(countStep < 15)
        {
            if (slot01.transform.childCount == 0 && countStep == 2)
            {
                effectBound01Box.gameObject.SetActive(false);
            }
            else
            {
                effectBound01Box.gameObject.SetActive(true);
            }

            if (slot02.transform.childCount == 0 && countStep == 3)
            {
                effectBound02Box.gameObject.SetActive(false);
            }
            else
            {
                effectBound02Box.gameObject.SetActive(true);
            }

            if (slot03.transform.childCount == 0 && countStep == 4)
            {
                effectBound03Box.gameObject.SetActive(false);
            }
            else
            {
                if (countStep > 4 && countStep != 11)
                {
                    effectBound03Box.gameObject.SetActive(true);
                }
            }

        }

        if (GameManager.Instance.waveCount > _waveCount)
        {
            Step07();
            Step10();
            Step13();
            _waveCount = GameManager.Instance.waveCount;
        }

        if(countStep == 8)
        {
            if (blockScriptDragPlantRequire.aboveObjs.Count == 0)
            {
                if (blockContrainSecondGate.activeSelf)
                {
                    blockContrainSecondGate.SetActive(false);
                }
            }
            else
            {
                blockContrainSecondGate.SetActive(true);
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            if (countStep == 8 && blockScriptDragPlantRequire.aboveObjs.Count == 0 && scriptOfBlockSecondGate.aboveObjs.Count == 1)
            {
                Step08();
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            if (countStep == 11 && scriptOfBlockMerge02.aboveObjs.Count == 2 && scriptOfBlockMerge01.aboveObjs.Count == 0)
            {
                Step11();
            }
        }
        
    }

    private void OnDisable()
    {
        transform.DOKill();
    }
}

