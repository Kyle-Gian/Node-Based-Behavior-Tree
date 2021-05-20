//Author: Kyle Gian
//Date Created: 13/05/2021
//Last Modified: 13/05/2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NodeBasedBehaviourTree;

public class Flee : AIBehaviour
{
    NavMeshAgent _agent;
    GameObject _player;
    Vector3 _destination;
    bool _destinationReached = false;
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        _agent.SetDestination(Vector3.zero);

    }

    public override AIBehaviour GetBehaviour()
    {
        return this.GetComponent<Chase>();
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
