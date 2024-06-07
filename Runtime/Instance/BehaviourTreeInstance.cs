using System;
using System.Collections.Generic;
using TheKiwiCoder;
using TheKiwiCoder.Context;
using UnityEngine;

namespace TheKiwiCoder
{
    public class BehaviourTreeInstance : MonoBehaviour, IBehaviourTreeInstance
    {
        // The main behaviour tree asset
        [Tooltip("BehaviourTree asset to instantiate during Awake")]
        public BehaviourTree behaviourTree;

        [Tooltip("Run behaviour tree validation at startup (Can be disabled for release)")]
        public bool validate = true;

        // These values override the keys in the blackboard
        public List<BlackboardKeyValuePair> blackboardOverrides = new();
        
        private BehaviourTree _runtimeTree;

        public BehaviourTree RuntimeTree => _runtimeTree != null ? _runtimeTree : behaviourTree;

        public void Initialize(IContext context)
        {
            var isValid = ValidateTree();
            if (!isValid)
                _runtimeTree = null;
            else
            {
                _runtimeTree = behaviourTree.Clone();
                _runtimeTree.Bind(context);

                ApplyBlackboardOverrides();
            }
        }

        public void Execute()
        {
            if (_runtimeTree) 
                _runtimeTree.Update();
        }

        private void OnDrawGizmosSelected()
        {
            if (!Application.isPlaying) return;

            if (!_runtimeTree) return;

            BehaviourTree.Traverse(_runtimeTree.rootNode, n =>
            {
                if (n.drawGizmos) n.OnDrawGizmos();
            });
        }

        private void ApplyBlackboardOverrides()
        {
            foreach (var pair in blackboardOverrides)
            {
                // Find the key from the new behaviour tree instance
                var targetKey = _runtimeTree.blackboard.Find(pair.key.name);
                var sourceKey = pair.value;
                if (targetKey != null && sourceKey != null) targetKey.CopyFrom(sourceKey);
            }
        }

        private bool ValidateTree()
        {
            if (!behaviourTree)
            {
                Debug.LogWarning($"No BehaviourTree assigned to {name}, assign a behaviour tree in the inspector");
                return false;
            }

            var isValid = true;
            if (validate)
            {
                string cyclePath;
                isValid = !IsRecursive(behaviourTree, out cyclePath);

                if (!isValid) Debug.LogError($"Failed to create recursive behaviour tree. Found cycle at: {cyclePath}");
            }

            return isValid;
        }

        private bool IsRecursive(BehaviourTree tree, out string cycle)
        {
            // Check if any of the subtree nodes and their decendents form a circular reference, which will cause a stack overflow.
            var treeStack = new List<string>();
            var referencedTrees = new HashSet<BehaviourTree>();

            var cycleFound = false;
            var cyclePath = "";

            Action<Node> traverse = null;
            traverse = node =>
            {
                if (!cycleFound)
                    if (node is SubTree subtree && subtree.treeAsset != null)
                    {
                        treeStack.Add(subtree.treeAsset.name);
                        if (referencedTrees.Contains(subtree.treeAsset))
                        {
                            var index = 0;
                            foreach (var tree in treeStack)
                            {
                                index++;
                                if (index == treeStack.Count)
                                    cyclePath += $"{tree}";
                                else
                                    cyclePath += $"{tree} -> ";
                            }

                            cycleFound = true;
                        }
                        else
                        {
                            referencedTrees.Add(subtree.treeAsset);
                            BehaviourTree.Traverse(subtree.treeAsset.rootNode, traverse);
                            referencedTrees.Remove(subtree.treeAsset);
                        }

                        treeStack.RemoveAt(treeStack.Count - 1);
                    }
            };
            treeStack.Add(tree.name);

            referencedTrees.Add(tree);
            BehaviourTree.Traverse(tree.rootNode, traverse);
            referencedTrees.Remove(tree);

            treeStack.RemoveAt(treeStack.Count - 1);
            cycle = cyclePath;
            return cycleFound;
        }

        public BlackboardKey<T> FindBlackboardKey<T>(string keyName)
        {
            if (_runtimeTree) return _runtimeTree.blackboard.Find<T>(keyName);
            return null;
        }

        public void SetBlackboardValue<T>(string keyName, T value)
        {
            if (_runtimeTree) _runtimeTree.blackboard.SetValue(keyName, value);
        }

        public T GetBlackboardValue<T>(string keyName)
        {
            if (_runtimeTree) return _runtimeTree.blackboard.GetValue<T>(keyName);
            return default;
        }
    }
}