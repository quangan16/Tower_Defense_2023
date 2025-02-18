using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Object = UnityEngine.Object;

[System.Serializable]
public class Pool
{
    public string name;
    public GameObject go;
    public int count;
    public List<GameObject> actives;
    public Queue<GameObject> deactives;

    public Pool(string name, GameObject go, int count)
    {
        this.name = name;
        this.go = go;
        this.count = count;
    }

    public void InitaPool(Transform container, Dictionary<int, int> dicClones)
    {
        actives = new List<GameObject>();
        deactives = new Queue<GameObject>();
        for (int i = 0; i < count; i++)
        {
            spawnAClone(container, dicClones);
        }
    }

    void spawnAClone(Transform container, Dictionary<int, int> dicClones)
    {
        var clone = Object.Instantiate(go, container);
        clone.transform.localScale = Vector3.one;
        clone.name += (actives.Count + deactives.Count);
        deactives.Enqueue(clone);
        dicClones.Add(clone.GetHashCode(), GetHashCode());
    }

    public GameObject Get(Transform container, Dictionary<int, int> dicClones)
    {
        if (deactives.Count == 0)
            spawnAClone(container, dicClones);
        var clone = deactives.Dequeue();
        actives.Add(clone);
        return clone;
    }

    public void Return(GameObject go)
    {
        go.SetActive(false);
        actives.Remove(go);
        deactives.Enqueue(go);
    }

    public void OnDestroy()
    {
        foreach (var active in actives)
        {
            active.transform.DOKill();
        }

        foreach (var deactive in deactives)
        {
            deactive.transform.DOKill();
        }
    }
}
[System.Serializable]
public struct PoolPlants
{
    public Pool[] array;
}
[System.Serializable]
public struct PoolUIPlants
{
    public Pool[] array;
}

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    public Pool arrow;
    public Pool[] enemies;
    public Pool[] particleEnemies;
    [SerializeField]
    public List<PoolPlants> plants;
    public List<PoolUIPlants> uiPlants;
    public Pool[] bullets;
    public Pool[] powerUps;
    public Pool[] enemiesIcons;

    public Dictionary<int, int> dicClones = new Dictionary<int, int>();
    public List<Pool> pools = new List<Pool>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        pools.Add(arrow);
        foreach(Pool enemy in enemies)
        {
            pools.Add(enemy);
        }
        foreach(Pool powerUp in powerUps)
        {
            pools.Add(powerUp);
        }
        foreach(Pool particles in particleEnemies)
        {
            pools.Add(particles);
        }

        for (int i = 0; i < plants.Count; i++)
        {
            for (int j = 0; j < plants[i].array.Length; j++)
            {
                pools.Add(plants[i].array[j]);
            }
        }

        foreach (Pool enemyIcon in enemiesIcons)
        {
            pools.Add(enemyIcon);
        }

        for (int i = 0; i < uiPlants.Count; i++)
        {
            for (int j = 0; j < uiPlants[i].array.Length; j++)
            {
                pools.Add(uiPlants[i].array[j]);
            }
        }

        foreach (Pool bullet in bullets)
        {
            pools.Add(bullet);
        }

        foreach (var pool in pools)
        {
            pool.InitaPool(transform, dicClones);
        }
    }

    public Pool TryAddPoolByScript(Pool p)
    {
        var existedPool = pools.Find(x => x.go == p.go);
        if (existedPool != null)
        {
            Debug.LogWarning($"existed pool: {p.go.name}", p.go.transform);
            return existedPool;
        }
        pools.Add(p);
        p.InitaPool(transform, dicClones);
        return p;
    }
    
    #if UNITY_EDITOR
    private void Update()
    {
        foreach (var p in pools)
        {
            var activeCount = p.actives.Count;
            var deactiveCount = p.deactives.Count;
            p.name = $"total: {activeCount + deactiveCount} | active: {activeCount} | deactive: {deactiveCount}";
        }
    }
#endif

    public GameObject Get(Pool p)
    {
        return p.Get(transform, dicClones);
    }
    public GameObject GetFromObjectPool(Pool p, Vector3 transformPos)
    {
        GameObject newEnemy =  p.Get(transform, dicClones);
        newEnemy.SetActive(true);
        newEnemy.transform.position = transformPos;

        return newEnemy;
    }


    public void Return(GameObject clone)
    {
        clone.SetActive(false);
        clone.transform.DOKill();
        clone.transform.position = new Vector3(-10, 0, 0);
        var hash = clone.GetHashCode();
        if (dicClones.ContainsKey(hash))
        {
            var p = getPool(dicClones[hash]);
            p.Return(clone);
        }
    }
    Pool getPool(int hash)
    {
        foreach (var pool in pools)
        {
            if (pool.GetHashCode() == hash)
                return pool;
        }

        return null;
    }

    private void OnDestroy()
    {
        foreach (var pool in pools)
        {
            pool.OnDestroy();
        }
    }
}