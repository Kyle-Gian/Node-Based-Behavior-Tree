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
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        _agent.SetDestination(_player.transform.position);
    }

    public override Behaviour GetBehaviour()
    {
        return this.GetComponent<Chase>();
    }
}
