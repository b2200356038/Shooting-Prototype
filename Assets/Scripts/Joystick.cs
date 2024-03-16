using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public delegate void OnStickInputValueUpdated(Vector2 inputValue);
    public delegate void OnStickTaped();
    public event OnStickTaped OnStickTapedEvent;
    public event OnStickInputValueUpdated OnStickInputValueUpdatedEvent;
    [SerializeField] private RectTransform thumbStickTransform;
    [SerializeField] private RectTransform backgroundTransform;
    [SerializeField] private RectTransform joystickTransform;
    bool bWasDragged = false;
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 touchPos = eventData.position;
        Vector2 centerPos= backgroundTransform.position;
        Vector2 localOffset = Vector2.ClampMagnitude(touchPos - centerPos, backgroundTransform.sizeDelta.x/2);
        Vector2 inputValue = localOffset / (backgroundTransform.sizeDelta.x/2);
        thumbStickTransform.localPosition = localOffset;
        OnStickInputValueUpdatedEvent?.Invoke(inputValue);
        bWasDragged = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        joystickTransform.gameObject.SetActive(false);
        thumbStickTransform.localPosition = Vector2.zero;
        OnStickInputValueUpdatedEvent?.Invoke(Vector2.zero);
        if (bWasDragged)
        {
            OnStickTapedEvent?.Invoke();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        joystickTransform.gameObject.SetActive(true);
        joystickTransform.position = eventData.position;
    }
}
