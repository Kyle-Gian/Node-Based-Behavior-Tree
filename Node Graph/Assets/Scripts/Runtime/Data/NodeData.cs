//Author: Kyle Gian
//Date Created: 25/04/2021
//Last Modified: 30/04/2021

using UnityEngine;

[System.Serializable]
public class NodeData 
{
    public string NodeGUID;
    public Vector2 Position;
    public string NodeType;
    [HideInInspector]
    public ScriptContainer NodeFunction;
    [HideInInspector]
    public string FunctionName;
}
