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
     GameObject _target;
     Vector3 _destination;
     private Vector3 _distanceAroundTarget = new Vector3(2,0,2);
     public bool _destinationReached = false;


    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
        _target = GetComponent<Target>().GetTarget();

        if (_target != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, _target.transform.position);
            if (_target != null && distanceToTarget > 8)
            {
                _destination = _target.transform.position + _distanceAroundTarget;
                if (LineOfSightBlocked())
                {
                    _destination = _target.transform.position - _distanceAroundTarget;
                }
                _agent.SetDestination(_destination);
            }
        }

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

    public bool LineOfSightBlocked()
    {
        string enemytag;
        if (this.CompareTag("Blue"))
        {
            enemytag = "Red";
        }
        else
        {
            enemytag = "Blue";
        }
        if (Physics.Raycast(transform.position,transform.forward,out RaycastHit hit, 5, 10))
        {
            if (hit.collider.CompareTag(enemytag))
            {
                return false;
            }
        }
        return true;
    }
}
