using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
#endif
public class NodeFunctionality : MonoBehaviour
{
    List<Edge> connectedOutputEdges;


    public void CreateNewOutputList()
    {
        connectedOutputEdges = new List<Edge>();
    }

    public Edge GetOutputEdge(int a_location)
    {
        return connectedOutputEdges[a_location];
    }
    public List<Edge> GetOutputEdge()
    {
        return connectedOutputEdges;
    }

    public void SetOutputList(List<Edge> a_nodeOutputEdges)
    {
       connectedOutputEdges = a_nodeOutputEdges;
    }

}
