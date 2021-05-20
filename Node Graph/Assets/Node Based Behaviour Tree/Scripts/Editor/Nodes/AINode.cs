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

        public enum NodeType
        {
            Null = 0,
            Leaf,
            Sequence,
            Selector,
            Decorator,
            Root
        }
        public NodeType nt;

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

        public List<Edge> GetOutputEdgeList(Port a_outputPort)
        {
            List<Edge> edges = new List<Edge>();

            foreach (var edge in a_outputPort.connections)
            {
                edges.Add(edge);
            }

            return edges;

        }

        public NodeType NodeSetter(string nodeName)
        {

            switch (nodeName.ToLower())
            {
                case "leafnode":
                    return NodeType.Leaf;
                case "selector":
                    return NodeType.Selector;
                case "sequence":
                    return NodeType.Sequence;
                case "decorator":
                    return NodeType.Decorator;
                default:
                    Debug.LogError("Bad Node Creation");
                    return NodeType.Null;
            }

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

        public AINode CreateNode(string nodeName, Vector2 position, string guid, MonoScript a_function)
        {
            //Removes the namespace from the recieved node name
            if (nodeName.Contains("NodeBasedBehaviourTree"))
            {
                nodeName = nodeName.Remove(0, 23);

            }

            AINode newNode = NodeClass(nodeName);

            newNode.title = nodeName;

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
            textField.SetValueWithoutNotify("Node Name");

            if (nodeName == "LeafNode" || nodeName == "DecoratorNode")
            {
                newNode.mainContainer.Add(AddObjectField(a_function));
            }


            newNode.titleContainer.Add(textField);

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

            if (function != null)
            {
                objectField.value = function;
            }

            return objectField;
        }

    }
}