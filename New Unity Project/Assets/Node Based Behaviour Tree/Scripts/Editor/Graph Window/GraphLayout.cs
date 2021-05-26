//Author: Kyle Gian
//Date Created: 23/04/2021
//Last Modified: 29/04/2021

//The functionality and tools of the graph enabling the ability to add nodes and manipulators which gives basic graph funcionality
namespace NodeBasedBehaviourTree
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;
    using UnityEditor.Experimental.GraphView;
    using UnityEngine.UIElements;
    using System.Linq;

    public class GraphLayout : GraphView
    {
        public readonly Vector2 defaultNodeSize = new Vector2(x: 150, y: 200);
        public List<ExposedProperty> exposedProperties = new List<ExposedProperty>();

        public Blackboard blackboard;
        private NodeSearchWindow _searchWindow;
        AINode node = new AINode();
        RootNode rootNode = new RootNode();
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
                    //Check if the output port is a leaf node
                    //If it is a leaf on allows for the decorator node to be attached
                    if (startPort.node.ToString().Contains("LeafNode"))
                    {               
                        if (port.node.ToString().Contains("DecoratorNode"))
                        {
                            compatiblePorts.Add(port);

                        }
                    }
                    else
                    {
                        compatiblePorts.Add(port);
                    }

                }


            });
            return compatiblePorts;

        }

        //Add the node to graph
        public void CreateNode(string nodeName, Vector2 position, string a_title)
        {
            MonoScript scriptContainer = null;
            string guid = null;
            var newNode = node.CreateNode(nodeName, position, guid, scriptContainer, a_title);
            AddElement(newNode);

        }

        public AINode LoadNode(string nodeName, Vector2 position, string a_guid, MonoScript a_function, string a_title)
        {
            var newNode = node.CreateNode(nodeName, position, a_guid, a_function, a_title);
            AddElement(newNode);

            return newNode;
        }
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
}