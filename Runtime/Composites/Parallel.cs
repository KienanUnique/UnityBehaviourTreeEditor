using System;
using System.Collections.Generic;
using System.Linq;

namespace TheKiwiCoder
{
    [Serializable]
    public class Parallel : CompositeNode
    {
        private List<ENodeState> childrenLeftToExecute = new();

        protected override void OnStart()
        {
            childrenLeftToExecute.Clear();
            children.ForEach(a => { childrenLeftToExecute.Add(ENodeState.Running); });
        }

        protected override void OnStop()
        {
        }

        protected override ENodeState OnUpdate()
        {
            var stillRunning = false;
            for (var i = 0; i < childrenLeftToExecute.Count(); ++i)
                if (childrenLeftToExecute[i] == ENodeState.Running)
                {
                    var status = children[i].Update();
                    if (status == ENodeState.Failure)
                    {
                        AbortRunningChildren();
                        return ENodeState.Failure;
                    }

                    if (status == ENodeState.Running) stillRunning = true;

                    childrenLeftToExecute[i] = status;
                }

            return stillRunning ? ENodeState.Running : ENodeState.Success;
        }

        private void AbortRunningChildren()
        {
            for (var i = 0; i < childrenLeftToExecute.Count(); ++i)
                if (childrenLeftToExecute[i] == ENodeState.Running)
                    children[i].Abort();
        }
    }
}