using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum NodeResult
{
    Running,
    Success,
    InProgress
}
public abstract class BTNode
{
    public NodeResult UpdateNode()
    {
        if (!started)
        {
            started = true;
            NodeResult result = Execute();
            if(result!=NodeResult.InProgress)
            {
                EndNode();
                return result;
            }
        }
        NodeResult updateResult = Update();
        if(updateResult!=NodeResult.InProgress)
        {
            EndNode();
        }

        return updateResult;
    }
    protected virtual NodeResult Execute()
    {
        return NodeResult.Success;
    }
    protected virtual NodeResult Update()
    {
        return NodeResult.Success;
    }

    private void EndNode()
    {
        started = false;
        End();
    }
    protected virtual void End()
    {
        //clean up
    }
   
    bool started = false;
}
