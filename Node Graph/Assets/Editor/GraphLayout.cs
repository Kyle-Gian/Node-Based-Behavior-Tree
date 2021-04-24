//Author: Kyle Gian
//Date Created: 23/04/2021
//Last Modified: 23/04/2021

//The funcionality and tools of the graph enabling the ability to add nodes and manipulators which gives basic graph funcionality

using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Linq;

public class GraphLayout : GraphView
{
    public readonly Vector2 defaultNodeSize = new Vector2(x: 150, y: 200);
    //public Blackboard blackboard;
    private NodeSearchWindow _searchWindow;

    public GraphLayout(EditorWindow editorWindow)
    {
        //Adds functionality to the Graph 
        styleSheets.Add(styleSheet: Resources.Load<StyleSheet>(path: "NodeGraph"));
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        //Add Grid Style to the window
        var grid = new GridBackground();
        Insert(index: 0, grid);
        grid.StretchToParentSize();

        AddElement(GenerateEntryPointNode());
        AddSearchWindow(editorWindow);
    }

    private void AddSearchWindow(EditorWindow editorWindow)
    {
        _searchWindow = ScriptableObject.CreateInstance<NodeSearchWindow>();
        _searchWindow.INIT(editorWindow, this);
        nodeCreationRequest = context => SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), _searchWindow);

    }

    //Checks that output can only go to an input node, checking each node in the list
    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        var compatiblePorts = new List<Port>();
        ports.ForEach(funcCall: (port) =>
        {
            if (startPort != port && startPort.node != port.node)
            {
                compatiblePorts.Add(port);
            }
        });
        return compatiblePorts;

    }

    private Port GeneratePort(AINode node, Direction portDirection)
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

    //Add the node to graph
    public void CreateNode(string nodeName, Vector2 position)
    {

        if (nodeName == "SelectorNode")
        {
            AddElement(CreateSelectorNode(nodeName, position));

        }

        if (nodeName == "SequenceNode")
        {
            AddElement(CreateSequenceNode(nodeName, position));

        }

        if (nodeName == "DecoratorNode")
        {
            AddElement(CreateDecoratorNode(nodeName, position));

        }

        if (nodeName == "LeafNode")
        {
            AddElement(CreateLeafNode(nodeName, position));

        }
    }

    //Creat the Dialogue node with input/output ports, textfields and the name of the node
    public AINode CreateNewNode(string nodeName, Vector2 position)
    {
        var selectorNode = new SelectorNode
        {
            title = nodeName,
            _funcionOfNodeName = nodeName,
            _GUID = Guid.NewGuid().ToString()
        };

        var inputPort = GeneratePort(selectorNode, Direction.Input);
        inputPort.portName = "Input";
        selectorNode.inputContainer.Add(inputPort);
        
        // Add stylesheet to the node colors
        selectorNode.styleSheets.Add(Resources.Load<StyleSheet>("Node"));

        var textField = new TextField(string.Empty);

        textField.RegisterValueChangedCallback(evt =>
        {
            selectorNode._funcionOfNodeName = evt.newValue;
            selectorNode.title = evt.newValue;
        });

        ObjectField objectField = new ObjectField();

        textField.SetValueWithoutNotify(selectorNode.title);
        selectorNode.mainContainer.Add(textField);
        selectorNode.mainContainer.Add(objectField);


        selectorNode.RefreshExpandedState();
        selectorNode.RefreshPorts();
        selectorNode.SetPosition(new Rect(position: position, defaultNodeSize));
        return selectorNode;
    }

    public void AddNewPort(AINode AINode, string overriddenPortName = "")
    {
        var generatedPort = GeneratePort(AINode, Direction.Output);

        var oldLabel = generatedPort.contentContainer.Q<Label>("type");
        generatedPort.contentContainer.Remove(oldLabel);

        var outputPortCount = AINode.outputContainer.Query(name: "connector").ToList().Count;

        //Increase port count on node by 1 unless name is overwritten manually
        var choicePortName = string.IsNullOrEmpty(overriddenPortName) ? $"Choice {outputPortCount + 1}" : overriddenPortName;

        var textField = new TextField
        {
            name = string.Empty,
            value = choicePortName
        };
        textField.RegisterValueChangedCallback(evt => generatedPort.portName = evt.newValue);
        generatedPort.contentContainer.Add(new Label("  "));
        generatedPort.contentContainer.Add(textField);

        generatedPort.portName = choicePortName;
        AINode.outputContainer.Add(generatedPort);
        AINode.RefreshPorts();
        AINode.RefreshExpandedState();
    }

    #region Create Nodes

    //Creates the start node for the graph
    private RootNode GenerateEntryPointNode()
    {
        var node = new RootNode
        {
            title = "Root Node",
            _funcionOfNodeName = "Start",
            _GUID = Guid.NewGuid().ToString(),
            _entryPoint = true
        };
        var generatedPort = GeneratePort(node, Direction.Output);
        generatedPort.portName = "Next";
        node.outputContainer.Add(generatedPort);

        node.capabilities &= ~Capabilities.Movable;
        node.capabilities &= ~Capabilities.Deletable;

        node.SetPosition(new Rect(x: 100, y: 200, width: 100, height: 150));
        return node;
    }

    public SelectorNode CreateSelectorNode(string nodeName, Vector2 position)
    {
        SelectorNode newSelectorNode = new SelectorNode();

        var inputPort = GeneratePort(newSelectorNode, Direction.Input);
        inputPort.portName = "Input";
        newSelectorNode.inputContainer.Add(inputPort);

        // Add stylesheet to the node colors
        newSelectorNode.styleSheets.Add(Resources.Load<StyleSheet>("Node"));

        var textField = new TextField(string.Empty);

        textField.RegisterValueChangedCallback(evt =>
        {
            newSelectorNode._funcionOfNodeName = evt.newValue;
            newSelectorNode.title = evt.newValue;
        });

        textField.SetValueWithoutNotify(newSelectorNode.title);
        newSelectorNode.mainContainer.Add(textField);


        newSelectorNode.RefreshExpandedState();
        newSelectorNode.RefreshPorts();
        newSelectorNode.SetPosition(new Rect(position, defaultNodeSize));

        return newSelectorNode;
    }

    public SequenceNode CreateSequenceNode(string nodeName, Vector2 position)
    {
        SequenceNode newSequenceNode = new SequenceNode();

        var inputPort = GeneratePort(newSequenceNode, Direction.Input);
        inputPort.portName = "Input";
        newSequenceNode.inputContainer.Add(inputPort);

        // Add stylesheet to the node colors
        newSequenceNode.styleSheets.Add(Resources.Load<StyleSheet>("Node"));

        var textField = new TextField(string.Empty);

        textField.RegisterValueChangedCallback(evt =>
        {
            newSequenceNode._funcionOfNodeName = evt.newValue;
            newSequenceNode.title = evt.newValue;
        });

        textField.SetValueWithoutNotify(newSequenceNode.title);
        newSequenceNode.mainContainer.Add(textField);


        newSequenceNode.RefreshExpandedState();
        newSequenceNode.RefreshPorts();
        newSequenceNode.SetPosition(new Rect(position, defaultNodeSize));

        return newSequenceNode;
    }

    public LeafNode CreateLeafNode(string nodeName, Vector2 position)
    {
        LeafNode newLeafNode = new LeafNode();

        var inputPort = GeneratePort(newLeafNode, Direction.Input);
        inputPort.portName = "Input";
        newLeafNode.inputContainer.Add(inputPort);

        // Add stylesheet to the node colors
        newLeafNode.styleSheets.Add(Resources.Load<StyleSheet>("Node"));

        var textField = new TextField(string.Empty);

        textField.RegisterValueChangedCallback(evt =>
        {
            newLeafNode._funcionOfNodeName = evt.newValue;
            newLeafNode.title = evt.newValue;
        });

        ObjectField objectField = new ObjectField();


        textField.SetValueWithoutNotify(newLeafNode.title);
        newLeafNode.mainContainer.Add(textField);
        newLeafNode.mainContainer.Add(objectField);


        newLeafNode.RefreshExpandedState();
        newLeafNode.RefreshPorts();
        newLeafNode.SetPosition(new Rect(position, defaultNodeSize));

        return newLeafNode;
    }

    public DecoratorNode CreateDecoratorNode(string nodeName, Vector2 position)
    {
        DecoratorNode newDecoratorNode = new DecoratorNode();

        var inputPort = GeneratePort(newDecoratorNode, Direction.Input);
        inputPort.portName = "Input";
        newDecoratorNode.inputContainer.Add(inputPort);

        // Add stylesheet to the node colors
        newDecoratorNode.styleSheets.Add(Resources.Load<StyleSheet>("Node"));

        var textField = new TextField(string.Empty);

        textField.RegisterValueChangedCallback(evt =>
        {
            newDecoratorNode._funcionOfNodeName = evt.newValue;
            newDecoratorNode.title = evt.newValue;
        });

        textField.SetValueWithoutNotify(newDecoratorNode.title);
        newDecoratorNode.mainContainer.Add(textField);


        newDecoratorNode.RefreshExpandedState();
        newDecoratorNode.RefreshPorts();
        newDecoratorNode.SetPosition(new Rect(position, defaultNodeSize));

        return newDecoratorNode;
    }
    #endregion


}
