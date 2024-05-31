using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogState : NPCState
{
    public NPCDialogState(NPCBase npc, NPCStateMachine npcStateMachine) : base(npc, npcStateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        npc.DialogStateInstance.DoEnterLogic();
    }

    public override void ExitState()
    {
        base.ExitState();

        npc.DialogStateInstance.DoExitLogic();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        npc.DialogStateInstance.DoFrameUpdateLogic();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        npc.DialogStateInstance.DoPhysicsLogic();
    }

    public override void AnimationTriggerEvent(NPCBase.AnimationTriggerType type)
    {
        base.AnimationTriggerEvent(type);

        npc.DialogStateInstance.DoAnimationTriggerEventLogic(type);
    }
}
