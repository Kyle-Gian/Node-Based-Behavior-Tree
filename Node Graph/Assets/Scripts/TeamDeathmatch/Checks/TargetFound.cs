//Author: Kyle Gian
//Date Created: 16/05/2021
//Last Modified: 16/05/2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class TargetFound: NodeCheck
{
    
    public override TreeNode.Status CheckCondition(GameObject AI)
    {

        if (AI.GetComponent<Target>().GetTarget() != null)
        {
            return TreeNode.Status.SUCCESS;
        }
        return TreeNode.Status.FAIL;
    }
}
