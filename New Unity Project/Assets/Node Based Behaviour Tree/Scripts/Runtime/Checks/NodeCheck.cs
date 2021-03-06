//Author: Kyle Gian
//Date Created: 23/04/2021
//Last Modified: 23/04/2021

namespace NodeBasedBehaviourTree
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;

    [System.Serializable]
    public class NodeCheck : MonoBehaviour
    {
        public virtual TreeNode.Status CheckCondition(GameObject AI)
        {
            return TreeNode.Status.PROCESSING;
        }
    }
}
