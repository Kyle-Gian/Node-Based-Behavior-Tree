﻿//Author: Kyle Gian
//Date Created: 30/04/2021
//Last Modified: 30/04/2021

using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LeafFunctionality : NodeFunctionality
{
    public override void RunFunction(LeafTreeNode node)
    {
        node._script.Invoke("CheckCondition",0f);

        node._script.Invoke("GetBehaviour", 0f);

    }
}
