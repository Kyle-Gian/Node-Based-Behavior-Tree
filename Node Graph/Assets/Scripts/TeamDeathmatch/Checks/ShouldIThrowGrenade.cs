﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class ShouldIThrowGrenade : NodeCheck
{
    GameObject _grenade;

    GameObject[] _teamBlue;
    GameObject[] _teamRed;

    GameObject _closestPlayer = null;

    private void Start()
    {
        _teamBlue = GameObject.FindGameObjectsWithTag("TeamRed");
        _teamRed = GameObject.FindGameObjectsWithTag("TeamBlue");
        _closestPlayer = this.GetComponent<CanISeeTheEnemy>()._closestPlayer;

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
