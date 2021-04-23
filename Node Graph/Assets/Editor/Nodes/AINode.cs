//Author: Kyle Gian
//Date Created: 23/04/2021
//Last Modified: 23/04/2021



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;

public class AINode : Node
{
    public string _GUID;
    public string _funcionOfNodeName;

    public bool _entryPoint = false;

    enum NodeStatus
    {
        Success,
        Failure,
        Processing
    }

}
