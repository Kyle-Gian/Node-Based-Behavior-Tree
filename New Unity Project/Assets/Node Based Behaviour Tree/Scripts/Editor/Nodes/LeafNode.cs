//Author: Kyle Gian
//Date Created: 23/04/2021
//Last Modified: 29/04/2021


namespace NodeBasedBehaviourTree
{
    using System;
    using UnityEngine;
    using UnityEditor.Experimental.GraphView;
    using UnityEngine.UIElements;
    using UnityEditor.UIElements;

    public class LeafNode : AINode
    {
        public LeafNode()
        {
            _NodeType = "leafnode";
            _GUID = Guid.NewGuid().ToString();

        }

        LeafNode(string nodeName, Vector2 position, string guid)
        {
            _NodeType = nodeName;
            _position = position;
            _GUID = guid;
        }

        public AINode CreateNode(Vector2 position, string guid)
        {
            return this;
        }

        public LeafNode CreateLeafNode(string nodeName, Vector2 position, string guid)
        {
            LeafNode newLeafNode = new LeafNode();

            newLeafNode.title = "Leaf";

            if (guid == null)
            {
                newLeafNode._GUID = Guid.NewGuid().ToString();

            }
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
}