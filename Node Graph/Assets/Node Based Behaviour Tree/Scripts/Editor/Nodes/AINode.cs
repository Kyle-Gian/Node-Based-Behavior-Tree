//Author: Kyle Gian
//Date Created: 23/04/2021
//Last Modified: 30/04/2021

namespace NodeBasedBehaviourTree
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;
    using UnityEditor.Experimental.GraphView;
    using UnityEngine.UIElements;
    using UnityEditor.UIElements;

    [System.Serializable]
    public class AINode : Node
    {
        public readonly Vector2 defaultNodeSize = new Vector2(x: 150, y: 200);

        public string _GUID;
        public string _NodeType;
        public Vector2 _position;
        public bool _entryPoint = false;

        enum NodeStatus
        {
            Success,
            Failure,
            Running
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

        public List<Edge> GetOutputEdgeList(Port a_outputPort)
        {
            List<Edge> edges = new List<Edge>();

            foreach (var edge in a_outputPort.connections)
            {
                edges.Add(edge);
            }

            return edges;

        }

        public AINode NodeClass(string nodeName)
        {

            switch (nodeName.ToLower())
            {
                case "leafnode":
                    return new LeafNode();
                case "selectornode":
                    return new SelectorNode();
                case "sequencenode":
                    return new SequenceNode();
                case "decoratornode":
                    return new DecoratorNode();
                default:
                    Debug.LogError("Bad Node Creation");
                    return new AINode();
            }
        }

        public AINode CreateNode(string nodeName, Vector2 position, string guid, MonoScript a_function, string a_title)
        {
            //Removes the namespace from the received node name
            if (nodeName.Contains("NodeBasedBehaviourTree"))
            {
                nodeName = nodeName.Remove(0, 23);

            }

            //Create the node type
            AINode newNode = NodeClass(nodeName);
            newNode.title = nodeName;

            //if the node has no ID give it one
            if (guid == null)
            {
                newNode._GUID = Guid.NewGuid().ToString();
            }
            newNode._NodeType = nodeName;

            var inputPort = GeneratePort(newNode, Direction.Input);
            inputPort.portName = "Input";
            newNode.inputContainer.Add(inputPort);

            // Add stylesheet to the node colors
            newNode.styleSheets.Add(Resources.Load<StyleSheet>("Node"));

            var textField = new TextField(string.Empty);

            
            textField.RegisterValueChangedCallback(evt =>
            {
                textField.value = evt.newValue;
            });

            //textField.SetValueWithoutNotify(textField.value.ToString());
            
            if (nodeName == "LeafNode" || nodeName == "DecoratorNode")
            {
                newNode.mainContainer.Add(AddObjectField(a_function));
                newNode.titleContainer.Add(textField);
                textField.value = a_title;

            }

            newNode.tooltip = AddToolTipToNode(nodeName);

            var addOutput = GeneratePort(newNode, Direction.Output);
            addOutput.portName = "Output";
            newNode.outputContainer.Add(addOutput);

            newNode.RefreshExpandedState();
            newNode.RefreshPorts();
            newNode.SetPosition(new Rect(position, defaultNodeSize));

            return newNode;
        }

        public ObjectField AddObjectField(MonoScript function)
        {
            ObjectField objectField = new ObjectField();
            objectField.label = "Script to Check:";
            objectField.name = "Function";
            objectField.objectType = typeof(MonoScript);
            objectField.tooltip = "Add a Node Check Or AIBehaviour Script Here";

            if (function != null)
            {
                objectField.value = function;
            }

            return objectField;
        }

        public string AddToolTipToNode(string nodeType)
        {
            string toolTip = "";
            switch (nodeType.ToLower())
            {
                case "leafnode":
                    toolTip = "Action or Check node. Used to run a Behaviour or return Success/Failure based off given script";
                    break;
                case "decoratornode":
                    toolTip = "Decorator Node, also known as Inverter. Can change the outcome based off the script attached!";
                    break;
                case "sequencenode":
                    toolTip = "Selector Node, It runs through the children nodes until a child returns Fail or all children return Success";
                    break;
                case "selectornode":
                    toolTip = "Sequence Node, This node will run through it's children until one returns Success.";
                    break;
            }

            return toolTip;
        }

    }
}