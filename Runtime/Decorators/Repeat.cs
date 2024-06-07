using System;
using UnityEngine;

namespace TheKiwiCoder
{
    [Serializable]
    public class Repeat : DecoratorNode
    {
        [Tooltip("Restarts the subtree on success")]
        public bool restartOnSuccess = true;

        [Tooltip("Restarts the subtree on failure")]
        public bool restartOnFailure;

        [Tooltip("Maximum number of times the subtree will be repeated. Set to 0 to loop forever")]
        public int maxRepeats;

        private int iterationCount;

        protected override void OnStart()
        {
            iterationCount = 0;
        }

        protected override void OnStop()
        {
        }

        protected override ENodeState OnUpdate()
        {
            if (child == null) return ENodeState.Failure;

            switch (child.Update())
            {
                case ENodeState.Running:
                    break;
                case ENodeState.Failure:
                    if (restartOnFailure)
                    {
                        iterationCount++;
                        if (iterationCount >= maxRepeats && maxRepeats > 0)
                            return ENodeState.Failure;
                        return ENodeState.Running;
                    }
                    else
                    {
                        return ENodeState.Failure;
                    }
                case ENodeState.Success:
                    if (restartOnSuccess)
                    {
                        iterationCount++;
                        if (iterationCount >= maxRepeats && maxRepeats > 0)
                            return ENodeState.Success;
                        return ENodeState.Running;
                    }
                    else
                    {
                        return ENodeState.Success;
                    }
            }

            return ENodeState.Running;
        }
    }
}