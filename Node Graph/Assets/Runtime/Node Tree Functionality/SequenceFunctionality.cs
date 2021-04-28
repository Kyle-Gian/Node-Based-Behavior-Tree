using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
#endif

public class SequenceFunctionality : NodeFunctionality
{
    // Start is called before the first frame update
    void Start()
    {
        CreateNewOutputList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
