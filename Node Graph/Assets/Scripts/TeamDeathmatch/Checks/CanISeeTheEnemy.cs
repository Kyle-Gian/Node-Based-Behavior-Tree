//Author: Kyle Gian
//Date Created: 13/05/2021
//Last Modified: 13/05/2021


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NodeBasedBehaviourTree;

public class CanISeeTheEnemy : NodeCheck
{
    GameObject[] _teamBlue;
    GameObject[] _teamRed;

    public float _distanceCheck = 15;
    public GameObject _closestPlayer;
    float _distanceFromPreviousAI = 0;
    public LayerMask _enemy;


    private void Start()
    {
        _teamBlue = GameObject.FindGameObjectsWithTag("Blue");
        _teamRed = GameObject.FindGameObjectsWithTag("Red");

    }

    public override TreeNode.Status CheckCondition(GameObject AI)
    {
        //Check what team the AI is on
        if (AI.CompareTag("Red"))
        {
            _distanceFromPreviousAI = 0;
            _closestPlayer = null;

            foreach (var blue in _teamBlue)
            {
                //If blue team member is killed do not check
                if (blue != null && blue.activeSelf == true)
                {
                    float distanceToTarget = Vector3.Distance(AI.transform.position, blue.transform.position);
                    if (distanceToTarget < _distanceCheck)
                    {
                        
                        if (distanceToTarget < _distanceFromPreviousAI || _distanceFromPreviousAI == 0)
                        {
                            _closestPlayer = blue;
                            _distanceFromPreviousAI = Vector3.Distance(AI.transform.position, blue.transform.position);
                        }
                    }
                }

            }

        }
        //Check what team the AI is on
        if (AI.CompareTag("Blue"))
        {
            _distanceFromPreviousAI = 0;
            _closestPlayer = null;
            foreach (var red in _teamRed)
            {
                //If red team member is killed do not check
                if (red != null && red.activeSelf == true)
                {
                    float distanceToTarget = Vector3.Distance(AI.transform.position, red.transform.position);

                    if (distanceToTarget < _distanceCheck)
                    {

                        if (distanceToTarget < _distanceFromPreviousAI || _distanceFromPreviousAI == 0)
                        {
                            _closestPlayer = red;
                            _distanceFromPreviousAI = Vector3.Distance(AI.transform.position, red.transform.position);
                        }
                    }
                }

            }
        }

        if (_closestPlayer != null && _closestPlayer.activeSelf == true)
        {
            AI.GetComponent<Target>().SetTarget(_closestPlayer); 
            return TreeNode.Status.FAIL;
            
        }
        
        AI.GetComponent<Target>().SetTarget(null);

        return TreeNode.Status.SUCCESS;
    }
}
