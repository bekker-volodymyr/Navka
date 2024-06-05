using UnityEngine;

[CreateAssetMenu(fileName = "Chase-Default Chase", menuName = "NPC Logic/Chase Logic/Default Chase")]
public class NPCDefaultChase : NPCChaseSOBase
{
    private Vector3 _direction;
    private IDamageable attackTarget;
    private float moveSpeed = 5f;

    private bool attackStarted;
    private float attackDelay;
    private float attackTimer;

    public override void Initialize(NPCBase npc)
    {
        base.Initialize(npc);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();

        attackTarget = target.GetComponent<IDamageable>();

        attackStarted = false;
        attackDelay = 1f;
        attackTimer = attackDelay;

    }
    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }
    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        
        if (!attackStarted)
        {
            if ((npc.transform.position - target.transform.position).sqrMagnitude < npc.AttackRadius.radius)
            {
                attackStarted = true;
                attackTimer = attackDelay;
                npc.Move(Vector2.zero);
            }
            else
            {
                _direction = (target.transform.position - npc.transform.position).normalized;
                npc.Move(_direction * moveSpeed);
            }
        }
        else
        {
            attackTimer -= Time.deltaTime;
            if(attackTimer <= 0)
            {
                if ((npc.transform.position - target.transform.position).sqrMagnitude < npc.AttackRadius.radius)
                    npc.Attack(attackTarget);
                attackStarted = false;
            }
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
