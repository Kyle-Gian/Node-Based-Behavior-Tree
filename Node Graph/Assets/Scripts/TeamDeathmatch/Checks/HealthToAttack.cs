//Author: Kyle Gian
//Date Created: 10/05/2021
//Last Modified: 12/05/2021

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using NodeBasedBehaviourTree;

//Check the entity has enough health to attack
[System.Serializable]
public class HealthToAttack: NodeCheck
{
    private float _maxHealth;
    public override TreeNode.Status CheckCondition(GameObject AI)
    {

        if (_maxHealth == 0)
        {
            _maxHealth = AI.GetComponent<AIHealth>()._maxHealth;

        }
        float health = AI.GetComponent<AIHealth>()._currentHealth;
        
        if (health > _maxHealth / 2 )
        {
            return TreeNode.Status.SUCCESS;
        }
        
        return TreeNode.Status.FAIL;
    }
}
