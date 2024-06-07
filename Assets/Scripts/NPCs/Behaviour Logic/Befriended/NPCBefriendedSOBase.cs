using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBefriendedSOBase : ScriptableObject
{
    protected NPCBase npc;

    protected Player player;

    public virtual void Initialize(NPCBase npc)
    {
        this.npc = npc;
    }

    public virtual void DoEnterLogic() 
    {
        //player = npc.BefriendedPlayer;
    }
    public virtual void DoExitLogic() { ResetValues(); }
    public virtual void DoFrameUpdateLogic() { }
    public virtual void DoPhysicsLogic() { }
    public virtual void DoAnimationTriggerEventLogic(NPCBase.AnimationTriggerType triggerType) { }
    public virtual void ResetValues() { }
}
