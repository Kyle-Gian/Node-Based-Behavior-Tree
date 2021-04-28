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
    public List<ExposedProperty> exposedProperties = new List<ExposedProperty>();

    public Blackboard blackboard;
    private NodeSearchWindow _searchWindow;
    LeafNode leafNode = new LeafNode();
    RootNode rootNode = new RootNode();
    SelectorNode selectorNode = new SelectorNode();
    SequenceNode sequenceNode = new SequenceNode();
    DecoratorNode decoratorNode = new DecoratorNode();
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

        AddElement(rootNode.GenerateEntryPointNode());
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

    //Add the node to graph
    public void CreateNode(string nodeName, Vector2 position)
    {
        if (nodeName == "SelectorNode")
        {
            AddElement(selectorNode.CreateSelectorNode(nodeName, position));

        }
        if (nodeName == "SequenceNode")
        {
            AddElement(sequenceNode.CreateSequenceNode(nodeName, position));

        }
        if (nodeName == "DecoratorNode")
        {
            AddElement(decoratorNode.CreateDecoratorNode(nodeName, position));

        }
        if (nodeName == "LeafNode")
        {

            AddElement(leafNode.CreateLeafNode(nodeName, position));

        }
    }

    public void LoadNode(string nodeName, Vector2 position, string a_guid)
    {
        if (nodeName == "SelectorNode")
        {
            AddElement(selectorNode.CreateSelectorNode(nodeName, position));

        }
        if (nodeName == "SequenceNode")
        {
            AddElement(sequenceNode.CreateSequenceNode(nodeName, position));

        }
        if (nodeName == "DecoratorNode")
        {
            AddElement(decoratorNode.CreateDecoratorNode(nodeName, position));

        }
        if (nodeName == "LeafNode")
        {

            AddElement(leafNode.CreateLeafNode(nodeName, position));

        }
    }

    //public void AddNewPort(AINode AINode, string overriddenPortName = "")
    //{
    //    var generatedPort = GeneratePort(AINode, Direction.Output);

    //    var oldLabel = generatedPort.contentContainer.Q<Label>("type");
    //    generatedPort.contentContainer.Remove(oldLabel);

    //    var outputPortCount = AINode.outputContainer.Query(name: "connector").ToList().Count;

    //    //Increase port count on node by 1 unless name is overwritten manually
    //    var choicePortName = string.IsNullOrEmpty(overriddenPortName) ? $"Choice {outputPortCount + 1}" : overriddenPortName;

    //    var textField = new TextField
    //    {
    //        name = string.Empty,
    //        value = choicePortName
    //    };
    //    textField.RegisterValueChangedCallback(evt => generatedPort.portName = evt.newValue);
    //    generatedPort.contentContainer.Add(new Label("  "));
    //    generatedPort.contentContainer.Add(textField);

    //    generatedPort.portName = choicePortName;
    //    AINode.outputContainer.Add(generatedPort);
    //    AINode.RefreshPorts();
    //    AINode.RefreshExpandedState();
    //}

    public void ClearBlackBoardAndExposedProperties()
    {
        exposedProperties.Clear();
        blackboard.Clear();
    }
    public void AddPropertyToBlackBoard(ExposedProperty exposedProperty)
    {
        var localPropertyName = exposedProperty.PropertyName;
        var localPropertyValue = exposedProperty.PropertyValue;
        while (exposedProperties.Any(X => X.PropertyValue == localPropertyName))
            localPropertyName = $"{localPropertyName}(1)";
        var property = new ExposedProperty();
        property.PropertyName = localPropertyName;
        property.PropertyValue = localPropertyValue;

        exposedProperties.Add(property);

        var container = new VisualElement();
        var blackBoardField = new BlackboardField { text = property.PropertyName, typeText = "string" };
        container.Add(blackBoardField);

        var propertyValueTextField = new TextField("Value")
        {
            value = localPropertyValue
        };
        propertyValueTextField.RegisterValueChangedCallback(evt =>
        {
            var changingPropertyIndex = exposedProperties.FindIndex(x => x.PropertyName == property.PropertyName);
            exposedProperties[changingPropertyIndex].PropertyValue = evt.newValue;
        });
        var blackBoardValueRow = new BlackboardRow(blackBoardField, propertyValueTextField);
        container.Add(blackBoardValueRow);


        blackboard.Add(container);
    }


}
