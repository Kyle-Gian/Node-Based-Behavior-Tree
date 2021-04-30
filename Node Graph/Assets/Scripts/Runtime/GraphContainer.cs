//Author: Kyle Gian
//Date Created: 23/04/2021
//Last Modified: 23/04/2021


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphContainer : ScriptableObject
{
    public List<NodeEdge> NodeLink = new List<NodeEdge>();
    public List<NodeData> NodeData = new List<NodeData>();
}
