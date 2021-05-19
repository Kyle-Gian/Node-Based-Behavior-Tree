using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class IsHealthAvailable: NodeCheck
{
    private GameObject[] _healthPacks;

    private void Start()
    {
        _healthPacks = GameObject.FindGameObjectsWithTag("Health Pack");
    }

    public override TreeNode.Status CheckCondition(GameObject AI)
    {
        if (_healthPacks != null)
        {
            for (int i = 0; i < _healthPacks.Length; i++)
            {
                if (_healthPacks[i].activeSelf)
                {
                    return TreeNode.Status.SUCCESS;

                }
            }
        }

        return TreeNode.Status.FAIL;

    }
}
