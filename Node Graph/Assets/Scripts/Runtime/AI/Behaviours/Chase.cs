//Author: Kyle Gian
//Date Created: 23/04/2021
//Last Modified: 11/05/2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : Behaviour
{
     NavMeshAgent _agent;
     GameObject _player;
     Vector3 _destination;
     public bool _destinationReached = false;


    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player");

        _destination = _player.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        _destination = _player.transform.position;

        _agent.SetDestination(_destination);     
    }

    public override Behaviour GetBehaviour()
    {
        return GetComponent<Chase>();
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
