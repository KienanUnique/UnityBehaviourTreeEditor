using System;
using UnityEngine;

namespace Actions
{
    [Serializable]
    public class Breakpoint : ActionNode
    {
        protected override void OnStart()
        {
            Debug.Log("Trigging Breakpoint");
            Debug.Break();
        }

        protected override void OnStop()
        {
        }

        protected override ENodeState OnUpdate()
        {
            return ENodeState.Success;
        }
    }
}