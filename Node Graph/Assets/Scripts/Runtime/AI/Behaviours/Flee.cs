using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Flee : Behaviour
{
    NavMeshAgent _agent;
    GameObject _player;
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

    public override Behaviour GetBehaviour()
    {
        return this.GetComponent<Chase>();
    }
}
