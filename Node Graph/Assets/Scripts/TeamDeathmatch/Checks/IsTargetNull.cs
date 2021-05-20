//Author: Kyle Gian
//Date Created: 10/05/2021
//Last Modified: 10/05/2021
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using NodeBasedBehaviourTree;


[System.Serializable]
public class IsTargetNull: NodeCheck
{
    public override TreeNode.Status CheckCondition(GameObject AI)
    {
        if (AI.GetComponent<Target>().GetTarget() == null)
        {
            return TreeNode.Status.SUCCESS;
        }
        
        return TreeNode.Status.FAIL;
    }
}
