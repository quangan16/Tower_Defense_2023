using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    public Button play;
    [SerializeField] private Button playTutorial;
    public GameObject tutorialPanel;
    public Animator animatorOfScenemanager;
    //curencies text.
    public TextMeshProUGUI heartText;
    public TextMeshProUGUI diamondText;
    public TextMeshProUGUI goldText;

    //result panel.
    public GameObject resultPanel;
    //show popup.
    public GameObject popUpBuyHeart;
    public GameObject popUpCardInfo;
    public LoadCardInfo loadCardInfo;
    //logo.
    public GameObject panelLogo;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        
    }
    private void OnEnable()
    {
        AddListener();
    }
    private void OnDisable()
    {
        RemoveListener();
        transform.DOKill();
    }
    private void Start()
    {
        LoadDataUi();
        if(GamePersist.instance.showedResultPanel)
        {
            resultPanel.SetActive(true);
            GamePersist.instance.showedResultPanel = false;
        }
    }
    public void AddListener()
    {
        ActionUi.changeHeart += UpdateHeartText;
        ActionUi.changeDiamond += UpdateDiamondText;
        ActionUi.changeGold += UpdateGoldTextInt;
    }
    public void RemoveListener()
    {
        ActionUi.changeHeart -= UpdateHeartText;
        ActionUi.changeDiamond -= UpdateDiamondText;
        ActionUi.changeGold -= UpdateGoldTextInt;
    }

    public void UpdateHeartText()
    {
        heartText.text = DataPersist.playerData.GetAmountHeart().ToString() + "/ 20";
    }
    public void UpdateDiamondText()
    {
        float value = DataPersist.playerData.GetAmountDiamond();
        if (value >= 1000)
        {
            diamondText.SetText(value / 1000 + "K");
        }
        else if (value >= 1000000)
        {
            diamondText.SetText(value / 1000000 + "M");
        }
        else
        {
            diamondText.text = DataPersist.playerData.GetAmountDiamond().ToString();
        }

        
    }
    public void UpdateGoldTextInt()
    {
        float value = DataPersist.playerData.GetAmountGold();
        if(value >= 1000)
        {
            goldText.SetText(value / 1000 + "K");
        }
        else if(value >= 1000000)
        {
            goldText.SetText(value / 1000000 + "M");
        }
        else
        {
            goldText.text = DataPersist.playerData.GetAmountGold().ToString();
        }
    }
    public void UpdateGoldTextFloat()
    {
        float value = DataPersist.playerData.GetAmountGold();
        if(value >= 1000)
        {
            goldText.SetText((value / 1000).ToString("N1") + "K");
        }
        else if(value >= 1000000)
        {
            goldText.SetText((value / 1000000).ToString("N1") + "M");
        }
        else
        {
            goldText.text = DataPersist.playerData.GetAmountGold().ToString();
        }
    }

    public void CheckTutorial()
    {
        if (!DataPersist.playerData.startedTutorial)
        {
            tutorialPanel.SetActive(true);
            DataPersist.playerData.startedTutorial = true;
        }
        else
        {
            play.gameObject.SetActive(true);
            tutorialPanel.SetActive(false);
        }
    }

    public void UpdateUiCurrencies()
    {
        UpdateHeartText();
        UpdateDiamondText();
        UpdateGoldTextInt();
    }
    public void LoadDataUi()
    {
        UpdateUiCurrencies();
        DataPersist.LoadDataTutorial();
        CheckTutorial();
        _SceneManager sceneManager = _SceneManager.instance;
        sceneManager.animator = animatorOfScenemanager;
        play.onClick.AddListener(() => sceneManager.LoadSceneByChapter());
        playTutorial.onClick.AddListener(() => sceneManager.LoadTutorial());
    }

    public void ShowPopUpBuyHeart()
    {
        popUpBuyHeart.SetActive(true);
    }
    public void ShowPanelCardInfor(string id, Sprite image)
    {
       
        popUpCardInfo.SetActive(true);
        loadCardInfo.LoadCardData(id, image);
        
    }

    public void UpdateCardLevel(int level)
    {
        
    }
}
