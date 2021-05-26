//Author: Kyle Gian
//Date Created: 25/04/2021
//Last Modified: 29/04/2021

namespace NodeBasedBehaviourTree
{
    using System;
    using UnityEngine;
    using UnityEditor.Experimental.GraphView;
    using UnityEngine.UIElements;
    using UnityEditor.UIElements;

    public class DecoratorNode : AINode
    {
        public DecoratorNode()
        {
            _NodeType = "decoratornode";
            _GUID = Guid.NewGuid().ToString();
        }

        DecoratorNode(string nodeName, Vector2 position, string guid)
        {
            _NodeType = nodeName;
            _position = position;
            _GUID = guid;
        }

        public DecoratorNode CreateDecoratorNode(string nodeName, Vector2 position, string guid)
        {
            DecoratorNode newDecoratorNode = new DecoratorNode();

            newDecoratorNode.title = "Decorator";
            if (guid == null)
            {
                newDecoratorNode._GUID = Guid.NewGuid().ToString();

            }
            newDecoratorNode._NodeType = GetType().ToString();



            var inputPort = GeneratePort(newDecoratorNode, Direction.Input);
            inputPort.portName = "Input";
            newDecoratorNode.inputContainer.Add(inputPort);

            // Add stylesheet to the node colors
            newDecoratorNode.styleSheets.Add(Resources.Load<StyleSheet>("Node"));

            var textField = new TextField(string.Empty);
            ObjectField objectField = new ObjectField();

            textField.RegisterValueChangedCallback(evt =>
            {
                newDecoratorNode._NodeType = evt.newValue;
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
}