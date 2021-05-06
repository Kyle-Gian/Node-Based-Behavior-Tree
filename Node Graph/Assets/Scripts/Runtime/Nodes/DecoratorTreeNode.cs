//Author: Kyle Gian
//Date Created: 29/04/2021
//Last Modified: 29/04/2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoratorTreeNode : TreeNode
{
    ScriptContainer _scriptToBeChecked;

    public DecoratorTreeNode(string a_guid, Vector2 a_position, ScriptContainer script)
    {
        _nodeType = "decoratornode";
        _GUID = a_guid;
        _position = a_position;
        _function = new DecoratorFunctionality();
        _scriptToBeChecked = script;
    }

    public override void NodeFunction()
    {
        this._function.RunFunction(this._linksToChildren);
    }
}
