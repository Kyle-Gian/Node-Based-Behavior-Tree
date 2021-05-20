//Author: Kyle Gian
//Date Created: 19/05/2021
//Last Modified: 19/05/2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using NodeBasedBehaviourTree;

//Checks if the entity is being shot

[System.Serializable]
public class GettingShot: NodeCheck
{
    public override TreeNode.Status CheckCondition(GameObject AI)
    {
        return TreeNode.Status.PROCESSING;
    }
}
