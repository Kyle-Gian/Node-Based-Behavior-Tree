using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBehaviorTree : MonoBehaviour
{
    GraphContainer _storedNodes;
    // Start is called before the first frame update
    void Start()
    {
        //LoadTree();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadTree(string fileName)
    {
        _storedNodes = Resources.Load<GraphContainer>(fileName);

        if (_storedNodes == null)
        {
            Debug.LogError("File not found!");
            return;
        }
    }
}
