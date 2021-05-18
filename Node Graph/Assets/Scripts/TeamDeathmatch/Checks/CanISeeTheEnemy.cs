//Author: Kyle Gian
//Date Created: 13/05/2021
//Last Modified: 13/05/2021


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CanISeeTheEnemy : NodeCheck
{
    GameObject[] _teamBlue;
    GameObject[] _teamRed;

    public float _distanceCheck = 15;
    public GameObject _closestPlayer = null;
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
        if (AI.tag == "Red")
        {
            //_closestPlayer = (GameObject)_teamBlue.GetValue(0);

            foreach (var blue in _teamBlue)
            {
                //If blue team member is killed do not check
                if (blue != null)
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
                    else
                    {
                        AI.GetComponent<Target>().SetTarget(null);

                    }
                }

            }

        }
        //Check what team the AI is on
        if (AI.tag == "Blue")
        {
            //_closestPlayer = (GameObject)_teamBlue.GetValue(0);

            foreach (var red in _teamRed)
            {
                //If red team member is killed do not check
                if (red != null)
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
                    else
                    {
                        AI.GetComponent<Target>().SetTarget(null);

                    }
                }
            }
        }

        if (_closestPlayer != null)
        {
            //Check if the AI can see the enemy target
            if (Vector3.Distance(AI.transform.position ,_closestPlayer.transform.position) < _distanceCheck)
            {
                AI.GetComponent<Target>().SetTarget(_closestPlayer);
                return TreeNode.Status.FAIL;
            }
        }
        
        return TreeNode.Status.SUCCESS;
    }

    public bool HasLineOfSight(GameObject AI, GameObject target)
    {

        if (Physics.Raycast(AI.transform.position, AI.transform.TransformDirection(Vector3.forward), out RaycastHit hit))
        {
            if (hit.collider.tag == target.tag)
            {
                return true;
            }
        }
        return false;

    }
}
