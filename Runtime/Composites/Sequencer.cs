using System;

namespace TheKiwiCoder
{
    [Serializable]
    public class Sequencer : CompositeNode
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
                    case ENodeState.Failure:
                        return ENodeState.Failure;
                    case ENodeState.Success:
                        continue;
                }
            }

            return ENodeState.Success;
        }
    }
}