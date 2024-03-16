using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class SenseComponent : MonoBehaviour
{
    [SerializeField] private float forgetTime = 5f;
    static List<PerceptionStimuli> _registeredStimuli = new List<PerceptionStimuli>();
    List<PerceptionStimuli> _perceivableStimuli = new List<PerceptionStimuli>();
    Dictionary<PerceptionStimuli, Coroutine> _forgetStimuliCoroutines = new Dictionary<PerceptionStimuli, Coroutine>();
    
    public delegate void OnPerceptionUpdated(PerceptionStimuli stimuli, bool isSuccessfullySensed);
    public event OnPerceptionUpdated onPerceptionUpdated;
   public static void RegisterStimuli(PerceptionStimuli stimuli)
    {
       if( _registeredStimuli.Contains(stimuli)) return;
       
       _registeredStimuli.Add(stimuli);
    }
    public static void UnregisterStimuli(PerceptionStimuli stimuli)
    {
        if( !_registeredStimuli.Contains(stimuli)) return;
       
        _registeredStimuli.Remove(stimuli);
    }
    protected abstract bool IsStimuliSensable(PerceptionStimuli stimuli);

    public void Update()
    {
        foreach (var stimuli in _registeredStimuli)
        {
            if (IsStimuliSensable(stimuli))
            {
                if (!_perceivableStimuli.Contains(stimuli))
                {
                    _perceivableStimuli.Add(stimuli);
                    if (_forgetStimuliCoroutines.TryGetValue(stimuli, out Coroutine coroutine))
                    {
                        StopCoroutine(_forgetStimuliCoroutines[stimuli]);
                        _forgetStimuliCoroutines.Remove(stimuli);
                    }
                    else
                    {
                        onPerceptionUpdated?.Invoke(stimuli, true);
                    }
                }
            }
            else
            {
                if (_perceivableStimuli.Contains(stimuli))
                {
                    _perceivableStimuli.Remove(stimuli);
                    _forgetStimuliCoroutines.Add(stimuli,StartCoroutine(ForgetStimuli(stimuli)));
                }
                    
            }
        }
    }

    IEnumerator ForgetStimuli(PerceptionStimuli stimuli)
    {
        
        yield return new WaitForSeconds(forgetTime);
        _forgetStimuliCoroutines.Remove(stimuli);
        onPerceptionUpdated?.Invoke(stimuli, false);
        Debug.Log("Forget stimuli " + stimuli.name);
    }

    protected virtual void DrawDebug()
    {
    }
    private void OnDrawGizmos()
    {
        DrawDebug();
    }
}
