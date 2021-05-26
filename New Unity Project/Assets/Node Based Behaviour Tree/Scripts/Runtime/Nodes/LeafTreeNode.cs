//Author: Kyle Gian
//Date Created: 29/04/2021
//Last Modified: 29/04/2021
namespace NodeBasedBehaviourTree
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class LeafTreeNode : TreeNode
    {
        public string _nameOfAttachedFunction;
        public MonoBehaviour _script;
        public LeafTreeNode(string a_guid, Vector2 a_position, string a_attachedFunction)
        {
            _nodeType = "leafnode";
            _GUID = a_guid;
            _position = a_position;
            _linksToChildren = new List<NodeEdge>();
            _function = new LeafFunctionality();
            _nameOfAttachedFunction = a_attachedFunction;
            _script = BehaviourTree.GetScriptOnObject(a_attachedFunction);

        }
        public override void NodeFunction(GameObject AI)
        {
            _function.RunFunction(this._linksToChildren, AI);
        }
    }
}