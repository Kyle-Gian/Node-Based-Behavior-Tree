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
