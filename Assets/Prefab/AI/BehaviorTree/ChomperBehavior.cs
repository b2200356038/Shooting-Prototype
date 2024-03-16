using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperBehavior : BehaviorTree
{
    protected override void ConstructBehaviorTree(out BTNode root)
    {
        root = new BTTaskWait(2f);
    }

}
