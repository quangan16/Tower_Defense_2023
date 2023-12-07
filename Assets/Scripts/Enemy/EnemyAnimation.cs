using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer1;
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer2;
    public float blinkIntensity;
    public float blinkDuration;
    public float blinkTimer;
    public Transform posExplore;

    public void Update()
    {
        blinkTimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(blinkTimer / blinkDuration);
        float intensity = (lerp * blinkIntensity) + 1;
        skinnedMeshRenderer1.material.color = Color.white * intensity;
        skinnedMeshRenderer2.material.color = Color.white * intensity;
    }

    public void ChangeStateAnimation(string name, float duration, int layer, float offset)
    {
        animator.CrossFade(name, duration, layer, offset);
    }

    public void CreateVfx(int index)
    {
        ObjectPool objectPool = ObjectPool.instance;
        GameObject newParticle = objectPool.Get(objectPool.particleEnemies[index]);
        newParticle.transform.position = posExplore.position;
        newParticle.GetComponent<ParticleSystem>().Play();
        DOVirtual.DelayedCall(1, () => { objectPool.Return(newParticle); });
    }

    public void OnPoisoned()
    {
        skinnedMeshRenderer1.material.color = Color.magenta * 2;
    }

    public void OnNormal()
    {
        skinnedMeshRenderer1.material.color = Color.white * 0;
    }

}
