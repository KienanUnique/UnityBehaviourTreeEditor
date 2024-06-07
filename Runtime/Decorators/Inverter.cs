using System;

namespace TheKiwiCoder
{
    [Serializable]
    public class Inverter : DecoratorNode
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

            switch (child.Update())
            {
                case ENodeState.Running:
                    return ENodeState.Running;
                case ENodeState.Failure:
                    return ENodeState.Success;
                case ENodeState.Success:
                    return ENodeState.Failure;
            }

            return ENodeState.Failure;
        }
    }
}