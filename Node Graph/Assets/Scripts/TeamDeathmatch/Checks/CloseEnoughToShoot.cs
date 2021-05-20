//Author: Kyle Gian
//Date Created: 10/05/2021
//Last Modified: 16/05/2021


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using NodeBasedBehaviourTree;

//Checks if the entity is within range to shoot
[System.Serializable]
public class CloseEnoughToShoot: NodeCheck
{
    public float _attackDiastance = 20;
    public override TreeNode.Status CheckCondition(GameObject AI)
    {
        float distanceToTarget =
            Vector3.Distance(AI.transform.position, AI.GetComponent<Target>().GetTarget().transform.position);

        if (distanceToTarget < _attackDiastance)
        {
            return TreeNode.Status.SUCCESS;
        }
        
        return TreeNode.Status.FAIL;
    }
}
