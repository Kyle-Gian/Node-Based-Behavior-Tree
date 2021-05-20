﻿//Author: Kyle Gian
//Date Created: 19/05/2021
//Last Modified: 19/05/2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NodeBasedBehaviourTree;

public class ChangeTarget : AIBehaviour
{
    public TreeNode.Status _currentStatus;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override AIBehaviour GetBehaviour()
    {
        return null;
    }

    public override TreeNode.Status ReturnBehaviorStatus()
    {
        return TreeNode.Status.PROCESSING;
    }

    public override void SetBehaviourStatus(TreeNode.Status status)
    {
        _currentStatus = status;

    }

    public override Vector3 GetObjectPosition()
    {
        return transform.position;
    }
}
