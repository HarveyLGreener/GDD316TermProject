using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AgentBaseState 
{
    public abstract void EnterState(StateController agent);
    public abstract void Update(StateController agent);
    public abstract void ExitState(StateController agent);
}
