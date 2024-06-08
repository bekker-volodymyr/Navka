using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Befriended-DefendPoint", menuName = "NPC Logic/Befriended Logic/Defend Point Befriended")]
public class NPCDefendPointBefriended : NPCBefriendedSOBase
{
    Transform defendPoint;

    private float wanderRadius = 5f; // The radius of the wandering area
    private float wanderTimer = 2f;
    private float timer;

    public override void Initialize(BefriendableNPC npc)
    {
        base.Initialize(npc);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();

        defendPoint = player.HideoutGO.transform;

        if (Vector3.Distance(player.transform.position, defendPoint.position) < wanderRadius)
        {
            npc.transform.position = defendPoint.position + Vector3.right;
        }

        timer = wanderTimer;
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomCircle(player.HideoutGO.transform.position, wanderRadius);
            Vector3 velocity = (newPos - npc.transform.position).normalized;
            npc.Move(velocity);
            timer = 0;
        }
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
    }
    public override void DoAnimationTriggerEventLogic(NPCBase.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }

    private Vector3 RandomCircle(Vector3 center, float radius)
    {
        float angle = Random.Range(0f, 2f * Mathf.PI);
        float x = center.x + radius * Mathf.Cos(angle);
        float y = center.y + radius * Mathf.Sin(angle);
        return new Vector3(x, y);
    }
}
