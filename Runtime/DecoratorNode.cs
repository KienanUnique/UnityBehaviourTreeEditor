using UnityEngine;

public abstract class DecoratorNode : Node
{
    [SerializeReference] [HideInInspector] public Node child;
}