using System;
using Context;
using UnityEngine;

[Serializable]
public abstract class Node
{
    public enum ENodeState
    {
        Running,
        Failure,
        Success
    }

    [HideInInspector] public ENodeState state = ENodeState.Running;
    [HideInInspector] public bool started;
    [HideInInspector] public string guid = Guid.NewGuid().ToString();
    [HideInInspector] public Vector2 position;
    [HideInInspector] public Blackboard blackboard;
    [TextArea] public string description;

    [Tooltip("When enabled, the nodes OnDrawGizmos will be invoked")]
    public bool drawGizmos;

    [NonSerialized] public IContext context;

    public virtual void OnInitialize()
    {
        // Nothing to do here
    }

    public ENodeState Update()
    {
        if (!started)
        {
            OnStart();
            started = true;
        }

        state = OnUpdate();

        if (state != ENodeState.Running)
        {
            OnStop();
            started = false;
        }

        return state;
    }

    public void Abort()
    {
        BehaviourTree.Traverse(this, node =>
        {
            node.started = false;
            node.state = ENodeState.Running;
            node.OnStop();
        });
    }

    public virtual void OnDrawGizmos()
    {
    }

    protected abstract void OnStart();
    protected abstract void OnStop();
    protected abstract ENodeState OnUpdate();

    protected virtual void Log(string message)
    {
        Debug.Log($"[{GetType()}]{message}");
    }
}