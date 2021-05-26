//Author: Kyle Gian
//Date Created: 23/04/2021
//Last Modified: 29/04/2021
namespace NodeBasedBehaviourTree
{
    using System;
    using UnityEngine;
    using UnityEditor.Experimental.GraphView;

    public class RootNode : AINode
    {
        public RootNode GenerateEntryPointNode()
        {
            var node = new RootNode
            {
                title = "Root Node",
                _NodeType = "Start",
                _GUID = Guid.NewGuid().ToString(),
                _entryPoint = true
            };
            var generatedPort = GeneratePort(node, Direction.Output);
            generatedPort.portName = "Next";
            node.outputContainer.Add(generatedPort);

            node.capabilities &= ~Capabilities.Movable;
            node.capabilities &= ~Capabilities.Deletable;

            node.SetPosition(new Rect(x: 0, y: 0, width: 100, height: 150));
            return node;
        }

    }
}