//Author: Kyle Gian
//Date Created: 23/04/2021
//Last Modified: 23/04/2021

using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class SelectorNode : AINode
{
    public SelectorNode CreateSelectorNode(string nodeName, Vector2 position)
    {
        SelectorNode newSelectorNode = new SelectorNode();
        newSelectorNode.title = "Selector";
        newSelectorNode._GUID = Guid.NewGuid().ToString();
        newSelectorNode._NodeType = GetType().ToString();

        var inputPort = GeneratePort(newSelectorNode, Direction.Input);
        inputPort.portName = "Input";
        newSelectorNode.inputContainer.Add(inputPort);

        // Add stylesheet to the node colors
        newSelectorNode.styleSheets.Add(Resources.Load<StyleSheet>("Node"));

        var textField = new TextField(string.Empty);

        textField.RegisterValueChangedCallback(evt =>
        {
            newSelectorNode._NodeType = evt.newValue;
            newSelectorNode.title = evt.newValue;
        });

        textField.SetValueWithoutNotify(newSelectorNode.title);
        newSelectorNode.mainContainer.Add(textField);

        var addOutput = GeneratePort(newSelectorNode, Direction.Output);
        addOutput.portName = "Output";

        newSelectorNode.outputContainer.Add(addOutput);

        newSelectorNode.RefreshExpandedState();
        newSelectorNode.RefreshPorts();
        newSelectorNode.SetPosition(new Rect(position, defaultNodeSize));

        return newSelectorNode;
    }
}
