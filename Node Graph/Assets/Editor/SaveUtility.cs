﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
public class SaveUtility
{
    private GraphLayout _targetGraphView;
    private DialogueContainer _containerCache;

    private List<Edge> Edges => _targetGraphView.edges.ToList();
    private List<AINode> Nodes => _targetGraphView.nodes.ToList().Cast<AINode>().ToList();

    public static SaveUtility GetInstance(GraphLayout targetGraphView)
    {
        return new SaveUtility
        {
            _targetGraphView = targetGraphView
        };
    }

    public void SaveGraph(string fileName)
    {
        var container = ScriptableObject.CreateInstance<DialogueContainer>();
        if (!SaveNodes(container)) return;
        SaveExposedProperties(container);

        //Auto creates folders if they do not exist
        if (!AssetDatabase.IsValidFolder(path: "Assets/Resources"))
        {
            AssetDatabase.CreateFolder(parentFolder: "Assets", newFolderName: "Resources");
        }

        AssetDatabase.CreateAsset(container, path: $"Assets/Resources/{fileName}.asset");
        AssetDatabase.SaveAssets();
    }

    private bool SaveNodes(DialogueContainer container)
    {
        if (!Edges.Any()) return false;

        var dialogueContainer = ScriptableObject.CreateInstance<DialogueContainer>();
        var connectedSockets = Edges.Where(x => x.input.node != null).ToArray();

        for (var i = 0; i < connectedSockets.Count(); i++)
        {
            var outputNode = (connectedSockets[i].output.node as DialogueNode);
            var inputNode = (connectedSockets[i].input.node as DialogueNode);
            dialogueContainer.NodeLink.Add(new NodeLinkData
            {
                BaseNodeGUID = outputNode.GUID,
                PortName = connectedSockets[i].output.portName,
                TargetNodeGUID = inputNode.GUID
            });
        }

        foreach (var AINode in Nodes.Where(node => !node._entryPoint))
        {
            dialogueContainer.DialogueNodeData.Add(item: new DialogueNodeData
            {
                NodeGUID =AINode.GUID,
                DialogueText = AINode.DialogueText,
                Position = AINode.GetPosition().position

            });
        }
        return true;


    }

    public void LoadGraph(string fileName)
    {
        _containerCache = Resources.Load<DialogueContainer>(fileName);

        if (_containerCache == null)
        {
            EditorUtility.DisplayDialog("File not found!", "Target dialogue graph file does not exist!", "OK");
            return;
        }

        ClearGraph();
        CreateNodes();
        ConnectNodes();
    }

    private void ConnectNodes()
    {
        for (var i = 0; i < Nodes.Count; i++)
        {
            var connections = _containerCache.NodeLink.Where(x => x.BaseNodeGUID == Nodes[i].GUID).ToList();

            for (var j = 0; j < connections.Count; j++)
            {
                var targetNodeGuid = connections[j].TargetNodeGUID;
                var targetNode = Nodes.First(x => x.GUID == targetNodeGuid);
                LinkedNodes(Nodes[i].outputContainer[j].Q<Port>(), (Port)targetNode.inputContainer[0]);

                targetNode.SetPosition(new Rect(_containerCache.DialogueNodeData.First(x => x.NodeGUID == targetNodeGuid).Position,
                    _targetGraphView.defaultNodeSize
                    ));

            }
        }

    }

    private void LinkedNodes(Port output, Port input)
    {
        var tempEdge = new Edge
        {
            output = output,
            input = input
        };

        tempEdge?.input.Connect(tempEdge);
        tempEdge?.output.Connect(tempEdge);
        _targetGraphView.Add(tempEdge);
    }

    private void CreateNodes()
    {
        foreach (var nodeData in _containerCache.DialogueNodeData)
        {
            //Pass position on later, so vec2 used as position for now
            var tempNode = _targetGraphView.CreateNode(nodeData.DialogueText, Vector2.zero);
            tempNode._GUID = nodeData.NodeGUID;
            _targetGraphView.AddElement(tempNode);

            var nodePorts = _containerCache.NodeLink.Where(x => x.BaseNodeGUID == nodeData.NodeGUID).ToList();
            nodePorts.ForEach(x => _targetGraphView.AddChoicePort(tempNode, x.PortName));
        }

    }

    private void ClearGraph()
    {
        //Sets entry points guid back from the save. Discard existing guid.
        Nodes.Find(x => x._entryPoint)._GUID = _containerCache.NodeLink[0].BaseNodeGUID;

        foreach (var node in Nodes)
        {
            if (node._entryPoint) continue;
            //Remove edges that connect to this node
            Edges.Where(x => x.input.node == node).ToList().ForEach(edge => _targetGraphView.RemoveElement(edge));
            //Then remove the node
            _targetGraphView.RemoveElement(node);
        }
    }
}
