﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class NewTarget: NodeCheck
{
    
    public override TreeNode.Status CheckCondition(GameObject AI)
    {
        GameObject newTarget;
        
        
        //AI.GetComponent<Target>().SetTarget(newTarget);
        return TreeNode.Status.PROCESSING;
    }
}
