﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class IsHealthNearBy: NodeCheck
{
    public override TreeNode.Status CheckCondition(GameObject AI)
    {
        return TreeNode.Status.PROCESSING;
    }
}
