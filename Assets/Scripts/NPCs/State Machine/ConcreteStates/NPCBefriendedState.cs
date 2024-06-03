using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBefriendedState : NPCState
{
    public NPCBefriendedState(NPCBase npc, NPCStateMachine npcStateMachine) : base(npc, npcStateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        npc.BefriendedStateInstance.DoEnterLogic();
    }

    public override void ExitState()
    {
        base.ExitState();

        npc.BefriendedStateInstance.DoExitLogic();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        npc.BefriendedStateInstance.DoFrameUpdateLogic();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        npc.BefriendedStateInstance.DoPhysicsLogic();
    }

    public override void AnimationTriggerEvent(NPCBase.AnimationTriggerType type)
    {
        base.AnimationTriggerEvent(type);

        npc.BefriendedStateInstance.DoAnimationTriggerEventLogic(type);
    }
}
