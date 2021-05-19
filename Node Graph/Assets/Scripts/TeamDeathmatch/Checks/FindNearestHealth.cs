using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class FindNearestHealth: NodeCheck
{
    private GameObject[] _nearbyHealthPacks;

    [SerializeField] private float _distanceCheck = 20;

    public override TreeNode.Status CheckCondition(GameObject AI)
    {
        GameObject _closestHealthPack = null;

        _nearbyHealthPacks = GameObject.FindGameObjectsWithTag("Health Pack");
        if (_nearbyHealthPacks != null)
        {
            for (int i = 0; i < _nearbyHealthPacks.Length; i++)
            {
                if (_nearbyHealthPacks[i].activeSelf)
                {
                    float distanceToHealthPack =
                        Vector3.Distance(AI.transform.position, _nearbyHealthPacks[i].transform.position);

                    if (distanceToHealthPack < _distanceCheck)
                    {
                        _closestHealthPack = _nearbyHealthPacks[i];
                    }

                    if (_closestHealthPack != null)
                    {
                        if (Vector3.Distance(_closestHealthPack.transform.position, AI.transform.position) <
                            distanceToHealthPack)
                        {
                            _closestHealthPack = _nearbyHealthPacks[i];
                        }
                    }
                }
            }
            AI.GetComponent<GetHealth>().SetHealthPack(_closestHealthPack);
            
            return TreeNode.Status.SUCCESS;

        }

        return TreeNode.Status.FAIL;

    }
}
