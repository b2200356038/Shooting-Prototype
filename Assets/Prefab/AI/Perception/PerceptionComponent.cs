using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class PerceptionComponent : MonoBehaviour
{
    [SerializeField] private SenseComponent[] senses;
    PerceptionStimuli _targetStimuli;
    LinkedList<PerceptionStimuli> _currentlyPerceivedStimuli = new LinkedList<PerceptionStimuli>();
    
    public delegate void OnPerceptionTargetChanged(GameObject target, bool sensed);
    public event OnPerceptionTargetChanged onPerceptionTargetChanged;
    
    // Start is called before the first frame update    
    void Start()
    {
        foreach(SenseComponent sense in senses)
        {
            sense.onPerceptionUpdated += SenseUpdated;
        }
    }

    private void SenseUpdated(PerceptionStimuli stimuli, bool isSuccessfullySensed)
    {
        var node = _currentlyPerceivedStimuli.Find(stimuli);
        Debug.Log($"SenseUpdated {stimuli.name} {isSuccessfullySensed}");
        if (isSuccessfullySensed)
        {
            if (node != null)
            {
                _currentlyPerceivedStimuli.AddAfter(node, stimuli);
            }
            else
            {
                _currentlyPerceivedStimuli.AddLast(stimuli);
            }
        }
        else
        {
            _currentlyPerceivedStimuli.Remove(node);
        }
        if(_currentlyPerceivedStimuli.Count>0)
        {
            PerceptionStimuli highestStimuli = _currentlyPerceivedStimuli.First.Value;
            if(_targetStimuli==null|| _targetStimuli!=highestStimuli)
            {
                _targetStimuli = highestStimuli;
                Debug.Log("Target is " + _targetStimuli.name);
                onPerceptionTargetChanged?.Invoke(_targetStimuli.gameObject, true);
            }
        }
        else
        {
            onPerceptionTargetChanged?.Invoke(_targetStimuli.gameObject, false);
            _targetStimuli = null;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
