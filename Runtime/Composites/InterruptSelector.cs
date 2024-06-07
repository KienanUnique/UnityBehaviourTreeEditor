using System;

namespace TheKiwiCoder
{
    [Serializable]
    public class InterruptSelector : Selector
    {
        protected override ENodeState OnUpdate()
        {
            var previous = current;
            base.OnStart();
            var status = base.OnUpdate();
            if (previous != current)
                if (children[previous].state == ENodeState.Running)
                    children[previous].Abort();

            return status;
        }
    }
}