using System;

namespace Composites
{
    [Serializable]
    public class Switch : CompositeNode
    {
        public NodeProperty<int> index;
        public bool interruptable = true;
        private int currentIndex;

        protected override void OnStart()
        {
            currentIndex = index.Value;
        }

        protected override void OnStop()
        {
        }

        protected override ENodeState OnUpdate()
        {
            if (interruptable)
            {
                var nextIndex = index.Value;
                if (nextIndex != currentIndex) children[currentIndex].Abort();
                currentIndex = nextIndex;
            }

            if (currentIndex < children.Count) return children[currentIndex].Update();
            return ENodeState.Failure;
        }
    }
}