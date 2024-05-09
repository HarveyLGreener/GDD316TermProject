using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{
    public AgentBaseState currentState;

    public readonly ChaseState chaseState = new ChaseState();

    public readonly BallState ballState = new BallState();

    public NavMeshAgent agent;

    public GameObject ball;


    public MeshRenderer meshRenderer;
    
    public Transform shootPos;

    private void Start()
    {
        TransitionToState(chaseState);
    }
    public void TransitionToState(AgentBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        shootPos = FindFirstObjectByType<ShootPos>().transform;
        agent.speed = 20f;
        meshRenderer = GetComponent<MeshRenderer>();
        if (FindObjectOfType<Ball>() != null)
        {
            ball = FindObjectOfType<Ball>().gameObject;
        }

        //shootPos = FindObjectOfType<ShootPos>().gameObject.transform;
    }
    private void Update()
    {
        if (FindObjectOfType<Ball>() != null && ball == null)
        {
            ball = FindObjectOfType<Ball>().gameObject;
        }
        currentState.Update(this);   
    }
}
