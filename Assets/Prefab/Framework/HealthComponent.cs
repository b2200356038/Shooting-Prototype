using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
   public delegate void OnHealthChanged(float health,float healthChange,float maxHealth);
   public delegate void OnTakeDamage(float health,float healthChange,float maxHealth, GameObject source);
   public delegate void OnDie();
   public event OnHealthChanged onHealthChanged;
   public event OnTakeDamage onTakeDamage;
   public event OnDie onDie;
   [SerializeField] float health=100;
   [SerializeField] float maxHealth=100;
   public void ChangeHealth(float amount, GameObject source)
   {
      if (amount==0 || health==0)
      {
         return;
      }
      float oldHealth=health;
      health+=amount;
      if (amount<0)
      {
         health=Mathf.Max(health,0);
         onTakeDamage?.Invoke(health,amount,maxHealth, source);
      }
      onHealthChanged?.Invoke(health,amount,maxHealth);
      if(health==0)
      {
         Debug.Log("Dead");
         onDie?.Invoke();
      }
   }
}
