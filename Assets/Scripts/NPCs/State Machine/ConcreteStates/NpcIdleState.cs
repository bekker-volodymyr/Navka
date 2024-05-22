public class NPCIdleState : NPCState
{
    public NPCIdleState(NPCBase npc, NPCStateMachine npcStateMachine) : base(npc, npcStateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        npc.IdleStateInstance.DoEnterLogic();
    }

    public override void ExitState()
    {
        base.ExitState();

        npc.IdleStateInstance.DoExitLogic();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        npc.IdleStateInstance.DoFrameUpdateLogic();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        npc.IdleStateInstance.DoPhysicsLogic();
    }

    public override void AnimationTriggerEvent(NPCBase.AnimationTriggerType type)
    {
        base.AnimationTriggerEvent(type);

        npc.IdleStateInstance.DoAnimationTriggerEventLogic(type);
    }
}
