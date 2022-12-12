using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Written by Yuewei Zhang, 12/12/2022
namespace YZ.RootMotionAgent
{
    /// <summary>
    /// This class is meant to be no more than an abstraction for the type of navigation agent used and provide support to the Root Motion
    /// The whole point is that we should be able to swap in and out different nav mesh agent, e.g we may wanna use A* instead
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public class NavAgentInterface : MonoBehaviour, INavAgentInterface
    {
        private NavMeshAgent agent;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }
        public Vector3 GetSteeringTarget()
        {
            return agent.steeringTarget;
        }

        public Vector3 GetDestination()
        {
            return agent.destination;
        }

        public void SetDestination(Vector3 position)
        {
            agent.destination = position;
        }

        public bool CompletedDestination()
        {
            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        // Done
                        return true;
                    }
                }
            }

            return false;
        }
    }
}