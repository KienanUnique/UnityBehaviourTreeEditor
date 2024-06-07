using System;

namespace TheKiwiCoder
{
    [Serializable]
    public class Selector : CompositeNode
    {
        protected int current;

        protected override void OnStart()
        {
            current = 0;
        }

        protected override void OnStop()
        {
        }

        protected override ENodeState OnUpdate()
        {
            for (var i = current; i < children.Count; ++i)
            {
                current = i;
                var child = children[current];

                switch (child.Update())
                {
                    case ENodeState.Running:
                        return ENodeState.Running;
                    case ENodeState.Success:
                        return ENodeState.Success;
                    case ENodeState.Failure:
                        continue;
                }
            }

            return ENodeState.Failure;
        }
    }
}