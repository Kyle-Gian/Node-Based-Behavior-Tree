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
    public override TreeNode.Status CheckCondition(Transform AI)
    {
        
        if (Vector3.Distance(_player.transform.position, AI.position) > 20 && !_testCheck )
        {
            return TreeNode.Status.SUCCESS;
        }
        return TreeNode.Status.FAIL;
    }
}
