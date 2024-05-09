using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallState : AgentBaseState
{
    private float shotWaitTimer = 0f;
    private bool timerRestart = true;
    public override void EnterState(StateController agent)
    {
        agent.meshRenderer.material.color = Color.red;
    }

    public override void ExitState(StateController agent)
    {

    }
    public override void Update(StateController agent)
    {
        if (shotWaitTimer > 0f)
        {
            shotWaitTimer -= Time.deltaTime;
        }
        if (Mathf.Abs((agent.gameObject.transform.position - agent.shootPos.position).magnitude) < 5f && agent.gameObject.transform.childCount > 0)
        {
            Debug.Log("Added Force");
            if (timerRestart)
            {
                timerRestart = false;
                agent.ball.GetComponent<Rigidbody>().isKinematic = false;
                agent.ball.GetComponent<Rigidbody>().AddForce(new Vector3(-0.5f, 15f, -10f), ForceMode.Impulse);
                shotWaitTimer = 3f;

            }
            if (shotWaitTimer <= 0f)
            {
                agent.transform.DetachChildren();
                timerRestart = true;
            }
            //agent.TransitionToState(agent.chaseState);
        }
        else if (agent.gameObject.transform.childCount > 0)
        {
            agent.agent.SetDestination(agent.shootPos.position);
        }
        else
        {
            agent.TransitionToState(agent.chaseState);
        }

    }
    
}
