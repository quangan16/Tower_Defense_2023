using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlant : MonoBehaviour
{
    public Animator animator;
    public string currentState;
    public ParticleSystem smokeBullet;

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState)
        {
            return;
        }
        animator.Play(newState);
        currentState = newState;
    }
    public void EmitParticle()
    {
        smokeBullet.Play();
    }
    public void ChangeAnimationState(string newState, float transition, int layer, float timeOffse)
    {
        if (currentState == newState)
        {
            return;
        }
        animator.CrossFade(newState, transition, layer, timeOffse);
        currentState = newState;
    }


}
