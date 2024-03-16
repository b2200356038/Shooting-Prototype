using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerceptionStimuli : MonoBehaviour
{
    public void Start()
    {
        SenseComponent.RegisterStimuli(this);
    }
    private void OnDestroy()
    {
        SenseComponent.UnregisterStimuli(this);
    }
}
