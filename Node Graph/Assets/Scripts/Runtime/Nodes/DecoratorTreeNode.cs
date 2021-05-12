//Author: Kyle Gian
//Date Created: 29/04/2021
//Last Modified: 29/04/2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoratorTreeNode : TreeNode
{
    string _nameOfAttachedFunction;

    public DecoratorTreeNode(string a_guid, Vector2 a_position, string script)
    {
        _nodeType = "decoratornode";
        _GUID = a_guid;
        _position = a_position;
        _function = new DecoratorFunctionality();
        _nameOfAttachedFunction = script;
    }

    public override void NodeFunction(Transform AI)
    {
        this._currentStatus = TreeNode.Status.PROCESSING;
        this._function.RunFunction(this._linksToChildren, AI);
    }
}
