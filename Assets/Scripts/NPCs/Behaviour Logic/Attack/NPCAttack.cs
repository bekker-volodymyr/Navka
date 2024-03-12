using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack-NPC Attack", menuName = "NPC Logic/Attack Logic/NPC Attack")]
public class NPCAttack : NPCAttackSOBase
{
    private float startTime;

    public override void DoAnimationTriggerEventLogic(Npc.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();

        startTime = Time.time;
        npc.isOnCooldown = true;
        Debug.Log("Attack started");
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        if(Time.time - startTime > npc.cooldown) 
        {
            Debug.Log("Attack ended");
            if (npc.IsWithinAttackDistance)
            {
                Debug.Log("New Attack Started");
                startTime = Time.time;
                npc.isOnCooldown = true;
            }
            else
            {
                npc.isOnCooldown = false;
            }
        }
        else if(Time.time - startTime > npc.delayBeforeDamage && npc.IsWithinAttackDistance)
        {
            Debug.Log("Damage applying");
            npc.Attack(playerScript);
        }

        // npc.Attack(playerScript);
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
    }

    public override void Initialize(GameObject gameObject, Npc npc)
    {
        base.Initialize(gameObject, npc);
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }

    private void DelayedAttack(IDamageable target)
    {

    }
}
