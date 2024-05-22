using UnityEngine;

[CreateAssetMenu(fileName = "Idle-Random Wander", menuName = "NPC Logic/Idle Logic/Random Wander")]
public class NPCIdleRandomWander : NPCIdleSOBase
{
    [SerializeField] private float RandomMovementRange = 5f;
    [SerializeField] private float RandomMovementSpeed = 1f;

    private Vector3 _targetPos;
    private Vector3 _direction;

    public override void Initialize(NPCBase npc)
    {
        base.Initialize(npc);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();

        _targetPos = GetRandomPointInCircle();
    }
    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }
    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        _direction = (_targetPos - npc.transform.position).normalized;
        npc.Move(_direction * RandomMovementSpeed);

        if ((npc.transform.position - _targetPos).sqrMagnitude < 0.01f)
        {
            _targetPos = GetRandomPointInCircle();
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

    private Vector3 GetRandomPointInCircle()
    {
        return npc.transform.position + (Vector3)Random.insideUnitCircle * RandomMovementRange;
    }
}
