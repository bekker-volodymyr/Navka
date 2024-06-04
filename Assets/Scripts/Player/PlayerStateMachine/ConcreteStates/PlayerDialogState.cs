using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogState : PlayerState
{
    public PlayerDialogState(Player player, StateMachine playerStateMachine) : base(player, playerStateMachine) { }
    public override void EnterState()
    {
        base.EnterState();

        Debug.Log("Enter Dialog State");
    }

    public override void ExitState()
    {
        base.ExitState();
        Debug.Log("ExitDialogState");
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        Debug.Log("Dialog State Frame Update");
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
