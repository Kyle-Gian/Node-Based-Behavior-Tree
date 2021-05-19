﻿//Author: Kyle Gian
//Date Created: 16/05/2021
//Last Modified: 19/05/2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GetHealth : Behaviour
{
    private GameObject _healthPack;
    private NavMeshAgent _agent;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_healthPack != null)
        {
            _agent.SetDestination(_healthPack.transform.position);

        }
    }

    public override Behaviour GetBehaviour()
    {
        return GetComponent<GetHealth>();
    }

    public override TreeNode.Status ReturnBehaviorStatus()
    {
        return TreeNode.Status.PROCESSING;
    }

    public override void SetBehaviourStatus(TreeNode.Status status)
    {
        _currentStatus = status;

    }

    public override Vector3 GetObjectPosition()
    {
        return transform.position;
    }
    
    public GameObject GetHealthPack()
    {
        return _healthPack;
    }

    public void SetHealthPack(GameObject healthPack)
    {
        _healthPack = healthPack;
    }
}