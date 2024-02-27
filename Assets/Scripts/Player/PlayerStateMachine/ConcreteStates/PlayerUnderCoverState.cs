using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnderCoverState : PlayerState
{
    public PlayerUnderCoverState(Player player, StateMachine playerStateMachine) : base(player, playerStateMachine) { }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void AnimationTriggerEvent(Npc.AnimationTriggerType type)
    {
        base.AnimationTriggerEvent(type);
    }
}
