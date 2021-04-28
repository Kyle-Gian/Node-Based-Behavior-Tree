//Author: Kyle Gian
//Date Created: 23/04/2021
//Last Modified: 23/04/2021


using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class LeafNode : AINode
{
    public LeafNode CreateLeafNode(string nodeName, Vector2 position)
    {
        LeafNode newLeafNode = new LeafNode();

        newLeafNode.title = "Leaf";
        newLeafNode._GUID = Guid.NewGuid().ToString();
        newLeafNode._NodeType = GetType().ToString();

        var inputPort = GeneratePort(newLeafNode, Direction.Input);
        inputPort.portName = "Input";
        newLeafNode.inputContainer.Add(inputPort);

        // Add stylesheet to the node colors
        newLeafNode.styleSheets.Add(Resources.Load<StyleSheet>("Node"));

        var textField = new TextField(string.Empty);
        textField.SetValueWithoutNotify("Node Name");


        ObjectField objectField = new ObjectField();
        objectField.label = "Script to Check:";
        objectField.objectType = typeof(NodeCheck);

        newLeafNode.titleContainer.Add(textField);
        newLeafNode.mainContainer.Add(objectField);

        var addOutput = GeneratePort(newLeafNode, Direction.Output);
        addOutput.portName = "Output";
        newLeafNode.outputContainer.Add(addOutput);

        newLeafNode.RefreshExpandedState();
        newLeafNode.RefreshPorts();
        newLeafNode.SetPosition(new Rect(position, defaultNodeSize));

        return newLeafNode;
    }
}
