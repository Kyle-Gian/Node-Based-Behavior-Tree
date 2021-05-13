//Author: Kyle Gian
//Date Created: 29/04/2021
//Last Modified: 10/05/2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCheck : NodeCheck
{
    public bool _testCheck = true;

    GameObject _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    public override TreeNode.Status CheckCondition(GameObject AI)
    {
        
        if (Vector3.Distance(_player.transform.position, AI.transform.position) < 20  )
        {
            return TreeNode.Status.SUCCESS;
        }
        return TreeNode.Status.FAIL;
    }
}
