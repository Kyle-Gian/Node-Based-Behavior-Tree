//Author: Kyle Gian
//Date Created: 16/05/2021
//Last Modified: 21/05/2021

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
            //Checks if the Health pack has been set for the AI
            if (!_healthPackSet)
            {
                _closestHealthPack = null;
                for (int i = 0; i < _nearbyHealthPacks.Length; i++)
                {
                    //Check Health pack is active
                    if (_nearbyHealthPacks[i].activeSelf)
                    {
                        //Set the first health pack if not set
                        if (_closestHealthPack == null)
                        {
                            _closestHealthPack = _nearbyHealthPacks[i];

                        }
                        
                        //checks the distance between the closest health pack and current list health pack
                        float distanceToHealthPackI =
                        Vector3.Distance(AI.transform.position, _nearbyHealthPacks[i].transform.position);

                        float distanceToHealthPackJ = 
                            Vector3.Distance(AI.transform.position, _closestHealthPack.transform.position);
                        
                        if (distanceToHealthPackI <= distanceToHealthPackJ)
                        {
                            //If the healthpack is closer set as the closest health pack, and set pack 
                            // to targeted
                            if (!_nearbyHealthPacks[i].GetComponent<HealthPack>().IsObjectTargeted())
                            {
                                _nearbyHealthPacks[i].GetComponent<HealthPack>().SetTargetingObject(_nearbyHealthPacks[i]);
                                _closestHealthPack = _nearbyHealthPacks[i];
                                _healthPackSet = true;
                            }

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
                //Set the health pack as a target for the entity
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
