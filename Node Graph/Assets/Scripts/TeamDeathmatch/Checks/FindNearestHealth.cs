using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class FindNearestHealth: NodeCheck
{
    private GameObject[] _nearbyHealthPacks;
    GameObject _closestHealthPack = null;
    private bool _healthPackSet = false;
    
    [SerializeField] private float _distanceCheck = 20;

    private void Start()
    {
        _nearbyHealthPacks = GameObject.FindGameObjectsWithTag("Health Pack");
    }

    public override TreeNode.Status CheckCondition(GameObject AI)
    {
        if (_nearbyHealthPacks != null)
        {

            if (!_healthPackSet)
            {
                _closestHealthPack = null;
                for (int i = 0; i < _nearbyHealthPacks.Length; i++)
                {
                    if (_nearbyHealthPacks[i].activeSelf)
                    {
                        if (_closestHealthPack == null)
                        {
                            _closestHealthPack = _nearbyHealthPacks[i];

                        }

                        float distanceToHealthPackI =
                        Vector3.Distance(AI.transform.position, _nearbyHealthPacks[i].transform.position);

                        float distanceToHealthPackJ = 
                            Vector3.Distance(AI.transform.position, _closestHealthPack.transform.position);
                        
                        if (distanceToHealthPackI <= distanceToHealthPackJ)
                        {
                            _closestHealthPack = _nearbyHealthPacks[i];
                            _healthPackSet = true;

                        }
                                
                            
                        
                    }
                }

            }



            if (_closestHealthPack != null)
            {
                if (!_closestHealthPack.activeSelf)
                {
                    _healthPackSet = false;
                }
                if (_healthPackSet)
                {
                    AI.GetComponent<GetHealth>().SetHealthPack(_closestHealthPack);

                }
                return TreeNode.Status.SUCCESS;

            }
            


        }

        return TreeNode.Status.FAIL;

    }
}
