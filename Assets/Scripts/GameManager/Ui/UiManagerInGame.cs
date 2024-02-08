using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UiManagerInGame : MonoBehaviour
{
    public static UiManagerInGame instance;
    public PanelInfoPlantInGame panelInfoPlant;
    public GameManager gameManager = GameManager.Instance;
    //UI TOP.
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI waveCountText;
    public TextMeshProUGUI[] gateTexts;

    public TextMeshProUGUI coinText;


    public TextMeshProUGUI liveText;

    public GameObject panelInfo;
    //Panel.
    public GameObject fightingPanel;
    public GameObject gameOverPanel;
    public GameObject gameVictoryPanel;
    public Image[] dangerIcons;      
    
    private List<GameObject> enemyActiveIcons;
   
    //public GameObject iconGate;


    public GameObject[] panelWarning;
    private void Awake()
    {
        enemyActiveIcons = new List<GameObject>();
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
       
        gameManager = GameManager.Instance;
        coinText.text = gameManager.coin.ToString();
        liveText.text = gameManager.live + "/" + gameManager.maxLive;
        UpdateWaveCountText(gameManager.waveCount);
        UpdateGateText();
    }
    public void ShowDetailPanel(PlantBase plantBase, string _name, string description, Sprite avatar)
    {
        panelInfoPlant.FillDataToPanel(plantBase, _name, description, avatar);
    }

    public void UpdateCoinText(int value)
    {
        gameManager.coin += value;
        coinText.text = gameManager.coin.ToString();
    }

    public void SetTextSpeed(string value)
    {
        speedText.SetText(value);
    }
    public void UpdateWaveCountText(int waveCount)
    {
        waveCountText.text = waveCount + "/ " + gameManager.waves.Count;
    }

    public void UpdateLiveText(int value)
    {
        gameManager.live = value;
        liveText.text = gameManager.live + "/" + gameManager.maxLive;
    }


    public void SetGateText(int index, string value)
    {
        gateTexts[index].SetText(value);
    }

    public void SetActiveFightingPanel(bool active)
    {
        fightingPanel.SetActive(active);
    }
    public void SetActiveGameOver(bool active)
    {
        gameOverPanel.SetActive(active);
    }
    public void SetActiveVictory(bool active)
    {
        gameVictoryPanel.SetActive(active);
    }

    public void UpdateGateText()
    {
        for (int j = 0; j < gameManager.waves[gameManager.waveCount - 1].gates.Length; j++)
        {
            if (gameManager.waves[gameManager.waveCount - 1].gates[j])
            {
                SetGateText(j, "X");
                FadeInOutEfx(dangerIcons[j]);
                //iconGate.SetActive(true);
            }
            else
            {
                
                int count = 1;
                for (int i = gameManager.waveCount; i < gameManager.waves.Count; i++)
                {
                    if (!gameManager.waves[i].gates[j])
                    {
                        count++;
                        dangerIcons[j].transform.DOKill();
                    }
                    else
                    {
                        break;
                    }
                }
                SetGateText(j, count.ToString());
                //iconGate.SetActive(false);
            }
        }
    }

    public void DisplayEnemiesNextWave()
    {
        if (enemyActiveIcons.Count > 0)
        {
            foreach (GameObject go in enemyActiveIcons)
            {
                ObjectPool.instance.Return(go);
            }
        }
        Dictionary<int, int> enemyDic = new Dictionary<int, int>();
        foreach (var id in gameManager.waves[gameManager.waveCount - 1].enemies)
        {
            if (enemyDic.ContainsKey((id)))
            {
                enemyDic[id]++;
            }
            else
            {
                enemyDic[id] = 1;
            }
        }
        
        foreach (var pair in enemyDic)
        {
            GameObject newIcon =
                ObjectPool.instance.GetFromObjectPool(ObjectPool.instance.enemiesIcons[pair.Key], panelInfo.transform.position);
            newIcon.transform.SetParent(panelInfo.transform, false);
            newIcon.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "x" + enemyDic[pair.Key].ToString();
            enemyActiveIcons.Add(newIcon);

        }
    }
    
    public void ToggleWaveInfo()
    {
        if (panelInfo.activeSelf == false)
        {
            panelInfo.SetActive(true);
        }
        else
        {
            panelInfo.SetActive((false));
        }
    }
    

    public void ShowWarning(int index)
    {
        if (panelWarning[index].activeSelf)
        {
            panelWarning[index].SetActive(false);
        }
        else
        {
            panelWarning[index].SetActive(true);
        }
    }

    public void FadeInOutEfx(Image image)
    {
        image.color = new Color(1,1,1,0.0f);
        image.DOFade(1.0f, 2.0f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

}
