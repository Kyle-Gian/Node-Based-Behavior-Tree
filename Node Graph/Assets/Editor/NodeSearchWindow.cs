//Author: Kyle Gian
//Date Created: 23/04/2021
//Last Modified: 23/04/2021

//This script has the funcionality of the pop up window inside the graph view and gives the user the ability to find a node on right click 
//To add the selected node from the list to the graph

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;

public class NodeSearchWindow : ScriptableObject, ISearchWindowProvider
{
    private GraphLayout _graphView;
    private EditorWindow _window;
    private Texture2D _indentationIcon;

    public void INIT(EditorWindow window, GraphLayout graphView)
    {
        _graphView = graphView;
        _window = window;
        _indentationIcon = new Texture2D(1, 1);
        _indentationIcon.SetPixel(0, 0, new Color(0, 0, 0, 0));
        _indentationIcon.Apply();
    }
    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
    {
        //Used to place items into a search list with different nodes
        var tree = new List<SearchTreeEntry>
        {
            new SearchTreeGroupEntry(new GUIContent("Create Elements"), 0),
            new SearchTreeGroupEntry(new GUIContent("AI Nodes"), 1),
            new SearchTreeEntry(new GUIContent("Selector Node", _indentationIcon))
            {
                userData = new SelectorNode(), level = 2
            },
            new SearchTreeEntry(new GUIContent("Sequence Node", _indentationIcon))
            {
                userData = new SequenceNode(), level = 2
            },          
            new SearchTreeEntry(new GUIContent("Leaf Node", _indentationIcon))
            {
                userData = new LeafNode(), level = 2
            },          
            new SearchTreeEntry(new GUIContent("Decorator Node", _indentationIcon))
            {
                userData = new DecoratorNode(), level = 2
            },

        };
        return tree;
    }

    public bool OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
    {
        var worldMousePosition = _window.rootVisualElement.ChangeCoordinatesTo(_window.rootVisualElement.parent, context.screenMousePosition - _window.position.position);
        var localMousePosition = _graphView.contentViewContainer.WorldToLocal(worldMousePosition);

        switch (searchTreeEntry.userData)
        {
            case AINode AINode:
                _graphView.CreateNode(AINode.GetType().ToString(), localMousePosition);
                return true;
            default:
                return false;

        }
    }
}
