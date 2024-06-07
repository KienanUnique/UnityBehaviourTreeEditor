using System;
using UnityEngine;

namespace TheKiwiCoder
{
    [Serializable]
    public class Wait : ActionNode
    {
        [Tooltip("Amount of time to wait before returning success")]
        public float duration = 1;

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
            var timeRemaining = Time.time - startTime;
            if (timeRemaining > duration) return ENodeState.Success;
            return ENodeState.Running;
        }
    }
}