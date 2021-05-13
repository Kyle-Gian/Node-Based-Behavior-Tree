//Author: Kyle Gian
//Date Created: 13/05/2021
//Last Modified: 13/05/2021


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhatIsMyHealth : NodeCheck
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override TreeNode.Status CheckCondition(GameObject AI)
    {

        return TreeNode.Status.FAIL;
    }
}
