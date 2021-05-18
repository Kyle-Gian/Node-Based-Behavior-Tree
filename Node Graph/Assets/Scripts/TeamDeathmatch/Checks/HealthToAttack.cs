using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class HealthToAttack: NodeCheck
{
    public override TreeNode.Status CheckCondition(GameObject AI)
    {
        float health = AI.GetComponent<AIHealth>()._currentHealth;
        
        if (health > health / 2 )
        {
            return TreeNode.Status.SUCCESS;
        }
        
        return TreeNode.Status.FAIL;
    }
}
