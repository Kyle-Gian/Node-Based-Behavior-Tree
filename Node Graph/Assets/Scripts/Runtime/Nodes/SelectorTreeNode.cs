//Author: Kyle Gian
//Date Created: 29/04/2021
//Last Modified: 29/04/2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorTreeNode : TreeNode
{

    public SelectorTreeNode(string a_guid, Vector2 a_position)
    {
        _nodeType = "selectornode";
        _GUID = a_guid;
        _position = a_position;
        _linksToChildren = new List<NodeEdge>();
        _function = new SelectorFuncionality();
    }

    public override void NodeFunction(GameObject AI)
    {
        this._currentStatus = TreeNode.Status.PROCESSING;
        this._function.RunFunction(this._linksToChildren, AI);
    }
}
