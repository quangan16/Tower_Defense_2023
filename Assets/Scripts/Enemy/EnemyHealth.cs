using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public TextMeshProUGUI healthText;
    public Slider healthSlider;
    public bool takeDamage = false;
    public EnemyMovement enemyMovement;
    public EnemyAnimation enemyAnimation;
    public EnemyState enemyState;

    public virtual void OnEnable()
    {
        SetValue();
    }
    
    public void SetValue()
    {
        healthText.gameObject.SetActive(true);
        int maxHealth1 = GameManager.Instance.waveCount * maxHealth;
        SetMaxHealth(maxHealth1);
    }

    public void SetMaxHealth(int value)
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

    public void UpdateHealthBar()
    {
        Debug.Log("cc");
        healthSlider.DOValue(health, 0.2f);
      
    }

    public void TakeDamage(int damage, int index)
    {
        if (!enemyMovement.dead)
        {
            health -= damage;
            enemyAnimation.blinkTimer = enemyAnimation.blinkDuration;
            AudioManager.instance.PlayShot("ZombieBaby", 0);
            UpdateHealthText();
            UpdateHealthBar();
            enemyAnimation.CreateVfx(index);
            if (health <= 0)
            {
                enemyMovement.dead = true;
                healthText.gameObject.SetActive(false);
                enemyAnimation.ChangeStateAnimation("die", 0.25f, 0, 0);
                GameManager.Instance.enemySpawned.Remove(gameObject);
                DOVirtual.DelayedCall(2, () => { enemyMovement.SetDead(); });
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

        enemyAnimation.OnNormal();
        enemyAnimation.OnPoisoned();
        enemyState = EnemyState.POISONED;

        // Start the new OnPoinsoned coroutine and store its reference
        poisonCoroutine = StartCoroutine(OnPoisoned(poisonDamage, index, duration));
        Debug.Log("occc");
        yield return new WaitForSeconds(3.0f);
        
        Debug.Log("obbb");
        enemyAnimation.OnNormal();
        enemyState = EnemyState.NORMAL;
    }

    public IEnumerator OnPoisoned(int poisonDamage, int index, float duration)
    {
        while (true)
        {
            if (enemyState == EnemyState.POISONED && !enemyMovement.dead)
            {


                health -= poisonDamage;
                enemyAnimation.blinkTimer = enemyAnimation.blinkDuration;
                AudioManager.instance.PlayShot("ZombieBaby", 0);
                UpdateHealthText();
                UpdateHealthBar();
                enemyAnimation.CreateVfx(index);

                if (health <= 0)
                {
                    enemyMovement.dead = true;
                    healthText.gameObject.SetActive(false);
                    enemyAnimation.ChangeStateAnimation("die", 0.25f, 0, 0);
                    GameManager.Instance.enemySpawned.Remove(gameObject);
                    DOVirtual.DelayedCall(2, () => { enemyMovement.SetDead(); });
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
