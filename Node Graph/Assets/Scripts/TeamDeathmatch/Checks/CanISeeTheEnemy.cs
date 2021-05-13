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

    GameObject _closestPlayer = null;
    float _distanceFromPreviousAI = 0;
    public LayerMask _enemy;


    private void Start()
    {
        _teamBlue = GameObject.FindGameObjectsWithTag("TeamRed");
        _teamRed = GameObject.FindGameObjectsWithTag("TeamBlue");

    }

    public override TreeNode.Status CheckCondition(GameObject AI)
    {
        //Check what team the AI is on
        if (AI.tag == "Red")
        {
            _closestPlayer = (GameObject)_teamBlue.GetValue(0);

            foreach (var blue in _teamBlue)
            {
                if (Vector3.Distance(AI.transform.position, blue.transform.position) < 20)
                {

                    if (Vector3.Distance(AI.transform.position, blue.transform.position) < _distanceFromPreviousAI)
                    {
                        _closestPlayer = blue;
                        _distanceFromPreviousAI = Vector3.Distance(AI.transform.position, blue.transform.position);
                    }
                }

            }

        }
        //Check what team the AI is on
        if (AI.tag == "Blue")
        {
            _closestPlayer = (GameObject)_teamBlue.GetValue(0);

            foreach (var red in _teamRed)
            {
                if (Vector3.Distance(AI.transform.position, red.transform.position) < 20)
                {

                    if (Vector3.Distance(AI.transform.position, red.transform.position) < _distanceFromPreviousAI)
                    {
                        _closestPlayer = red;
                        _distanceFromPreviousAI = Vector3.Distance(AI.transform.position, red.transform.position);
                    }
                }
            }
        }
        //Check if the AI can see the enemy target
        if (HasLineOfSight(AI ,_closestPlayer))
        {
            return TreeNode.Status.SUCCESS;
        }

        return TreeNode.Status.FAIL;
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
