using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCheck : NodeCheck
{
    GameObject _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    public TreeNode.Status CheckCondition()
    {
        
        if (Vector3.Distance(_player.transform.position, this.transform.position) > 0)
        {
            Debug.Log(TreeNode.Status.SUCCESS);
            return TreeNode.Status.SUCCESS;
        }
        return TreeNode.Status.FAIL;
    }
}
