//Author: Kyle Gian
//Date Created: 25/04/2021
//Last Modified: 30/04/2021

using UnityEngine;
using UnityEditor;

[System.Serializable]
public class NodeData
{
    public string NodeGUID;
    public Vector2 Position;
    public string NodeType;
    public MonoScript NodeFunction;
    public string FunctionName;
}
