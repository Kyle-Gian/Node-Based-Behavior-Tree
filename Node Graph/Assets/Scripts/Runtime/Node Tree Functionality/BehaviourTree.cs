using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTree : MonoBehaviour
{
    BuildBehaviorTree buildTree = new BuildBehaviorTree();
    RootTreeNode _rootTreeNode = new RootTreeNode();

    List<TreeNode> _treeNodes = new List<TreeNode>();
    List<NodeEdge> _nodeLinks = new List<NodeEdge>();

    [SerializeField]
    public GraphContainer _savedGraph;
    // Start is called before the first frame update
    void Start()
    {
        if (_savedGraph != null)
        {
            buildTree.LoadTree(_savedGraph, _rootTreeNode, _treeNodes, _nodeLinks);

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
            RunBehaviourTree();
        }

    }

    void RunBehaviourTree()
    {

        foreach (var link in _rootTreeNode._linksToChildren)
        {
            if (_rootTreeNode._linksToChildren != null)
            {
                //Get first node in list of root nodes outputs
                var nodeToCheck = ReturnChildNode(link.TargetNodeGUID);

                if (nodeToCheck._linksToChildren != null)
                {
                    //Get the firs node in list of roots child node
                    foreach (var child in nodeToCheck._linksToChildren)
                    {
                        if (nodeToCheck._currentStatus == TreeNode.Status.PROCESSING)
                        {
                            if (nodeToCheck._linksToChildren == null)
                            {
                                nodeToCheck.NodeFunction();
                            }

                        }

                    }
                }

            }

        }

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
