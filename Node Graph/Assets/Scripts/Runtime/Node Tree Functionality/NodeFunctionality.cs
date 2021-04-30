//Author: Kyle Gian
//Date Created: 30/04/2021
//Last Modified: 30/04/2021

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
    public TreeNode.Status ReturnStatus()
    {
        return TreeNode.Status.PROCESSING;
    }

    public virtual void RunFunction()
    {

    }
        

}
