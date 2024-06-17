using System;
using System.Collections.Generic;
using Context;
using UnityEngine;

[CreateAssetMenu]
public class BehaviourTree : ScriptableObject
{
    [SerializeReference] public RootNode rootNode;

    [SerializeReference] public List<Node> nodes = new();

    public Node.ENodeState treeState = Node.ENodeState.Running;

    public Blackboard blackboard = new();

    public BehaviourTree()
    {
        rootNode = new RootNode();
        nodes.Add(rootNode);
    }

    private void OnEnable()
    {
        // Validate the behaviour tree on load, removing all null children
        nodes.RemoveAll(node => node == null);
        Traverse(rootNode, node =>
        {
            if (node is CompositeNode composite) composite.children.RemoveAll(child => child == null);
        });
    }

    public Node.ENodeState Update()
    {
        if (treeState == Node.ENodeState.Running) treeState = rootNode.Update();
        return treeState;
    }

    public static List<Node> GetChildren(Node parent)
    {
        var children = new List<Node>();

        if (parent is DecoratorNode decorator && decorator.child != null) children.Add(decorator.child);

        if (parent is RootNode rootNode && rootNode.child != null) children.Add(rootNode.child);

        if (parent is CompositeNode composite) return composite.children;

        return children;
    }

    public static void Traverse(Node node, Action<Node> visiter)
    {
        if (node != null)
        {
            visiter.Invoke(node);
            var children = GetChildren(node);
            children.ForEach(n => Traverse(n, visiter));
        }
    }

    public BehaviourTree Clone()
    {
        var tree = Instantiate(this);
        return tree;
    }

    public void Bind(IContext context)
    {
        Traverse(rootNode, node =>
        {
            node.context = context;
            node.blackboard = blackboard;
            node.OnInitialize();
        });
    }

    #region EditorProperties

    public Vector3 viewPosition = new(600, 300);
    public Vector3 viewScale = Vector3.one;

    #endregion
}