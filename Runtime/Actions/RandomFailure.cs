using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Actions
{
    [Serializable]
    public class RandomFailure : ActionNode
    {
        [Range(0, 1)] [Tooltip("Percentage chance of failure")]
        public float chanceOfFailure = 0.5f;

        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override ENodeState OnUpdate()
        {
            var value = Random.value;
            if (value > chanceOfFailure) return ENodeState.Failure;
            return ENodeState.Success;
        }
    }
}