using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] HealthComponent healthComponent;
    [SerializeField] Animator animator;
    [SerializeField] PerceptionComponent perceptionComponent;
    private GameObject target;
    void Start()
    {
        if (healthComponent!=null)
        {
            healthComponent.onDie+=StartDead;
            healthComponent.onTakeDamage+=OnTakeDamage;
        }
        perceptionComponent.onPerceptionTargetChanged+=OnPerceptionTargetChanged;
    }

    private void OnTakeDamage(float health, float healthchange, float maxhealth, GameObject source)
    {
        Debug.Log($"Enemy take damage {healthchange}");
    }

    private void OnPerceptionTargetChanged(GameObject target, bool sensed)
    {
        if (sensed)
        {
            this.target = target;
        }
        else
        {
            this.target = null;
        }
    }

    private void StartDead()
    {
        TriggerDeadAnimation();
    }
    
    private void TriggerDeadAnimation()
    {
        animator.SetTrigger("Dead");
    }
    private void OnDeadAnimationFinished()
    {
        Destroy(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        if(target!=null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(target.transform.position, 1f);
        }
    }
}
