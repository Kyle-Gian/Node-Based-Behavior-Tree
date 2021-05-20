
//Author: Kyle Gian
//Date Created: 25/04/2021
//Last Modified: 30/04/2021

namespace NodeBasedBehaviourTree
{
    using UnityEngine;
    using UnityEditor;

    [System.Serializable]
    public class NodeData
    {
        public string NodeGUID;
        public Vector2 Position;
        public string NodeType;
#if UNITY_EDITOR
        public MonoScript NodeFunction;
#endif
        public string FunctionName;
    }
}
