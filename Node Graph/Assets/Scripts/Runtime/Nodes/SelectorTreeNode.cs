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
    }
}
