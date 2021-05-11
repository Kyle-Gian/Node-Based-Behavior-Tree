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

    Behaviour[] _behaviours;
    // Start is called before the first frame update
    void Start()
    {
        _behaviours = this.GetComponents<Behaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Behaviour ActivatedBehaviour()
    {
        foreach (var behaviour in _behaviours)
        {
            if (behaviour.name == )
            {

            }

        }
        return _defaultBehaviour;
    }

    public string GetBehaviour()
    {
        return null;
    }
}
