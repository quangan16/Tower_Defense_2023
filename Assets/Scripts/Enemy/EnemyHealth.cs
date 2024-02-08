using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public TextMeshProUGUI healthText;
    public Slider healthSlider;
    public EnemyController enemyController;
    public bool takeDamage = false;
    public EnemyAnimation enemyAnimation;
    public BoxCollider boxCollider;

    public virtual void OnEnable()
    {
        
        SetValue();
    }
    
    public void SetValue()
    {
        healthText.gameObject.SetActive(true);
        boxCollider.enabled = true;
        if (PlayerPrefs.HasKey("COMPLETETUTORIAL"))
        {
            SetMaxHealth(GameManager.Instance.waveCount * maxHealth);
        }
        else
        {
            SetMaxHealth(maxHealth);
        }
      
        
    }

    public virtual void SetMaxHealth(int value)
    {
        health = value;
        healthSlider.maxValue = value;
        UpdateHealthText();
        UpdateHealthBar();
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

    public virtual void UpdateHealthBar()
    {
        healthSlider.DOValue(health, 0.2f);
      
    }

    public void TakeDamage(int damage, int index)
    {
        if (!enemyController.dead)
        {
            health -= damage;
            StartCoroutine(enemyAnimation.Blink());
            AudioManager.instance.PlayShot("ZombieBaby", 0);
            UpdateHealthText();
            UpdateHealthBar();
            enemyAnimation.CreateVfx(index);
            if (health <= 0)
            {
                enemyController.dead = true;
                healthText.gameObject.SetActive(false);
                enemyAnimation.ChangeStateAnimation("die", 0.25f, 0, 0);
                GameManager.Instance.enemySpawned.Remove(gameObject);
                boxCollider.enabled = false;
                DOVirtual.DelayedCall(2, () => { enemyController.SetDead(); });
            }

        }
    }

    public void TakePoisoned1(int poisonDamage, int index,float duration)
    {
        StartCoroutine(TakePoisoned(poisonDamage, index, duration));
    }

    Coroutine poisonCoroutine; // Store reference to the OnPoinsoned coroutine

    public IEnumerator TakePoisoned(int poisonDamage, int index, float duration)
    {
        if (poisonCoroutine != null)
            StopCoroutine(poisonCoroutine); // Stop the ongoing OnPoinsoned coroutine
        
        enemyAnimation.OnPoisoned();
        enemyController.enemyState = EnemyState.POISONED;

        // Start the new OnPoinsoned coroutine and store its reference
        poisonCoroutine = StartCoroutine(OnPoisoned(poisonDamage, index, duration));
 
        yield return new WaitForSeconds(3.0f);
        
       
        enemyAnimation.OnNormal();
        enemyController.enemyState = EnemyState.NORMAL;
    }

    public IEnumerator OnPoisoned(int poisonDamage, int index, float duration)
    {
        while (true)
        {
            if (enemyController.enemyState == EnemyState.POISONED && !enemyController.dead)
            {
                health -= poisonDamage;
                enemyAnimation.blinkTimer = enemyAnimation.blinkDuration;
                AudioManager.instance.PlayShot("ZombieBaby", 0);
                UpdateHealthText();
                UpdateHealthBar();
                enemyAnimation.CreateVfx(index);

                if (health <= 0)
                {
                    enemyController.dead = true;
                    healthText.gameObject.SetActive(false);
                    enemyAnimation.ChangeStateAnimation("die", 0.25f, 0, 0);
                    GameManager.Instance.enemySpawned.Remove(gameObject);
                    DOVirtual.DelayedCall(2, () => { enemyController.SetDead(); });
                    yield break;
                }

                yield return new WaitForSeconds(0.7f);
            }
            else
            {
                yield break;
            }
        }
    }
}
