using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCIdleSOBase : ScriptableObject
{
    protected NPCBase npc;

    public virtual void Initialize(NPCBase npc)
    {
        this.npc = npc;
    }

    public virtual void DoEnterLogic() { }
    public virtual void DoExitLogic() { ResetValues(); }
    public virtual void DoFrameUpdateLogic() 
    {
        //if (npc.targets.Count > 0)
        //{
        //    foreach(var target in npc.targets)
        //    {
        //        switch (npc.DecideTarget(target))
        //        {
        //            default:
        //                break;
        //            case Enums.TargetDecisions.Chase:
        //                npc.SetChaseTarget(target.gameObject);
        //                npc.StateMachine.ChangeState(npc.ChaseState);
        //                break;
        //        }
        //    }
        //
        //    // npc.StateMachine.ChangeState(npc.PlayerNoticedState);
        //}
    }
    public virtual void DoPhysicsLogic() { }
    public virtual void DoAnimationTriggerEventLogic(NPCBase.AnimationTriggerType triggerType) { }
    public virtual void ResetValues() { }
}
