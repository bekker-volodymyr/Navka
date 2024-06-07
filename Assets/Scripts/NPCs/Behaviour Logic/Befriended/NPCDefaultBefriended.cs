using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(fileName ="Befriended-DefaultBefriended", menuName ="NPC Logic/Befriended Logic/Default Befriended")]
public class NPCDefaultBefriended : NPCBefriendedSOBase
{
    public float followSpeed = 2.0f;
    public float stoppingDistance = 1.0f;

    public override void Initialize(BefriendableNPC npc)
    {
        base.Initialize(npc);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        Vector2 direction = player.transform.position - npc.transform.position;
        float distance = direction.magnitude;

        if (distance > stoppingDistance)
        {
            direction.Normalize();
            Vector2 velocity = direction * followSpeed;
            npc.Move(velocity);
        }
        else
        {
            npc.Move(Vector2.zero);
        }
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }

    public override void DoAnimationTriggerEventLogic(NPCBase.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }
}
