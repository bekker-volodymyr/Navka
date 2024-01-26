using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCState
{
    protected Npc npc;
    protected NPCStateMachine npcStateMachine;

    public NPCState(Npc npc, NPCStateMachine npcStateMachine)
    {
        this.npc = npc;
        this.npcStateMachine = npcStateMachine;
    }

    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void FrameUpdate() { }
    public virtual void PhysicsUpdate() { }
    public virtual void AnimationTriggerEvent(Npc.AnimationTriggerType type) { }

}
