//Author: Kyle Gian
//Date Created: 13/05/2021
//Last Modified: 13/05/2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SeekEnemy : Behaviour
{
    NavMeshAgent agent;
    Vector3 _location;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (_location == Vector3.zero)
        {
            _location = RandomNavSphere(gameObject.transform.position, 50, -1);
            agent.SetDestination(_location);
        }
        if (Vector3.Distance(gameObject.transform.position,_location) <= 1)
        {
            _location = RandomNavSphere(gameObject.transform.position, 50, -1);
            agent.SetDestination(_location);
        }
        if (Physics.Raycast(gameObject.transform.position, gameObject.transform.TransformDirection(Vector3.forward), out RaycastHit hit,20))
        {
            if (hit.collider.tag == "Obstruction")
            {
                _location = RandomNavSphere(gameObject.transform.position, 50, -1);
                agent.SetDestination(_location);
            }
        }

    }

    public override Behaviour GetBehaviour()
    {
        return this.GetComponent<SeekEnemy>();
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
