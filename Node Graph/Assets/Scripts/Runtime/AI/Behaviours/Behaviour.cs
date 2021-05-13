//Author: Kyle Gian
//Date Created: 23/04/2021
//Last Modified: 11/05/2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Behaviour : NodeCheck
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

    public virtual Behaviour GetBehaviour()
    {
        return null;
    }

    public virtual TreeNode.Status ReturnBehaviorStatus()
    {
        return TreeNode.Status.PROCESSING;
    }

    public virtual void SetBehaviourStatus(TreeNode.Status status)
    {
        _currentStatus = status;

    }

    public virtual Vector3 GetObjectPosition()
    {
        return transform.position;
    }
}
