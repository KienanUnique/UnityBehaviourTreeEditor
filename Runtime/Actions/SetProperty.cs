using System;

namespace TheKiwiCoder
{
    [Serializable]
    public class SetProperty : ActionNode
    {
        public BlackboardKeyValuePair pair;

        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override ENodeState OnUpdate()
        {
            pair.WriteValue();

            return ENodeState.Success;
        }
    }
}