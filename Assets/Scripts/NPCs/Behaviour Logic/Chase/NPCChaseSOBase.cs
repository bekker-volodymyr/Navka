using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCChaseSOBase : ScriptableObject
{
    protected NPCBase npc;

    protected GameObject chaseTarget;

    public virtual void Initialize(NPCBase npc)
    {
        this.npc = npc;
    }

    public virtual void DoEnterLogic() 
    {
        chaseTarget = npc.ChaseTarget;
    }
    public virtual void DoExitLogic() { ResetValues(); }
    public virtual void DoFrameUpdateLogic()
    {

    }
    public virtual void DoPhysicsLogic() { }
    public virtual void DoAnimationTriggerEventLogic(NPCBase.AnimationTriggerType triggerType) { }
    public virtual void ResetValues() { }
}
