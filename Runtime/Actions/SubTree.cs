using System;
using UnityEngine;

namespace TheKiwiCoder
{
    [Serializable]
    public class SubTree : ActionNode
    {
        [Tooltip("Behaviour tree asset to run as a subtree")]
        public BehaviourTree treeAsset;

        [HideInInspector] public BehaviourTree treeInstance;

        public override void OnInitialize()
        {
            if (treeAsset)
            {
                treeInstance = treeAsset.Clone();
                treeInstance.Bind(context);
            }
        }

        protected override void OnStart()
        {
            if (treeInstance) treeInstance.treeState = ENodeState.Running;
        }

        protected override void OnStop()
        {
        }

        protected override ENodeState OnUpdate()
        {
            if (treeInstance) return treeInstance.Update();
            return ENodeState.Failure;
        }
    }
}