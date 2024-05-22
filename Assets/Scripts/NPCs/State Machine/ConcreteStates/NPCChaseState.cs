using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCChaseState : NPCState
{
    public NPCChaseState(NPCBase npc, NPCStateMachine npcStateMachine) : base(npc, npcStateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        npc.ChaseStateInstance.DoEnterLogic();
    }

    public override void ExitState()
    {
        base.ExitState();

        npc.ChaseStateInstance.DoExitLogic();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        npc.ChaseStateInstance.DoFrameUpdateLogic();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        npc.ChaseStateInstance.DoPhysicsLogic();
    }

    public override void AnimationTriggerEvent(NPCBase.AnimationTriggerType type)
    {
        base.AnimationTriggerEvent(type);

        npc.ChaseStateInstance.DoAnimationTriggerEventLogic(type);
    }
}
