using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
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
