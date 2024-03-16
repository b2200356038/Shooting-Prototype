using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysAwareSense : SenseComponent
{
    [SerializeField] private float awareDistance = 2;
    protected override bool IsStimuliSensable(PerceptionStimuli stimuli)
    {
        return Vector2 .Distance(transform.position, stimuli.transform.position) <= awareDistance;
    }
    protected override void DrawDebug()
    {
        Gizmos.DrawWireSphere(transform.position, awareDistance);
        base.DrawDebug();
    }
}
