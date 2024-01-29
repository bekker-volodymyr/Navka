using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCIdleSOBase : ScriptableObject
{
    protected Npc npc;
    protected Transform transform;
    protected GameObject gameObject;

    protected Transform playerTransform;

    public virtual void Initialize(GameObject gameObject, Npc npc)
    {
        this.gameObject = gameObject;
        transform = gameObject.transform;
        this.npc = npc;

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public virtual void DoEnterLogic() { }
    public virtual void DoExitLogic() { ResetValues(); }
    public virtual void DoFrameUpdateLogic() 
    {
        if (npc.IsPlayerNoticed)
        {
            npc.StateMachine.ChangeState(npc.PlayerNoticedState);
        }
    }
    public virtual void DoPhysicsLogic() { }
    public virtual void DoAnimationTriggerEventLogic(Npc.AnimationTriggerType triggerType) { }
    public virtual void ResetValues() { }
}
