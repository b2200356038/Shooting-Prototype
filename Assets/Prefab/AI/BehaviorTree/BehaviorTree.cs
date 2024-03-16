using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviorTree : MonoBehaviour
{
    BTNode root;
    void Start()
    {
        ConstructBehaviorTree(out root);
    }

    protected abstract void ConstructBehaviorTree(out BTNode root);
    void Update()
    {
        root.UpdateNode();
    }
}
