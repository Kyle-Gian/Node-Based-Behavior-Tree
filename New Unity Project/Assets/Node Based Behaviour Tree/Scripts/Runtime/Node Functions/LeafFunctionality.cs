//Author: Kyle Gian
//Date Created: 30/04/2021
//Last Modified: 30/04/2021
namespace NodeBasedBehaviourTree
{
    using System.Collections;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class LeafFunctionality : NodeFunctionality
    {
        public override void RunFunction(LeafTreeNode node, GameObject AI)
        {
            if (node._script != null)
            {
                //If the script is a node check, it will run this code and return the status of this node
                if (node._script.GetType().BaseType == typeof(NodeCheck))
                {
                    NodeCheck nodeCheck;
                    nodeCheck = (NodeCheck)node._script;

                    node._currentStatus = nodeCheck.CheckCondition(AI);

                }

                //If the script is a behaviour, set the behaviour of the AI with the attached behaviour
                if (node._script.GetType().BaseType == typeof(AIBehaviour))
                {
                    AIBehaviour nodeType;
                    nodeType = (AIBehaviour)node._script;

                    AI.GetComponent<ActiveBehaviour>().SetBehaviour(nodeType.GetBehaviour());


                }
            }
        }
    }
}