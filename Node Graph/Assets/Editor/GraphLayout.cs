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

    //Add the node to graph
    public void CreateNode(string nodeName, Vector2 position)
    {
        AddElement(CreateNewNode(nodeName, position));
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

        textField.SetValueWithoutNotify(selectorNode.title);
        selectorNode.mainContainer.Add(textField);


        selectorNode.RefreshExpandedState();
        selectorNode.RefreshPorts();
        selectorNode.SetPosition(new Rect(position: position, defaultNodeSize));
        return selectorNode;
    }

}
