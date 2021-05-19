using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class DoINeedHealth: NodeCheck
{
    private float _maxHealth = 0;
    public override TreeNode.Status CheckCondition(GameObject AI)
    {
        if (_maxHealth == 0)
        {
            _maxHealth = AI.GetComponent<AIHealth>()._maxHealth;

        }
        float health = AI.GetComponent<AIHealth>()._currentHealth;
        
        if (health <= _maxHealth / 2 )
        {
            return TreeNode.Status.SUCCESS;
        }
        
        return TreeNode.Status.FAIL;
    }
    
}
