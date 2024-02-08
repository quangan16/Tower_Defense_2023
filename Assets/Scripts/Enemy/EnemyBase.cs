using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public int index = 0;
    public float moveSpeed;
    public bool ismoved = false;
    public float turnSpeed = 10f;
    public TextMeshProUGUI healthText;
    public Vector3[] path;
    public float speedValue;
    public bool takeDamage = false;
    public ParticleSystem[] explores;
    public bool dead = false;
    public Animator animator;
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer1;
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer2;
    [SerializeField] private float blinkIntensity;
    [SerializeField] private float blinkDuration;
    private float blinkTimer;
    public virtual void OnEnable()
    {
        SetValue();
    }
    public void SetValue()
    {
        dead = false;
        healthText.gameObject.SetActive(true);
        int maxHealth1 = GameManager.Instance.waveCount * maxHealth;
        SetMaxHealth(maxHealth1);
        index = 0;
        GetPathMove();
        ismoved = true;
    }

    public void SetMaxHealth(int value)
    {
        health = value;
        UpdateHealthText();
    }
    public void UpdateHealthText()
    {
        if (health < 0)
        {
            healthText.text = "" + 0;
        }
        else
        {
            healthText.text = "" + health;
        }
    }

    public virtual void GetPathMove()
    {
        PathFinding pathFinding = PathFinding.Instance;
        GameManager gameManager = GameManager.Instance;
        for (int j = 0; j < gameManager.gatePositions.Length; j++)
        {
            if (transform.position == gameManager.gatePositions[j].position)
            {
                int lenghth = pathFinding.shortestPathList[j].points.Count;
                path = new Vector3[lenghth];
                for (int i = 0; i < lenghth; i++)
                {
                    float offset = 0;
                    for (int k = 0; k < pathFinding.shortestPathList[j].points[i].x; k++)
                    {
                        offset += 1.4f;
                    }
                    float x = offset;
                    float y = pathFinding.shortestPathList[j].points[i].y;
                    path[i] = new Vector3(x, 1, y);
                }
            }
        }

    }
    public virtual void Update()
    {
        blinkTimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(blinkTimer / blinkDuration);
        float intensity = (lerp * blinkIntensity) + 1;
        skinnedMeshRenderer1.material.color = Color.white * intensity;
        skinnedMeshRenderer2.material.color = Color.white * intensity;
    }
    public void LateUpdate()
    {

        if (takeDamage)
        {
            ReduceValue(0.5f, speedValue);
        }
        if (moveSpeed < 0.6f)
        {
            moveSpeed = 0.8f;
            takeDamage = false;
        }
    }
    private void ReduceValue(float valueEnd, float speed)
    {
        moveSpeed = Mathf.Lerp(moveSpeed, valueEnd, speed * Time.deltaTime);
    }
    public virtual void TakeDamage(int damage, int index)
    {
        if (!dead)
        {
            health -= damage;
            blinkTimer = blinkDuration;
            UpdateHealthText();
            explores[index].Play();
            if (health <= 0)
            {
                dead = true;
                healthText.gameObject.SetActive(false);
                animator.CrossFade("die", 0.25f, 0, 0f);
                Invoke("SetDead", 2);
            }
        }
    }
    public void SetDead()
    {
        GameManager.Instance.EnemyCount(2);
        ObjectPool.instance.Return(gameObject);
    }
    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Heart"))
        {
            dead = true;
            ObjectPool.instance.Return(gameObject);
        }
    }

    public virtual void GetPathMove1()
    {
        PathFinding pathFinding = PathFinding.Instance;
        GameManager gameManager = GameManager.Instance;
        for (int j = 0; j < gameManager.gatePositions.Length; j++)
        {
            if (transform.position == gameManager.gatePositions[j].position)
            {
                int lenghth = pathFinding.shortestPathList[j].points.Count;
                path = new Vector3[lenghth];
                for (int i = 0; i < lenghth; i++)
                {
                    float offset = 0;
                    for (int k = 0; k < pathFinding.shortestPathList[j].points[i].x; k++)
                    {
                        offset += 1.4f;
                    }
                    float x = offset;
                    float y = pathFinding.shortestPathList[j].points[i].y;
                    path[i] = new Vector3(x, 1, y);
                }
            }
        }

    }
}


