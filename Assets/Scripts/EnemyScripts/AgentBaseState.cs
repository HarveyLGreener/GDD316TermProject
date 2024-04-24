using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AgentBaseState 
{
    public abstract void EnterState();
    public abstract void Update();
    public abstract void ExitState();
}
