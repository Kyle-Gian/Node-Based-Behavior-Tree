//Author: Kyle Gian
//Date Created: 11/05/2021
//Last Modified: 11/05/2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class ActiveBehaviour : MonoBehaviour
{
    [SerializeField]
    Behaviour _defaultBehaviour;

    Behaviour _runningBehaviour;

    Behaviour[] _behaviours;
    // Start is called before the first frame update
    void Start()
    {
        _runningBehaviour = _defaultBehaviour;
        _behaviours = this.GetComponents<Behaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        RunActiveBehaviour(_runningBehaviour);

    }

    public void SetBehaviour(Behaviour newBehaviour)
    {
        foreach (var behaviour in _behaviours)
        {
            if (newBehaviour.GetType() == behaviour.GetType())
            {
                _runningBehaviour = behaviour;
            }

        }
    }

    public string GetBehaviour()
    {
        return null;
    }

    private void RunActiveBehaviour(Behaviour activeBehaviour)
    {
        foreach (var behaviour in _behaviours)
        {
            if (behaviour != activeBehaviour)
            {
                behaviour.enabled = false;
            }
        }
        if (!activeBehaviour.enabled)
        {
            activeBehaviour.enabled = true;

        }
    }
}
