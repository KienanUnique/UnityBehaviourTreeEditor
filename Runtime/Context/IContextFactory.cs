using UnityEngine;

namespace TheKiwiCoder.Context
{
    public interface IContextFactory
    {
        IContext Create(GameObject gameObject);
    }
}