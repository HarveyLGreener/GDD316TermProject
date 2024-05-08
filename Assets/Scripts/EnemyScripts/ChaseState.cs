using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : AgentBaseState
{
    public override void EnterState(StateController agent)
    {
        agent.meshRenderer.material.color = Color.red;
    }

    public override void ExitState(StateController agent)
    {
        
    }

    public override void Update(StateController agent)
    {
        if (agent.ball != null)
        {

            agent.agent.SetDestination(agent.ball.transform.position);
        }
    }
}
