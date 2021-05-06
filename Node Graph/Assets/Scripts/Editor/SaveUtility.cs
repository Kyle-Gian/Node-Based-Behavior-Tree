//Author: Kyle Gian
//Date Created: 25/04/2021
//Last Modified: 29/04/2021


//Saves/Loads nodes and edges to scriptable objects to and from the graph view
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
public class SaveUtility
{
    private GraphLayout _targetGraphView;
    private GraphContainer _containerCache;

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
        var container = ScriptableObject.CreateInstance<GraphContainer>();
        if (!SaveNodes(fileName, container)) return;
        //SaveExposedProperties(container);

        //Auto creates folders if they do not exist
        if (!AssetDatabase.IsValidFolder(path: "Assets/Resources"))
        {
            AssetDatabase.CreateFolder(parentFolder: "Assets", newFolderName: "Resources");
        }

        AssetDatabase.CreateAsset(container, path: $"Assets/Resources/{fileName}.asset");
        AssetDatabase.SaveAssets();
    }

    private bool SaveNodes(string fileName, GraphContainer container)
    {
        if (!Edges.Any()) return false;
        var connectedSockets = Edges.Where(x => x.input.node != null).ToArray();

        for (var i = 0; i < connectedSockets.Count(); i++)
        {

            var outputNode = (connectedSockets[i].output.node as AINode);
            var inputNode = (connectedSockets[i].input.node as AINode);
            container.NodeLink.Add(new NodeEdge
            {
                BaseNodeGUID = outputNode._GUID,
                PortName = connectedSockets[i].output.portName,
                TargetNodeGUID = inputNode._GUID
            });
        }

        foreach (var AINode in Nodes.Where(node => !node._entryPoint))
        {
            ScriptContainer functionName = null;
            ObjectField function = (ObjectField)AINode.mainContainer.ElementAt(2);

            if (function.value != null)
            {
                functionName = (ScriptContainer)function.value;
            }
            else
            {
                Debug.Log("No function Attached");
            }
            
            container.NodeData.Add(item: new NodeData
            {
                NodeGUID = AINode._GUID,
                Position = AINode.GetPosition().position,
                NodeType = AINode._NodeType,                  
                NodeFunction = functionName

            }) ;
        }
        return true;
    }

    public void LoadGraph(string fileName)
    {
        _containerCache = Resources.Load<GraphContainer>(fileName);

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
            var k = i; //Prevent access to modified closure
            var connections = _containerCache.NodeLink.Where(x => x.BaseNodeGUID == Nodes[i]._GUID).ToList();

            for (var j = 0; j < connections.Count(); j++)
            {
                var targetNodeGUID = connections[j].TargetNodeGUID;
                var targetNode = Nodes.First(x => x._GUID == targetNodeGUID);
                LinkedNodes(Nodes[i].outputContainer[0].Q<Port>(), (Port)targetNode.inputContainer[0]);

                targetNode.SetPosition(new Rect(
                    _containerCache.NodeData.First(x => x.NodeGUID == targetNodeGUID).Position,
                    _targetGraphView.defaultNodeSize));
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
        foreach (var nodeData in _containerCache.NodeData)
        {
            //Pass position on later, so vec2 used as position for now
            var tempNode = _targetGraphView.LoadNode(nodeData.NodeType, nodeData.Position, nodeData.NodeGUID, nodeData.NodeFunction);

            var nodePorts = _containerCache.NodeLink.Where(x => x.BaseNodeGUID == nodeData.NodeGUID).ToList();
            tempNode._GUID = nodeData.NodeGUID;

            _targetGraphView.AddElement(tempNode);
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
