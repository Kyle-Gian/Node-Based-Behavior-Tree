//Author: Kyle Gian
//Date Created: 30/04/2021
//Last Modified: 30/04/2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System;

public class BuildBehaviorTree
{
    

    public void LoadTree(GraphContainer _graph, RootTreeNode _rootTreeNode, List<TreeNode> _treeNodes, List<NodeEdge> _nodeLinks)
    {

        _rootTreeNode._GUID =  Guid.NewGuid().ToString();

        if (_graph == null)
        {
            Debug.LogError("File not found!");
            return;
        }

        foreach (var node in _graph.NodeData)
        {
            var newNode = CreateNodeType(node);
            
            _treeNodes.Add(newNode);
        }

        for (int i = 0; i < _graph.NodeLink.Count; i++)
        {
            _nodeLinks.Add(_graph.NodeLink[i]);
        }

        foreach (var node in _treeNodes)
        {

            foreach (var edge in _nodeLinks)
            {
                if (edge.PortName == "Next")
                {
                    edge.BaseNodeGUID = _rootTreeNode._GUID;
                    _rootTreeNode._linksToChildren.Add(edge);
                }
                if (edge.BaseNodeGUID == node._GUID)
                {
                    AddOutputEdgeToNode(node, edge);

                }
                if (edge.TargetNodeGUID == node._GUID)
                {
                    AddInputEdgeToNode(node, edge);
                }
            }

        }

    }
    public TreeNode CreateNodeType(NodeData a_nodeType)
    {
        switch (a_nodeType.NodeType.ToLower())
        {
            case "leafnode":
                LeafTreeNode leafNode = new LeafTreeNode(a_nodeType.NodeGUID, a_nodeType.Position, a_nodeType.NodeFunction);
                return leafNode;
            case "sequencenode":
                SequenceTreeNode sequenceNode = new SequenceTreeNode(a_nodeType.NodeGUID, a_nodeType.Position);
                return sequenceNode;
            case "selectornode":
                SelectorTreeNode selectorNode = new SelectorTreeNode(a_nodeType.NodeGUID, a_nodeType.Position);
                return selectorNode;
            case "decoratornode":
                DecoratorTreeNode decoratorNode = new DecoratorTreeNode(a_nodeType.NodeGUID, a_nodeType.Position, a_nodeType.NodeFunction);
                return decoratorNode;
            case "rootnode":
                RootTreeNode rootNode = new RootTreeNode(a_nodeType.NodeGUID, a_nodeType.Position);
                return rootNode;

            default:
                break;
        }
        return null;
    }

    public void AddOutputEdgeToNode<T>(T node,NodeEdge link) where T:TreeNode
    {
        node._linksToChildren.Add(link);
        link.Parent = node;

    }

    private void AddInputEdgeToNode<T>(T node, NodeEdge edge) where T : TreeNode
    {
        node._ParentEdge = edge;
        edge.Child = node;

    }
}
