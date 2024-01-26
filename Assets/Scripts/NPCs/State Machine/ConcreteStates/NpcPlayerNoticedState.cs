using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcPlayerNoticedState : NPCState
{
    public NpcPlayerNoticedState(Npc npc, NPCStateMachine npcStateMachine) : base(npc, npcStateMachine)
    {
    }

    public override void AnimationTriggerEvent(Npc.AnimationTriggerType type)
    {
        base.AnimationTriggerEvent(type);
    }

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
        base.FrameUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
