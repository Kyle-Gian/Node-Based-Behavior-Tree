//Author: Kyle Gian
//Date Created: 23/04/2021
//Last Modified: 29/04/2021
using System;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

public class SequenceNode : AINode
{
    public SequenceNode()
    {
        _NodeType = "sequenceNode";
        _GUID = Guid.NewGuid().ToString();

    }

    SequenceNode(string nodeName, Vector2 position, string guid)
    {
        _NodeType = nodeName;
        _position = position;
        _GUID = guid;
    }
    public SequenceNode CreateSequenceNode(string nodeName, Vector2 position , string guid)
    {
        SequenceNode newSequenceNode = new SequenceNode();

        newSequenceNode.title = "Sequence";
        if (guid == null)
        {
            newSequenceNode._GUID = Guid.NewGuid().ToString();

        }
        newSequenceNode._NodeType = GetType().ToString();

        var inputPort = GeneratePort(newSequenceNode, Direction.Input);
        inputPort.portName = "Input";
        newSequenceNode.inputContainer.Add(inputPort);

        // Add stylesheet to the node colors
        newSequenceNode.styleSheets.Add(Resources.Load<StyleSheet>("Node"));

        var textField = new TextField(string.Empty);

        textField.RegisterValueChangedCallback(evt =>
        {
            newSequenceNode._NodeType = evt.newValue;
            newSequenceNode.title = evt.newValue;
        });

        textField.SetValueWithoutNotify(newSequenceNode.title);
        newSequenceNode.mainContainer.Add(textField);

        var addOutput = GeneratePort(newSequenceNode, Direction.Output);
        addOutput.portName = "Output";
        newSequenceNode.outputContainer.Add(addOutput);

        newSequenceNode.RefreshExpandedState();
        newSequenceNode.RefreshPorts();
        newSequenceNode.SetPosition(new Rect(position, defaultNodeSize));

        return newSequenceNode;
    }

}
