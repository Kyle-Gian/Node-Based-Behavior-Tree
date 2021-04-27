using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class DecoratorNode : AINode
{
    public DecoratorNode CreateDecoratorNode(string nodeName, Vector2 position)
    {
        DecoratorNode newDecoratorNode = new DecoratorNode();

        newDecoratorNode.title = "Decorator";


        var inputPort = GeneratePort(newDecoratorNode, Direction.Input);
        inputPort.portName = "Input";
        newDecoratorNode.inputContainer.Add(inputPort);

        // Add stylesheet to the node colors
        newDecoratorNode.styleSheets.Add(Resources.Load<StyleSheet>("Node"));

        var textField = new TextField(string.Empty);
        ObjectField objectField = new ObjectField();

        textField.RegisterValueChangedCallback(evt =>
        {
            newDecoratorNode._functionOfNodeName = evt.newValue;
            newDecoratorNode.title = evt.newValue;
        });

        textField.SetValueWithoutNotify(newDecoratorNode.title);
        newDecoratorNode.mainContainer.Add(textField);
        newDecoratorNode.topContainer.Add(objectField);



        newDecoratorNode.RefreshExpandedState();
        newDecoratorNode.RefreshPorts();
        newDecoratorNode.SetPosition(new Rect(position, defaultNodeSize));

        return newDecoratorNode;
    }
}
