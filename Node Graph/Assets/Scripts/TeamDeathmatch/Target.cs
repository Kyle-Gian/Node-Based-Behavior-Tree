using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Target : MonoBehaviour
{
    //private NavMeshAgent _agent;

    private GameObject _target;
    // Update is called once per frame

    private void Start()
    {
        throw new NotImplementedException();
    }

    void Update()
    {
        
    }

    public void SetTarget(GameObject target)
    {
        _target = target;
    }

    public GameObject GetTarget()
    {
        return _target;
    }
    

}
