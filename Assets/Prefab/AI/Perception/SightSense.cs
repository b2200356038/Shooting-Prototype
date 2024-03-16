using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SightSense : SenseComponent
{
    [SerializeField] float sightRange = 10;
    [SerializeField] float sightAngle = 45;
    [SerializeField] float eyeHeight = 1.5f;
    protected override bool IsStimuliSensable(PerceptionStimuli stimuli)
    {
        Vector3 toStimuli = stimuli.transform.position - transform.position;
        if (toStimuli.magnitude > sightRange) return false;
        if (Vector3.Angle(transform.forward, toStimuli) > sightAngle) return false;
        if (Physics.Raycast(transform.position+Vector3.up*eyeHeight, toStimuli, out RaycastHit hit, sightRange))
        {
            if (hit.collider.gameObject == stimuli.gameObject)
            {
                return true;
            }
        }
        return false;
    }
    protected override void DrawDebug()
    {
         base.DrawDebug();
         Vector3 drawCenter = transform.position + Vector3.up * eyeHeight;
         Gizmos.DrawWireSphere(drawCenter, sightRange);
         Vector3 leftLimit = Quaternion.AngleAxis(-sightAngle, Vector3.up) * transform.forward;
         Vector3 rightLimit = Quaternion.AngleAxis(sightAngle, Vector3.up) * transform.forward;
         Gizmos.DrawLine(drawCenter, drawCenter + leftLimit * sightRange);
         Gizmos.DrawLine(drawCenter, drawCenter + rightLimit * sightRange);
         
    }
}
