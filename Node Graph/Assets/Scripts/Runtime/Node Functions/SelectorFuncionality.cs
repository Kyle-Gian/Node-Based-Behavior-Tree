//Author: Kyle Gian
//Date Created: 30/04/2021
//Last Modified: 30/04/2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;


public class SelectorFuncionality : NodeFunctionality
{
    public override void RunFunction(List<NodeEdge> nodeEdges)
    {
        for (int i = 0; i < nodeEdges.Count; i++)
        {
            //if the child node is processing then run the function and get a reult of return of fail or success

            if (nodeEdges[i].Child._linksToChildren.Count == 0 & nodeEdges[i].Child._nodeType == "leafnode")
            {
                nodeEdges[i].Child._function.RunFunction((LeafTreeNode)nodeEdges[i].Child);

            }
            else
            {
                nodeEdges[i].Child._function.RunFunction(nodeEdges[i].Child._linksToChildren);

            }

            //After the function is run, check if the status is a fail. If so the Parent node returns fail
            if (nodeEdges[i].Child._currentStatus == TreeNode.Status.SUCCESS)
            {
                nodeEdges[i].Parent._currentStatus = TreeNode.Status.SUCCESS;
                break;
            }
            //If all children nodes return FAIL, Parent returns FAIL
            if (i == nodeEdges.Count)
            {
                nodeEdges[i].Parent._currentStatus = TreeNode.Status.FAIL;
            }

        }

    }
}
