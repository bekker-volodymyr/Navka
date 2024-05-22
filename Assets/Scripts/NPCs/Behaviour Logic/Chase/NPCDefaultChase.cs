using UnityEngine;

[CreateAssetMenu(fileName = "Chase-Default Chase", menuName = "NPC Logic/Chase Logic/Default Chase")]
public class NPCDefaultChase : NPCChaseSOBase
{
    private Vector3 _direction;
    private IDamageable attackTarget;
    private float moveSpeed = 5f;

    public override void Initialize(NPCBase npc)
    {
        base.Initialize(npc);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();

        attackTarget = chaseTarget.GetComponent<IDamageable>();

    }
    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }
    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        
        if (chaseTarget == null)
        {
            npc.StateMachine.ChangeState(npc.IdleState);
        }

        if ((npc.transform.position - chaseTarget.transform.position).sqrMagnitude < npc.AttackRadius.radius)
        {
            npc.Attack(attackTarget);
        }
        else
        {
            _direction = (chaseTarget.transform.position - npc.transform.position).normalized;
            npc.Move(_direction * moveSpeed);
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
}
