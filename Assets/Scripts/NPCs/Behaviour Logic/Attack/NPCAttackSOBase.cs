using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAttackSOBase : ScriptableObject
{
    protected Npc npc;
    protected Transform transform;
    protected GameObject gameObject;

    protected Transform playerTransform;
    protected PlayerScript playerScript;

    public virtual void Initialize(GameObject gameObject, Npc npc)
    {
        this.gameObject = gameObject;
        transform = gameObject.transform;
        this.npc = npc;

        GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player");

        playerTransform = playerGameObject.transform;
        playerScript = playerGameObject.GetComponent<PlayerScript>();
    }

    public virtual void DoEnterLogic() { }
    public virtual void DoExitLogic() { ResetValues(); }
    public virtual void DoFrameUpdateLogic()
    {
        npc.Attack(playerScript);
    }
    public virtual void DoPhysicsLogic() { }
    public virtual void DoAnimationTriggerEventLogic(Npc.AnimationTriggerType triggerType) { }
    public virtual void ResetValues() { }
}
