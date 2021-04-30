﻿//Author: Kyle Gian
//Date Created: 29/04/2021
//Last Modified: 29/04/2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootTreeNode : TreeNode
{
    public RootTreeNode(string a_guid, Vector2 a_position)
    {
        _nodeType = "rootnode";
        _GUID = a_guid;
        _position = Vector2.zero;
        _entryPoint = true;
        _linksToChildren = new List<NodeEdge>();
        _function = new RootFunctionality();
    }

    public override void NodeFunction()
    {
        this._function.RunFunction();
    }
}
