//Author: Kyle Gian
//Date Created: 13/05/2021
//Last Modified: 13/05/2021

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Target : MonoBehaviour
{
    //private NavMeshAgent _agent;

    private GameObject _target = null;
    public void SetTarget(GameObject target)
    {
        _target = target;
    }

    public GameObject GetTarget()
    {
        return _target;
    }
    

}
