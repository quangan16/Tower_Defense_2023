using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public UiManagerInGame uiManagerInGame;

    public Transform[] gatePositions;
    public int waveCount;
    public List<Wave> waves;
    public bool isFighting = false;

    public float spawnRate = 1.0f;
    //public GameObject iconGate;
    public int coin = 10;
    public int live = 3;
    [HideInInspector]
    public int maxLive = 0;

    public int countEnemyExcept = 0;
    public int countEnemySpawned = 0;

    public List<GameObject> enemySpawned;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        enemySpawned = new List<GameObject>();
        uiManagerInGame = UiManagerInGame.instance;
        uiManagerInGame.UpdateGateText();
        maxLive = live;
        DataPersist.playerData.SubAmountHeart(5);
    }

    public void SetGameSpeed()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 2;
            uiManagerInGame.SetTextSpeed("2X");
        }
        else
        {
            Time.timeScale = 1;
            uiManagerInGame.SetTextSpeed("1X");
        }
    }
    public void OnSpawnWave()
    {
        countEnemySpawned = 0;
        StartCoroutine(SpawnWave());
        countEnemyExcept = waves[waveCount - 1].enemies.Length * GateOpening();
        uiManagerInGame.SetActiveFightingPanel(true);
        isFighting = true;
        PathFinding.Instance.DestroyMarkers();
    }
    public int GateOpening()
    {
        int countGateOpening = 0;
        for (int j = 0; j < waves[waveCount - 1].gates.Length; j++)
        {
            if (waves[waveCount - 1].gates[j])
            {
                countGateOpening++;
            }
        }
        return countGateOpening;
    }
    IEnumerator SpawnWave()
    {
        uiManagerInGame.UpdateWaveCountText(waveCount);
        if (waveCount - 1 < waves.Count)
        {
            for (int i = 0; i < waves[waveCount - 1].enemies.Length; i++)
            {
                for (int j = 0; j < waves[waveCount - 1].gates.Length; j++)
                {
                    if (waves[waveCount - 1].gates[j])
                    {
                        SpawnEnemy(waves[waveCount - 1].enemies[i], gatePositions[j]);
                        countEnemySpawned++;
                        yield return new WaitForSeconds(spawnRate);
                    }
                }
            }
        }
    }

    public void SpawnEnemy(int i, Transform trasPos)
    {
        GameObject newEnemy = ObjectPool.instance.GetFromObjectPool(ObjectPool.instance.enemies[i], trasPos.position);
        EnemyController enemy = newEnemy.GetComponent<EnemyController>();
        enemy.SetValue();
        Vector3 direction = enemy.paths[1] - newEnemy.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(newEnemy.transform.rotation, lookRotation, 2).eulerAngles;
        newEnemy.transform.rotation = Quaternion.Euler(0, rotation.y, 0);
        enemySpawned.Add(newEnemy);
    }

    public void FightingPanel(bool isActive)
    {
        uiManagerInGame.SetActiveFightingPanel(isActive);
        isFighting = isActive;
        uiManagerInGame.UpdateGateText();
    }

    public void EnemyCount(int length)
    {
        if (countEnemySpawned == countEnemyExcept && isFighting)
        {
            if(waveCount == waves.Count)
            {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                if (enemies.Length < length)
                    uiManagerInGame.SetActiveVictory(true);
            }
            else
            {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                if (enemies.Length < length)
                {
                    if (waveCount < waves.Count)
                    {
                        GameManager.Instance.waveCount++;
                        StopAllCoroutines();
                        countEnemySpawned = 0;
                    }
                    uiManagerInGame.UpdateWaveCountText(waveCount);
                    UiManagerInGame.instance.UpdateCoinText(10);
                    FightingPanel(false);
                    PathFinding.Instance.UpdatePath();
                }
            }
        }
    }

    public void ReloadScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void Pause()
    {
        Time.timeScale = 0;
    }
    public void UnPause()
    {
        Time.timeScale = 1;
    }
    public void LoadHome()
    {
        _SceneManager.instance.LoadScene(0);
    }

    public void LoadEndGame()
    {
        GamePersist.instance.showedResultPanel = true;
        LoadHome();
    }
    private void OnDisable()
    {
        transform.DOKill();
    }
}