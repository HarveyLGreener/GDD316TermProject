using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{
    public AgentBaseState currentState;

    public readonly ChaseState chaseState = new ChaseState();
    public readonly PatrolState patrolState = new PatrolState();

    public NavMeshAgent agent;

    public GameObject ball;
    public GameObject[] waypoints;

    public MeshRenderer meshRenderer;

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
        meshRenderer = GetComponent<MeshRenderer>();
        ball = FindFirstObjectByType<Ball>().gameObject;
    }
    private void Update()
    {
        currentState.Update(this);   
    }
}
