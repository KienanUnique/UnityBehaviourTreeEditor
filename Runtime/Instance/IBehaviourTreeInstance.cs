using TheKiwiCoder.Context;

namespace TheKiwiCoder
{
    public interface IBehaviourTreeInstance
    {
        void Initialize(IContext context);
        void Execute();
    }
}