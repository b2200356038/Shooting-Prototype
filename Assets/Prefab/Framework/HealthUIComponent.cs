using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUIComponent : MonoBehaviour
{
    [SerializeField] private Healthbar healthBarToSpawn;
    [SerializeField] Transform attachPoint;
    [SerializeField] HealthComponent healthComponent;
    private void Start()
    {
        InGameUI inGameUI = FindObjectOfType<InGameUI>();
        Healthbar healthBar = Instantiate(healthBarToSpawn, inGameUI.transform);
        healthBar.Init(attachPoint);
        healthComponent.onHealthChanged += healthBar.SetSliderValue;
        healthComponent.onDie += healthBar.OnOwnerDead;
    }
}
