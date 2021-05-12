//Author: Kyle Gian
//Date Created: 29/04/2021
//Last Modified: 29/04/2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeNode
{
    public string _GUID;
    public string _nodeType;
    public Vector2 _position;
    public bool _entryPoint = false;
    public List<NodeEdge> _linksToChildren;
    public NodeEdge _ParentEdge;
    public NodeFunctionality _function;
    public Status _currentStatus = Status.PROCESSING;

    public virtual void NodeFunction(Transform AI)
    {

    }

    public enum Status
    {
        SUCCESS,
        FAIL,
        PROCESSING

    }

}
