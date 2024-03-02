using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerLockToTargetState : PlayerState
{
    public PlayerLockToTargetState(Player player, StateMachine playerStateMachine) : base(player, playerStateMachine) { }

    //private float cooldown;
    //private float delayBeforeDamage;

    private Vector3 _targetPos;
    private Vector3 _direction;

    private float moveSpeed = 5f;



    public override void EnterState()
    {
       
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        Debug.Log("enemy search engaged");

        // Get all colliders within the specified circle
        //IMPORTANT NOTE: looking for colliders with tag enemy, tag is set on AttackRadius on enemy,
        //because it cant find enemy as a hole unit
        Collider2D[] colliders = player.GetAllItemsInCollisionRadius();
  
        foreach (Collider2D collider in colliders)
        {
            if( Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                Debug.Log("enemy scan disengaged or abandoned");
                player.StateMachine.ChangeState(player.IdleState);
            }

            if (collider.gameObject.tag != "Enemy")
            {
                //player.StateMachine.ChangeState(player.IdleState);
                continue;
            }

            if(collider.CompareTag("Enemy"))
            {
                Debug.Log("Enemy detected");

                _targetPos = collider.transform.position;

                _direction = (_targetPos - player.transform.position).normalized;
                player.Move(_direction * moveSpeed);

                if ((player.transform.position - _targetPos).sqrMagnitude < player.AttackRadius.radius) // NOTE: player attack radius
                {
                    //player.Attack(collider.gameObject);
                    player.StateMachine.ChangeState(player.IdleState);
                }

                if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                {
                    player.StateMachine.ChangeState(player.IdleState);
                }
            }
        }

       
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void AnimationTriggerEvent(Npc.AnimationTriggerType type)
    {
        base.AnimationTriggerEvent(type);
    }


}
