using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentAdder : MonoBehaviour
{
    void Awake()
    {
        gameObject.AddComponent<NavMeshAgent>();
    }
}
