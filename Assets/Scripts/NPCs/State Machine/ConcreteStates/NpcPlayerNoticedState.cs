using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPlayerNoticedState : NPCState
{
    public NPCPlayerNoticedState(NPCBase npc, NPCStateMachine npcStateMachine) : base(npc, npcStateMachine)
    {
    }

    public override void AnimationTriggerEvent(NPCBase.AnimationTriggerType type)
    {
        base.AnimationTriggerEvent(type);

        //npc.PlayerNoticedBaseInstance.DoAnimationTriggerEventLogic(type);
    }

    public override void EnterState()
    {
        base.EnterState();

        //npc.PlayerNoticedBaseInstance.DoEnterLogic();
    }

    public override void ExitState()
    {
        base.ExitState();

        //npc.PlayerNoticedBaseInstance.DoExitLogic();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        //npc.PlayerNoticedBaseInstance.DoFrameUpdateLogic();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        //npc.PlayerNoticedBaseInstance.DoPhysicsLogic();
    }
}
