using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    //Panel.
    public GameObject fightingPanel;
    public GameObject gameOverPanel;
    public GameObject gameVictoryPanel;

    //public GameObject iconGate;


    public GameObject[] panelWarning;
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

}
