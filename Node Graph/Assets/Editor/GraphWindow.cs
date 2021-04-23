//Author: Kyle Gian
//Date Created: 23/04/2021
//Last Modified: 23/04/2021

// Creates the window and handles the toolbar, minimap and blackboard

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System;
using System.Linq;

public class GraphWindow : EditorWindow
{
    
    private GraphLayout _graphView;
    private string _fileName = "New Narrative";

    [MenuItem("Graph/AI Node Window")]
    public static void OpenGraphWindow()
    {
        var Window = GetWindow<GraphWindow>();
        Window.titleContent = new GUIContent(text: "Node Graph");
    }

    private void ConstructGraphView()
    {
        _graphView = new GraphLayout(this)
        {
            name = "Behavior Tree Graph"
        };
        _graphView.StretchToParentSize();
        rootVisualElement.Add(_graphView);
    }
    private void OnEnable()
    {
        ConstructGraphView();
        GenerateToolbar();
        GenerateMiniMap();
        //GenerateBlackBoard();
    }

    //private void GenerateBlackBoard()
    //{
    //    var blackBoard = new Blackboard(_graphView);
    //    blackBoard.Add(new BlackboardSection { title = "Exposed Properties" });
    //    blackBoard.editTextRequested = (blackBoard1, element, newValue) =>
    //    {
    //        var oldPropertyName = ((BlackboardField)element).text;
    //        ((BlackboardField)element).text = newValue;
    //    };
    //    blackBoard.SetPosition(new Rect(10, 30, 200, 300));
    //    _graphView.Add(blackBoard);
    //    //_graphView.blackboard = blackBoard;
    //}

    // create the minimap at the top of the graph
    private void GenerateMiniMap()
    {
        var miniMap = new MiniMap { anchored = true };
        //This will give 10 px offset to leftside
        var cords = _graphView.contentViewContainer.WorldToLocal(new Vector2(position.width - 210, 30));

        miniMap.SetPosition(new Rect(cords.x, cords.y, 200, 140));
        _graphView.Add(miniMap);
    }

    //Creates the buttons and text field for the graph window
    private void GenerateToolbar()
    {
        var toolbar = new Toolbar();

        var fileNameTextField = new TextField(label: "File Name");
        fileNameTextField.SetValueWithoutNotify(_fileName);
        fileNameTextField.MarkDirtyRepaint();
        fileNameTextField.RegisterCallback((EventCallback<ChangeEvent<string>>)(evt => _fileName = evt.newValue));
        toolbar.Add(fileNameTextField);

        //toolbar.Add(child: new Button(clickEvent: () => RequestDataOperation(save: true)) { text = "Save Data" });
        //toolbar.Add(child: new Button(clickEvent: () => RequestDataOperation(save: false)) { text = "Load Data" });


        rootVisualElement.Add(toolbar);
    }

    //Controls the save and load function by saving when true, loading when false and an error message when no file found
    private void RequestDataOperation(bool save)
    {
        if (string.IsNullOrEmpty(_fileName))
        {
            EditorUtility.DisplayDialog(title: " Invalid file name!", message: " Please enter a valid file name", ok: "OK");
            return;
        }

        //var saveUtility = GraphSaveUtility.GetInstance(_graphView);

        //if (save)
        //{
        //    saveUtility.SaveGraph(_fileName);
        //}
        //else
        //{
        //    saveUtility.LoadGraph(_fileName);
        //}
    }

    private void OnDisable()
    {
        rootVisualElement.Remove(_graphView);
    }
}
