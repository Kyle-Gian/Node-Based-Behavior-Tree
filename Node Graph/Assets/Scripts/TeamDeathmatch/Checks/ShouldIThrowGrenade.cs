//Author: Kyle Gian
//Date Created: 02/05/2021
//Last Modified: 02/05/2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using NodeBasedBehaviourTree;

[System.Serializable]
public class ShouldIThrowGrenade : NodeCheck
{
    GameObject _grenade;

    GameObject[] _teamBlue;
    GameObject[] _teamRed;

    GameObject _closestPlayer = null;

    private void Start()
    {
        _teamBlue = GameObject.FindGameObjectsWithTag("Red");
        _teamRed = GameObject.FindGameObjectsWithTag("Blue");
    }

    public override TreeNode.Status CheckCondition(GameObject AI)
    {

        Utilities grenades = AI.GetComponent<Utilities>();
        if (grenades._currentGrenades > 0 && !AreTeamMatesInBlastRadius(AI, _closestPlayer))
        {
            return TreeNode.Status.SUCCESS;

        }
        return TreeNode.Status.FAIL;
    }

    private bool AreTeamMatesInBlastRadius(GameObject AI, GameObject enemy)
    {
        float grenadeRadius = AI.GetComponent<Utilities>()._grenadeRadius;

        Collider[] radiusAroundEnemy = Physics.OverlapSphere(enemy.transform.position, grenadeRadius, enemy.layer);


        for (int i = 0; i < radiusAroundEnemy.Length; i++)
        {
            if (radiusAroundEnemy[i].tag == AI.tag)
            {
                return true;
            }
        }
        return false;
    }
}
