using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCChaseSOBase : ScriptableObject
{
    protected NPCBase npc;

    protected GameObject target;
    protected NPCBase targetNPC;

    private float loseTargetDistance = 26f;

    public virtual void Initialize(NPCBase npc)
    {
        this.npc = npc;
    }

    public virtual void DoEnterLogic() 
    {
        target = npc.ChaseTarget;

        targetNPC = target.GetComponent<NPCBase>(); //.NPCDeathEvent += npc.SetDefaultState;

        if(targetNPC != null )
        {
            targetNPC.NPCDeathEvent += npc.SetDefaultState;
        }

        npc.HealthIndicator.gameObject.SetActive(true);
        npc.InteractHintGO.SetActive(false);
        npc.InteractCollider.gameObject.SetActive(false);
    }
    public virtual void DoExitLogic() { ResetValues(); }
    public virtual void DoFrameUpdateLogic()
    {
        if (target == null)
        {
            npc.SetDefaultState();
        }

        if (Vector3.Distance(target.transform.position, npc.transform.position) > loseTargetDistance)
        {
            target = null;
        }
    }
    public virtual void DoPhysicsLogic() { }
    public virtual void DoAnimationTriggerEventLogic(NPCBase.AnimationTriggerType triggerType) { }
    public virtual void ResetValues() 
    {
        npc.HealthIndicator.gameObject.SetActive(false);
        npc.InteractHintGO.SetActive(true);
        npc.InteractCollider.gameObject.SetActive(true);

        if(targetNPC != null)
        {
            targetNPC.NPCDeathEvent += npc.SetDefaultState;
        }
    }
}
