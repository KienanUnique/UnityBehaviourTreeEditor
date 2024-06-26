using System;
using UnityEngine;

namespace Decorators
{
    [Serializable]
    public class Timeout : DecoratorNode
    {
        [Tooltip("Returns failure after this amount of time if the subtree is still running.")]
        public float duration = 1.0f;

        private float startTime;

        protected override void OnStart()
        {
            startTime = Time.time;
        }

        protected override void OnStop()
        {
        }

        protected override ENodeState OnUpdate()
        {
            if (child == null) return ENodeState.Failure;

            if (Time.time - startTime > duration) return ENodeState.Failure;

            return child.Update();
        }
    }
}