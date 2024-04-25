using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : AgentBaseState
{
    private int currentPoint;
    public override void EnterState(StateController agent)
    {
        agent.meshRenderer.material.color = Color.green;
    }

    public override void ExitState(StateController agent)
    {
        
    }

    public override void Update(StateController agent)
    {
        if ((agent.transform.position.x == agent.waypoints[currentPoint].transform.position.x && agent.transform.position.z == agent.waypoints[currentPoint].transform.position.z))
        {
            currentPoint = (currentPoint + 1) % 2;
            agent.agent.SetDestination(agent.waypoints[currentPoint].transform.position);
        }
        if (Mathf.Abs(agent.ball.transform.position.x - agent.transform.position.x) <= 5 &&
            Mathf.Abs(agent.ball.transform.position.z - agent.transform.position.z) <= 5)
        {
            agent.TransitionToState(agent.chaseState);
        }
    }
}
