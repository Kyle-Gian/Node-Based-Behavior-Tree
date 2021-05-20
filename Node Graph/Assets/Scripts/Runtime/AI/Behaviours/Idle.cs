//Author: Kyle Gian
//Date Created: 13/05/2021
//Last Modified: 13/05/2021


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NodeBasedBehaviourTree;


public class Idle : AIBehaviour
{
    NavMeshAgent _agent;

    Vector3 _destination;
    // Start is called before the first frame update
    void Start()
    {
        _destination = Vector3.zero;
        _agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        _agent.SetDestination(_destination);
    }

    public override AIBehaviour GetBehaviour()
    {
        return this.GetComponent<Idle>();
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
