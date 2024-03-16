using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BTTaskWait : BTNode
{
  [SerializeField]
  float waitTime=2f;
  float timeElapsed=0f;
  public BTTaskWait(float waitTime)
  {
    this.waitTime = waitTime;
  }
  protected override NodeResult Execute()
  {
    if(waitTime<=0)
    {
      return NodeResult.Success;
    }
    Debug.Log($"Waiting Started with time {waitTime} seconds");
    timeElapsed = 0f;
    return NodeResult.InProgress;
  }

  protected override NodeResult Update()
  {
    timeElapsed += Time.deltaTime;
    if (timeElapsed >= waitTime)
    {
      Debug.Log("Waiting Finished ");
      return NodeResult.Success;
    }
    return NodeResult.InProgress; 
  }
}
