//Author: Kyle Gian
//Date Created: 23/04/2021
//Last Modified: 23/04/2021



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class AINode : Node
{
    public readonly Vector2 defaultNodeSize = new Vector2(x: 150, y: 200);

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
