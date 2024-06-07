using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBefriendedState : NPCState
{
    protected BefriendableNPC befriendableNpc;
    public NPCBefriendedState(BefriendableNPC npc, NPCStateMachine npcStateMachine) : base(npc, npcStateMachine)
    {
        befriendableNpc = npc;
    }

    public override void EnterState()
    {
        base.EnterState();

        befriendableNpc.CurrentBefriendedStateInstance.DoEnterLogic();
    }

    public override void ExitState()
    {
        base.ExitState();

        befriendableNpc.CurrentBefriendedStateInstance.DoExitLogic();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        befriendableNpc.CurrentBefriendedStateInstance.DoFrameUpdateLogic();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        befriendableNpc.CurrentBefriendedStateInstance.DoPhysicsLogic();
    }

    public override void AnimationTriggerEvent(NPCBase.AnimationTriggerType type)
    {
        base.AnimationTriggerEvent(type);

        befriendableNpc.CurrentBefriendedStateInstance.DoAnimationTriggerEventLogic(type);
    }
}
