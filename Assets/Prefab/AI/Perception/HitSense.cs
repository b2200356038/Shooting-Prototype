using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HitSense : SenseComponent
{
    [SerializeField]
    HealthComponent healthComponent;
    [SerializeField]
    float hitMemory = 5;
    Dictionary <PerceptionStimuli, Coroutine> _forgetStimuliCoroutines = new Dictionary<PerceptionStimuli, Coroutine>();
    void Start()
    {
        healthComponent.onTakeDamage += OnTakeDamage;
    }

    private void OnTakeDamage(float health, float healthchange, float maxhealth, GameObject source)
    {
        PerceptionStimuli stimuli = source.GetComponent<PerceptionStimuli>();
        if (stimuli != null)
        {
            Coroutine newCoroutine = StartCoroutine(ForgetStimuli(stimuli));
            if (_forgetStimuliCoroutines.TryGetValue(stimuli, out Coroutine coroutine))
            {
                StopCoroutine(_forgetStimuliCoroutines[stimuli]);
                _forgetStimuliCoroutines[stimuli] = newCoroutine;
            }
            else
            {
                _forgetStimuliCoroutines.Add(stimuli, newCoroutine);
            }
        }
    }


    // Update is called once per frame
    protected override bool IsStimuliSensable(PerceptionStimuli stimuli)
    {
        return _forgetStimuliCoroutines.ContainsKey(stimuli);
    }
    
    IEnumerator ForgetStimuli(PerceptionStimuli stimuli)
    {
        yield return new WaitForSeconds(hitMemory);
        _forgetStimuliCoroutines.Remove(stimuli);
     
    }
}