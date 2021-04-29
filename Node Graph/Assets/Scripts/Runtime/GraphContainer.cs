using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphContainer : ScriptableObject
{
    public List<NodeEdge> NodeLink = new List<NodeEdge>();
    public List<NodeData> NodeData = new List<NodeData>();
}
