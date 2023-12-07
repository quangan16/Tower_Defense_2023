using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _SceneManager : MonoBehaviour
{
    public static _SceneManager instance;
    public Animator animator;
    public int chapter;
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
        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        animator = GameObject.FindGameObjectWithTag("CrossFade").GetComponent<Animator>();
    }
    public void LoadSceneDelay()
    {
        animator.SetTrigger("start");
        Invoke("DelaySetAnimation", 1.5f);
    }
    public void DelaySetAnimation()
    {
        Time.timeScale = 1;
        LoadScene(chapter);
    }

    public void DelayLoadTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void LoadScene(int index)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(index);
    }
    public void LoadSceneByChapter()
    {
        if(DataPersist.playerData.GetAmountHeart() >= 5)
        {
            UiManager.instance.UpdateHeartText();
            DOVirtual.DelayedCall(0.2f, () => LoadSceneDelay());
        }
        else
        {
            UiManager.instance.ShowPopUpBuyHeart();
        }
    }

    public void ChangeChapter(int value)
    {
        chapter = value;
    }
    public void LoadTutorial()
    {
        animator.SetTrigger("start");
        Invoke("DelayLoadTutorial", 1f);
    }

    private void OnDisable()
    {
        transform.DOKill();
    }

    // private void Update()
    // {
    //     if(Input.GetKeyDown(KeyCode.T))
    //     {
    //         DataPersist.playerData.SetAmountHeart(20);
    //         DataPersist.playerData.SetAmountDiamond(2000);
    //         DataPersist.playerData.SetAmountGold(1000);
    //         UiManager.instance.UpdateHeartText();
    //         UiManager.instance.UpdateDiamondText();
    //     }
    //     else if(Input.GetKeyDown(KeyCode.Y))
    //     {
    //         DataPersist.ResetCardCollection();
    //     }
    // }


}