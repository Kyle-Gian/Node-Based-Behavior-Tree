//Author: Kyle Gian
//Date Created: 29/04/2021
//Last Modified: 29/04/2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafTreeNode : TreeNode
{
    public LeafTreeNode(string a_guid, Vector2 a_position)
    {
        _nodeType = "leafnode";
        _GUID = a_guid;
        _position = a_position;
        _linksToChildren = new List<NodeEdge>();
        _function = new LeafFunctionality();
    }
    public override void NodeFunction()
    {
        this._function.RunFunction();
    }
}
