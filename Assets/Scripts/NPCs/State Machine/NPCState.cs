public class NPCState
{
    protected NPCBase npc;
    protected NPCStateMachine npcStateMachine;

    public NPCState(NPCBase npc, NPCStateMachine npcStateMachine)
    {
        this.npc = npc;
        this.npcStateMachine = npcStateMachine;
    }

    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void FrameUpdate() { }
    public virtual void PhysicsUpdate() { }
    public virtual void AnimationTriggerEvent(NPCBase.AnimationTriggerType type) { }
}
