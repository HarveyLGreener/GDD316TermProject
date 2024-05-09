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
            if (timerRestart)
            {
                Debug.Log("Added Force");
                timerRestart = false;
                agent.ball.GetComponent<Rigidbody>().isKinematic = false;
                agent.ball.GetComponent<Rigidbody>().AddForce(new Vector3(-0f, 17f, -7f), ForceMode.Impulse);
                shotWaitTimer = 1.5f;
                agent.transform.DetachChildren();

            }
            if (shotWaitTimer <= 0f)
            {
                timerRestart = true;
            }
            //agent.TransitionToState(agent.chaseState);
        }
        else if (agent.gameObject.transform.childCount > 0 && shotWaitTimer <= 0f)
        {
            agent.agent.SetDestination(agent.shootPos.position);
        }
        else if (shotWaitTimer <= 0f)
        {
            agent.TransitionToState(agent.chaseState);
        }

    }
    
}
