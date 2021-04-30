using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoratorTreeNode : TreeNode
{
    public DecoratorTreeNode(string a_guid, Vector2 a_position)
    {
        _nodeType = "decoratornode";
        _GUID = a_guid;
        _position = a_position;

    }
}
