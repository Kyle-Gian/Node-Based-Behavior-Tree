using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;

[CreateAssetMenu(fileName = "Node Script", menuName = "New Node Script")]
public class ScriptContainer : ScriptableObject
{
    [System.Serializable]
    public class NodeTest
    {
        public NodeCheck[] NodeCheck;
    }

    public NodeTest nodeCheck;

}
