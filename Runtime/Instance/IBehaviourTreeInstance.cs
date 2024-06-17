using Context;

namespace Instance
{
    public interface IBehaviourTreeInstance
    {
        void Initialize(IContext context);
        void Execute();
        void Reset();
    }
}