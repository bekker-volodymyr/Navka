using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCChaseSOBase : ScriptableObject
{
    protected NPCBase npc;

    protected GameObject target;

    private float loseTargetDistance = 46f;

    public virtual void Initialize(NPCBase npc)
    {
        this.npc = npc;
    }

    public virtual void DoEnterLogic() 
    {
        target = npc.ChaseTarget;

        npc.HealthIndicator.gameObject.SetActive(true);
        npc.InteractHintGO.SetActive(false);
        npc.InteractCollider.gameObject.SetActive(false);
    }
    public virtual void DoExitLogic() { ResetValues(); }
    public virtual void DoFrameUpdateLogic()
    {
        if (Vector3.Distance(target.transform.position, npc.transform.position) > loseTargetDistance)
        {
            target = null;
        }

        if (target == null)
        {
            npc.StateMachine.ChangeState(npc.IdleState);
        }
    }
    public virtual void DoPhysicsLogic() { }
    public virtual void DoAnimationTriggerEventLogic(NPCBase.AnimationTriggerType triggerType) { }
    public virtual void ResetValues() 
    {
        npc.HealthIndicator.gameObject.SetActive(false);
        npc.InteractHintGO.SetActive(true);
        npc.InteractCollider.gameObject.SetActive(true);
    }
}
