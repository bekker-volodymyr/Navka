using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAttackSOBase : ScriptableObject
{
    protected Npc npc;
    protected Transform transform;
    protected GameObject gameObject;

    protected GameObject playerGameObject;
    protected Transform playerTransform;
    protected Player playerScript;

    public virtual void Initialize(GameObject gameObject, Npc npc)
    {
        this.gameObject = gameObject;
        transform = gameObject.transform;
        this.npc = npc;

        playerGameObject = GameObject.FindGameObjectWithTag("Player");

        playerTransform = playerGameObject.transform;
        playerScript = playerGameObject.GetComponent<Player>();
    }

    public virtual void DoEnterLogic() { }
    public virtual void DoExitLogic() { ResetValues(); }
    public virtual void DoFrameUpdateLogic()
    {
        if (!npc.IsWithinAttackDistance && !npc.isOnCooldown)
        {
            npc.StateMachine.ChangeState(npc.PlayerNoticedState);
        }
    }
    public virtual void DoPhysicsLogic() { }
    public virtual void DoAnimationTriggerEventLogic(Npc.AnimationTriggerType triggerType) { }
    public virtual void ResetValues() { }
}
