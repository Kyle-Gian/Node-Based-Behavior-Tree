//Author: Kyle Gian
//Date Created: 25/04/2021
//Last Modified: 25/04/2021

using UnityEngine;

[System.Serializable]
public class NodeEdge
{
    public string BaseNodeGUID;
    public string BaseNodeName;
    public string PortName;
    public string TargetNodeGUID;
    public string TargetNodeName;
    [HideInInspector]
    public TreeNode Parent;
    [HideInInspector]
    public TreeNode Child;
}
