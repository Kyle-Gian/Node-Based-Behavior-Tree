//Author: Kyle Gian
//Date Created: 29/04/2021
//Last Modified: 29/04/2021
namespace NodeBasedBehaviourTree
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class SequenceTreeNode : TreeNode
    {
        public SequenceTreeNode(string a_guid, Vector2 a_position)
        {
            _nodeType = "sequencenode";
            _GUID = a_guid;
            _position = a_position;
            _linksToChildren = new List<NodeEdge>();
            _function = new SequenceFunctionality();
            _childrenHaveBeenSorted = false;
        }

        public override void NodeFunction(GameObject AI)
        {
            if (!_childrenHaveBeenSorted)
            {
                RearrangeChildren();
                _childrenHaveBeenSorted = true;
            }
            this._function.RunFunction(this._linksToChildren, AI);
        }
    }
}