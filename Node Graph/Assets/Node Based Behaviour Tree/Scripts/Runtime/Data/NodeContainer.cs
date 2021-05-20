//Author: Kyle Gian
//Date Created: 23/04/2021
//Last Modified: 23/04/2021

namespace NodeBasedBehaviourTree
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    public class NodeContainer : ScriptableObject
    {
        public List<NodeEdge> NodeLink = new List<NodeEdge>();
        public List<NodeData> DialogueNodeData = new List<NodeData>();
        public List<ExposedProperty> exposedProperties = new List<ExposedProperty>();
    }
}