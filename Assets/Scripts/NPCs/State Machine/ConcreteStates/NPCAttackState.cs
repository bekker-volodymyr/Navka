using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAttackState : NPCState
{
    public NPCAttackState(NPCBase npc, NPCStateMachine npcStateMachine) : base(npc, npcStateMachine)
    {
    }

    public override void AnimationTriggerEvent(NPCBase.AnimationTriggerType type)
    {
        base.AnimationTriggerEvent(type);

        //npc.AttackBaseInstance.DoAnimationTriggerEventLogic(type);
    }

    public override void EnterState()
    {
        base.EnterState();

       // npc.AttackBaseInstance.DoEnterLogic();
    }

    public override void ExitState()
    {
        base.ExitState();

        //npc.AttackBaseInstance.DoExitLogic();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

       // npc.AttackBaseInstance.DoFrameUpdateLogic();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

       // npc.AttackBaseInstance.DoPhysicsLogic();
    }
}
