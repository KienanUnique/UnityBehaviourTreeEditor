using System;

namespace Decorators
{
    [Serializable]
    public class Failure : DecoratorNode
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
            if (state == ENodeState.Success) return ENodeState.Failure;
            return state;
        }
    }
}