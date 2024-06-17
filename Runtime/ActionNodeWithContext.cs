using System;
using Context;

[Serializable]
public abstract class ActionNodeWithContext<TContextType> : ActionNode where TContextType : class, IContext
{
    protected TContextType ConcreteContext { get; private set; }

    public override void OnInitialize()
    {
        if (context is TContextType concreteContext)
            ConcreteContext = concreteContext;
        else
            throw new InvalidCastException(
                $"Can't cast given context {context.GetType().Name} to need {typeof(TContextType)}");
    }
}