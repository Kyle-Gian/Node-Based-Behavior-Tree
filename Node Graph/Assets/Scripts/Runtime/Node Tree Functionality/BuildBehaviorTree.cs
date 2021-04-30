//Author: Kyle Gian
//Date Created: 30/04/2021
//Last Modified: 30/04/2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System;

public class BuildBehaviorTree : MonoBehaviour
{
    [SerializeField]
    GraphContainer _savedGraph;

    RootTreeNode _rootTreeNode; 

    List<TreeNode> _treeNodes = new List<TreeNode>();
    List<NodeEdge> _nodeLinks = new List<NodeEdge>();
    

    // Start is called before the first frame update
    void Start()
    {
        if (_savedGraph != null)
        {
            LoadTree();

        }
        else
        {
            Debug.LogError("No Graph Loaded");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_savedGraph != null)
        {
            foreach (var link in _rootTreeNode._linksToChildren)
            {
                if (_rootTreeNode._linksToChildren != null)
                {
                    var nodeToCheck = ReturnChildNode(link.TargetNodeGUID);

                    if (nodeToCheck._currentStatus == TreeNode.Status.PROCESSING)
                    {
                        if (nodeToCheck._linksToChildren == null)
                        {
                            nodeToCheck.NodeFunction();
                        }
                        else
                        {
                            foreach (var node in nodeToCheck._linksToChildren)
                            {
                                
                            }

                        }
   

                    }

                }
                
            }
        }

    }

    public void LoadTree()
    {
        _rootTreeNode = new RootTreeNode(Guid.NewGuid().ToString(), Vector2.zero);
        if (_savedGraph == null)
        {
            Debug.LogError("File not found!");
            return;
        }

        foreach (var node in _savedGraph.NodeData)
        {
            CreateNodes(node);

        }

        for (int i = 0; i < _savedGraph.NodeLink.Count; i++)
        {
            _nodeLinks.Add(_savedGraph.NodeLink[i]);
        }

        foreach (var node in _treeNodes)
        {

            foreach (var edge in _nodeLinks)
            {
                if (edge.PortName == "Next")
                {
                    edge.BaseNodeGUID = _rootTreeNode._GUID;
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



    public void CreateNodes(NodeData nodeType)
    {
        var newNode = CreateNodeType(nodeType);

        _treeNodes.Add(newNode);

    }

    public TreeNode CreateNodeType(NodeData a_nodeType)
    {
        switch (a_nodeType.NodeType.ToLower())
        {
            case "leafnode":
                LeafTreeNode leafNode = new LeafTreeNode(a_nodeType.NodeGUID, a_nodeType.Position);
                return leafNode;
            case "sequencenode":
                SequenceTreeNode sequenceNode = new SequenceTreeNode(a_nodeType.NodeGUID, a_nodeType.Position);
                return sequenceNode;
            case "selectornode":
                SelectorTreeNode selectorNode = new SelectorTreeNode(a_nodeType.NodeGUID, a_nodeType.Position);
                return selectorNode;
            case "decoratornode":
                DecoratorTreeNode decoratorNode = new DecoratorTreeNode(a_nodeType.NodeGUID, a_nodeType.Position);
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

    }

    private void AddInputEdgeToNode<T>(T node, NodeEdge edge) where T : TreeNode
    {
        node._ParentEdge = edge;

    }

    public TreeNode ReturnChildNode(string a_nodeGuid)
    {
        foreach (var node in _treeNodes)
        {
            if (a_nodeGuid == node._GUID)
            {
                return node;
            }
        }

        return null;
    }
}
