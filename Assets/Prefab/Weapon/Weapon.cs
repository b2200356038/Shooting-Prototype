using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private string AttachSlotTag;
    [SerializeField] private float AttackFireRateMultiplier;
    [SerializeField] AnimatorOverrideController animatorOverrideController;
    
    public abstract void Attack();
    public string GetAttachSlotTag()
    {
        return AttachSlotTag;
    }
    public GameObject Owner
    {
        get;
        private set;
    }
    public void Init(GameObject owner)
    {
        this.Owner = owner;
        Unequip();
    }
    public void Equip()
    {
        this.gameObject.SetActive(true);
        Owner.GetComponent<Animator>().runtimeAnimatorController = animatorOverrideController;
        Owner.GetComponent<Animator>().SetFloat("AttackSpeedMultiplier", AttackFireRateMultiplier);
    }
    public void Unequip()
    {
        this.gameObject.SetActive(false);
    }
    public void DamageTarget(GameObject target, float amount)
    {
        HealthComponent healthComponent = target.GetComponent<HealthComponent>();
        if (healthComponent != null)
        {
            healthComponent.ChangeHealth(-amount, Owner);
        }
    }
}
