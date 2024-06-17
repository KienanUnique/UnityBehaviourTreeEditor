using System;
using Random = UnityEngine.Random;

namespace Composites
{
    [Serializable]
    public class RandomSelector : CompositeNode
    {
        protected int current;

        protected override void OnStart()
        {
            current = Random.Range(0, children.Count);
        }

        protected override void OnStop()
        {
        }

        protected override ENodeState OnUpdate()
        {
            var child = children[current];
            return child.Update();
        }
    }
}