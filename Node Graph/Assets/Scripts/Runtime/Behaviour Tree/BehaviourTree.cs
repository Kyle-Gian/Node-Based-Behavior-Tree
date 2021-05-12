//Author: Kyle Gian
//Date Created: 02/05/2021
//Last Modified: 11/05/2021

//This is used to run the tree from the root node checking waiting for the behaviour to pass through to the AI


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTree : MonoBehaviour
{
    BuildBehaviorTree buildTree = new BuildBehaviorTree();
    RootTreeNode _rootTreeNode = new RootTreeNode();

    List<TreeNode> _treeNodes = new List<TreeNode>();
    List<NodeEdge> _nodeLinks = new List<NodeEdge>();
    public static MonoBehaviour[] _scripts;

    [SerializeField]
    public GraphContainer _savedGraph;
    [SerializeField]
    GameObject _enemyTeam = null;
    static GameObject _brain;

    List<Transform> _enemyList = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        _brain = this.gameObject;
        if (_savedGraph != null)
        {
            //Get the scripts attached to this object
            _scripts = this.GetComponents<MonoBehaviour>();

            //Make the tree
            buildTree.LoadTree(_savedGraph, _rootTreeNode, _treeNodes, _nodeLinks);

        }
        else
        {
            Debug.LogError("No Graph Loaded");
        }

        //Create the list of AI
        if (_enemyTeam != null)
        {
            for (int i = 0; i < _enemyTeam.transform.childCount; i++)
            {
                _enemyList.Add(_enemyTeam.transform.GetChild(i));
            }
        }
        else
        {
            Debug.LogError("No AI have been added to the list");
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
        //Run each individual AI through the behaviour tree to get their action 
        foreach (var AI in _enemyList)
        {

            for (int i = 0; i < _rootTreeNode._linksToChildren.Count; i++)
            {
                if (_rootTreeNode._linksToChildren != null)
                {
                    //Get first node in list of root nodes outputs
                    var nodeToCheck = ReturnChildNode(_rootTreeNode._linksToChildren[i].TargetNodeGUID);

                    if (nodeToCheck._linksToChildren != null)
                    {
                        //Get the first node in list of roots child node
                        for (int j = 0; j < nodeToCheck._linksToChildren.Count; j++)
                        {
                            nodeToCheck._currentStatus = TreeNode.Status.PROCESSING;

                            if (nodeToCheck._currentStatus == TreeNode.Status.PROCESSING)
                            {
                                if (nodeToCheck._linksToChildren[j].Child != null)
                                {
                                    nodeToCheck.NodeFunction(AI);
                                }

                            }
                            else
                            {
                                break;
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
    public static MonoBehaviour GetScriptOnObject(string className)
    {
        foreach (var script in _scripts)
        {
            if (script.ToString().Contains(className))
            {
                var getScript = _brain.GetComponent<NodeCheck>();
                return script;
            }
        }
        return null;
    }
}
