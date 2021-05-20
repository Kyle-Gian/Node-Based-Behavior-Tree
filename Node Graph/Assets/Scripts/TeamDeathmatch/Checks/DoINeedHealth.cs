//Author: Kyle Gian
//Date Created: 16/05/2021
//Last Modified: 20/05/2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using NodeBasedBehaviourTree;

//Checks if the entity is at the health level where it needs health

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
