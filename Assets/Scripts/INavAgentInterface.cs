using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YZ.RootMotionAgent
{
    public interface INavAgentInterface
    {
        Vector3 GetSteeringTarget();
        Vector3 GetDestination();
        void SetDestination(Vector3 position);

        bool CompletedDestination();
    }

}
