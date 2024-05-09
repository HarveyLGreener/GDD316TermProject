using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallState : AgentBaseState
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
        if (Mathf.Abs((agent.gameObject.transform.position - agent.shootPos.position).magnitude) < 5f)
        {
            Debug.Log("taking the shot");
        }
        else if (agent.gameObject.transform.GetChildCount() > 0)
        {
            agent.agent.SetDestination(agent.shootPos.position);
        }
        else
        {
            agent.TransitionToState(agent.chaseState);
        }

    }
}
