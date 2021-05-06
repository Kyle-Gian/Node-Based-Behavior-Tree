using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Node Script", menuName = "New Node Script")]
public class ScriptContainer : ScriptableObject
{
    [SerializeField]
    MonoScript nodeScript;
    
}
