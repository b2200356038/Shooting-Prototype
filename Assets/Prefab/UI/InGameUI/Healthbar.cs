using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
  [SerializeField] Slider slider;
  private Transform _attachPoint;
  public void Init(Transform attachPoint)
  {
    _attachPoint = attachPoint;
  }
  
  public void SetSliderValue(float health,float healthChange,float maxHealth)
  {
    slider.value = health/maxHealth;
  }
  public void OnOwnerDead()
  {
    Destroy(gameObject);
  }
  private void Update()
  {
    var attachScreenPoint = Camera.main.WorldToScreenPoint(_attachPoint.position);
    transform.position = attachScreenPoint;
  }
}
