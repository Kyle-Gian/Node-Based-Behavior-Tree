//Author: Kyle Gian
//Date Created: 30/04/2021
//Last Modified: 30/04/2021
namespace NodeBasedBehaviourTree
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

#if UNITY_EDITOR
    using UnityEditor;
    using UnityEditor.Experimental.GraphView;
    using UnityEditor.UIElements;
#endif
    public class NodeFunctionality
    {
        public TreeNode.Status ReturnStatus()
        {
            return TreeNode.Status.PROCESSING;
        }

        public virtual void RunFunction(List<NodeEdge> nodeEdges, GameObject AI)
        {

        }
        public virtual void RunFunction(LeafTreeNode node, GameObject AI)
        {

        }


        public void ResetNodeStatus(List<NodeEdge> nodeEdges)
        {
            foreach (var edge in nodeEdges)
            {
                edge.Child._currentStatus = TreeNode.Status.PROCESSING;
            }
        }
    }
}