//Author: Kyle Gian
//Date Created: 19/05/2021
//Last Modified: 19/05/2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//Checks for an enemy in the near area by checking the overlapping sphere

[System.Serializable]
public class EnemyInMyArea: NodeCheck
{
    public override TreeNode.Status CheckCondition(GameObject AI)
    {
        if (AI.CompareTag("Blue"))
        {
            Physics.OverlapSphere(AI.transform.position, 5, 8);
            return TreeNode.Status.FAIL;
        }
        
        if (AI.CompareTag("Red"))
        {
            Physics.OverlapSphere(AI.transform.position, 5, 9);
            return TreeNode.Status.FAIL;
        }

        return TreeNode.Status.SUCCESS;
    }
}
