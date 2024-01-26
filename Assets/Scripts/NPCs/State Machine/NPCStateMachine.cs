using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStateMachine
{
    public NPCState CurrentState { get; set; }
    public void Initialize(NPCState startingState)
    {
        CurrentState = startingState;
        CurrentState.EnterState();
    }
    public void ChangeState(NPCState newState)
    {
        CurrentState.ExitState();
        CurrentState = newState;
        CurrentState.EnterState();
    }
}
