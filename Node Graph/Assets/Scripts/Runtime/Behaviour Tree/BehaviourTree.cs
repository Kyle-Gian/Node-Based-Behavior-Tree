using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTree : MonoBehaviour
{
    BuildBehaviorTree buildTree = new BuildBehaviorTree();
    RootTreeNode _rootTreeNode = new RootTreeNode();

    List<TreeNode> _treeNodes = new List<TreeNode>();
    List<NodeEdge> _nodeLinks = new List<NodeEdge>();
    private static MonoBehaviour[] _scripts;

    [SerializeField]
    public GraphContainer _savedGraph;
    [SerializeField]
    GameObject _enemyTeam;

    List<Transform> _enemyList = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        if (_savedGraph != null)
        {
            _scripts = this.GetComponents<MonoBehaviour>();

            buildTree.LoadTree(_savedGraph, _rootTreeNode, _treeNodes, _nodeLinks);

        }
        else
        {
            Debug.LogError("No Graph Loaded");
        }

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
                        if (nodeToCheck._currentStatus == TreeNode.Status.PROCESSING)
                        {
                            if (nodeToCheck._linksToChildren[j].Child != null)
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

    public static void RunMethodFromScriptOnObject(string className, string methodName)
    {
        foreach (var script in _scripts)
        {
            if (script.ToString().Contains(className))
            {
                script.Invoke(methodName, 0);
            }
        }
    }
}
