//Author: Kyle Gian
//Date Created: 11/05/2021
//Last Modified: 11/05/2021
namespace NodeBasedBehaviourTree
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System;
    using System.Linq;

    public class ActiveBehaviour : MonoBehaviour
    {
        [SerializeField]
        AIBehaviour _defaultBehaviour;

        AIBehaviour _runningBehaviour;

        AIBehaviour[] _behaviours;
        // Start is called before the first frame update
        void Start()
        {
            _runningBehaviour = _defaultBehaviour;
            _behaviours = this.GetComponents<AIBehaviour>();
        }

        // Update is called once per frame
        void Update()
        {
            RunActiveBehaviour(_runningBehaviour);

        }

        public void SetBehaviour(AIBehaviour newBehaviour)
        {
            foreach (var behaviour in _behaviours)
            {
                if (newBehaviour.GetType() == behaviour.GetType())
                {
                    _runningBehaviour = behaviour;
                }

            }
        }

        public string GetBehaviour()
        {
            return null;
        }

        private void RunActiveBehaviour(AIBehaviour activeBehaviour)
        {
            foreach (var behaviour in _behaviours)
            {
                if (behaviour != activeBehaviour)
                {
                    behaviour.enabled = false;
                }
            }
            if (!activeBehaviour.enabled)
            {
                activeBehaviour.enabled = true;

            }
        }
    }
}
