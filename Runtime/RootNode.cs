using System;
using UnityEngine;

[Serializable]
public class RootNode : Node
{
    [SerializeReference] [HideInInspector] public Node child;

    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override ENodeState OnUpdate()
    {
        if (child != null)
            return child.Update();
        return ENodeState.Failure;
    }
}