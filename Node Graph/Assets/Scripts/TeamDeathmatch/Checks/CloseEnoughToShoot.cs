using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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
