﻿//Author: Kyle Gian
//Date Created: 13/05/2021
//Last Modified: 13/05/2021

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SeekEnemy : Behaviour
{
    NavMeshAgent agent;
    Vector3 _location;
    private float _distanceToDestination = 0;
    private Vector3 _previousDestination;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
        _distanceToDestination = Vector3.Distance(gameObject.transform.localPosition, _location);

        if (_location == Vector3.zero || _previousDestination == _location)
        {
            _location = RandomNavSphere(gameObject.transform.localPosition, 10, -1);
            agent.SetDestination(_location);
        }

        if (_distanceToDestination <= 1)
        {
            _location = RandomNavSphere(gameObject.transform.localPosition, 10, -1);
            agent.SetDestination(_location);
        }
        if (Physics.Raycast(gameObject.transform.localPosition, gameObject.transform.TransformDirection(Vector3.forward), out RaycastHit hit,10))
        {
            if (hit.collider.tag == "Obstruction")
            {
                _location = RandomNavSphere(gameObject.transform.localPosition, 10, -1);
                agent.SetDestination(_location);
            }
        }

        if (agent.velocity == Vector3.zero)
        {
            _location = RandomNavSphere(gameObject.transform.localPosition, 10, -1);
            agent.SetDestination(_location);
        }

    }

    private void OnEnable()
    {
        //Used for the change of behaviour, so the position is changed when reactivated
        _previousDestination = _location;
    }

    public override Behaviour GetBehaviour()
    {
        return GetComponent<SeekEnemy>();
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;

        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

        return navHit.position;
    }

    public override TreeNode.Status ReturnBehaviorStatus()
    {
        return TreeNode.Status.PROCESSING;
    }

    public override void SetBehaviourStatus(TreeNode.Status status)
    {
        _currentStatus = status;

    }

}
