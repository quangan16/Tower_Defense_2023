using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlantBase : MonoBehaviour, IShootable
{
    public string namePlant;
    public int index;
    public int atk;
    public float attackSpeed;
    public float range;
    public GameObject objRange;
    public float fireCountdown = 0;
    public GameObject target;
    public Transform partToRotate;
    public float turnSpeed;
    public Transform firePoint;
    public List<GameObject> enemies = new List<GameObject>();
    public int indexBulletPrefab;
    public int level;
    public GameObject objBullet;
    public GameObject posGrid;
    public GameObject uiPlantPrefab;
    public GameObject ui;
    private bool createdUi;

    public AnimationPlant animationPlant;
    private void Awake()
    {
        posGrid = GameObject.Find("UITop");
    }
    private void OnEnable()
    {
        fireCountdown = 0;
        createdUi = false;
        LoadDataPlant();
        // Debug.Log(atk);
    }

    // public void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.yellow;
    //     Gizmos.DrawWireSphere(transform.position, range);
    // }
    public void LoadDataPlant()
    {
        foreach (Card plant in DataPersist.playerData.cardsBattle)
        {
            if (namePlant.CompareTo(plant.id) == 0)
            {
                // Debug.Log(plant.range);
                atk = plant.atk;
                attackSpeed = plant.attackSpeed;
                range = plant.range;
                return;
            }
        }
    }
    public virtual void UpdateRotateGunHead()
    {
        if (target != null)
        {
            Vector3 direction = target.transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            partToRotate.rotation = Quaternion.Euler(0, rotation.y, 0);
        }
    }
    public virtual void RateAttack()
    {
        if (target != null)
        {
            if (fireCountdown <= 0)
            {
                Attack();
                fireCountdown = 1 / attackSpeed;
            }
            fireCountdown -= Time.deltaTime;
        }
    }
    public void DeActiveUpdateTarget()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (!enemies[i].activeSelf || enemies[i].GetComponent<EnemyController>().dead)
            {
                enemies.Remove(enemies[i]);
                UpdateTarget();
            }
        }
    }
    public void AddEnemy()
    {
        List<GameObject> targets = GameManager.Instance.enemySpawned;
        float distanceToEnemy = 0;
        foreach (var enemy in targets)
        {
            distanceToEnemy = Vector3.Distance(objRange.transform.position, enemy.transform.position);
            if (distanceToEnemy <= range && !enemies.Contains(enemy))
            {
                enemies.Add(enemy);
                UpdateTarget();
            }
        }
    }
    public void RemoveEnemy()
    {
        if (enemies.Count > 0)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemies[i].transform.position);
                if (distanceToEnemy > range || !enemies[i].activeSelf)
                {
                    enemies.Remove(enemies[i]);
                    UpdateTarget();
                }
            }
        }
    }
    public void UpdateTarget()
    {
        if (enemies.Count >= 1)
        {
            target = enemies[0];
        }
        else
        {
            target = null;
        }
    }
    public virtual void Attack()
    {
        if (!target.GetComponent<EnemyController>().dead)
        {
            animationPlant.ChangeAnimationState("attack", 0.1f, 0, 0.45f);
            animationPlant.EmitParticle();
            Invoke(nameof(DelayAnimationAttack), 0.0f / attackSpeed);
        }
    }

    public virtual void DelayAnimationAttack()
    {
        objBullet = ObjectPool.instance.GetFromObjectPool(ObjectPool.instance.bullets[indexBulletPrefab], firePoint.position);
        Bullet bullet = objBullet.GetComponent<Bullet>();
        if (target != null)
        {
            bullet.SetTarget(target.transform);
            bullet.enemyController = target.GetComponent<EnemyController>();
            bullet.atk = atk;
            PlaySfx(0);
        }
        Invoke(nameof(DelayIdleAnimation), 0.1f / attackSpeed);
    }

    public virtual void PlaySfx(int index)
    {
        AudioManager.instance.PlayShot("PeaShooter", index);
    }

    public void DelayIdleAnimation()
    {
        animationPlant.ChangeAnimationState("idle", 0.25f, 0, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bound")
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            if (!createdUi && GetComponent<FollowMousePos>().putted)
            {
                ui = Instantiate(uiPlantPrefab, transform.position, Quaternion.identity);
                ui.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
                ui.GetComponent<BoxCollider2D>().enabled = false;
                ui.transform.SetParent(posGrid.transform, false);
                ui.GetComponent<RectTransform>().position = Input.mousePosition;
                createdUi = true;
            }
        }
    }
    void LateUpdate()
    {
        if (ui.gameObject != null)
        {
            ui.GetComponent<RectTransform>().position = Input.mousePosition;
            if (transform.GetChild(0).gameObject.activeSelf)
            {
                Destroy(ui.gameObject);
                createdUi = false;
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Bound")
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}
