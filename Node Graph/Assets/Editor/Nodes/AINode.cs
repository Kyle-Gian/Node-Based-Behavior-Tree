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
    public string _functionOfNodeName;

    public bool _entryPoint = false;

    enum NodeStatus
    {
        Success,
        Failure,
        Processing
    }
    public Port GeneratePort(AINode node, Direction portDirection)
    {
        Port.Capacity capacity;
        //Checks the node type based off the node passed through and changes if the node needs to have multi ports or just single
        if (node.GetType() == typeof(SelectorNode) || node.GetType() == typeof(RootNode) || node.GetType() == typeof(SequenceNode))
        {
            capacity = Port.Capacity.Multi;
            return node.InstantiatePort(Orientation.Horizontal, portDirection, capacity, typeof(float));
        }
        else
        {
            capacity = Port.Capacity.Single;
            return node.InstantiatePort(Orientation.Horizontal, portDirection, capacity, typeof(float));
        }

    }

}
