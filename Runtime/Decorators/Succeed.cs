using System;

namespace TheKiwiCoder
{
    [Serializable]
    public class Succeed : DecoratorNode
    {
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override ENodeState OnUpdate()
        {
            if (child == null) return ENodeState.Failure;

            var state = child.Update();
            if (state == ENodeState.Failure) return ENodeState.Success;
            return state;
        }
    }
}