using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon
{
    [SerializeField] AimComponent aimComponent;
    [SerializeField] float damage = 5;
    public override void Attack()
    {
        GameObject target = aimComponent.GetAimTarget();
        if (target!=null)
        {
            Debug.Log($"Aim target is {target}");
            DamageTarget(target,damage);
        }
    }
}
