using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Noticed-Chase", menuName = "NPC Logic/Player Noticed Logic/Chase")]
public class PlayerNoticedChase : PlayerNoticedSOBase
{
    [SerializeField] private float _movementSpeed = 1.75f;
    public override void DoAnimationTriggerEventLogic(Npc.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
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

        Vector2 moveDirection = (playerTransform.position - npc.transform.position).normalized;

        npc.Move(moveDirection * _movementSpeed);
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
}
