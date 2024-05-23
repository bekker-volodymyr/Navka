using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerLockToTargetState : PlayerState
{
    public PlayerLockToTargetState(Player player, StateMachine playerStateMachine) : base(player, playerStateMachine) { }

    private Vector3 _targetPos;
    private Vector3 _direction;
    private IDamageable _targetEnemy;

    private float moveSpeed = 5f;


    public override void EnterState()
    {
        base.EnterState();

        if (!TryFindClosestTarget())
        {
            player.StateMachine.ChangeState(player.IdleState);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();


        if ((player.transform.position - _targetPos).sqrMagnitude < player.AttackRadius.radius)
        {
            player.Attack(_targetEnemy);
            player.StateMachine.ChangeState(player.IdleState);
        }
        else
        {
            _direction = (_targetPos - player.transform.position).normalized;
            player.Move(_direction * moveSpeed);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


    private bool TryFindClosestTarget()
    {
        // Get all colliders within the specified circle
        Collider2D[] colliders = player.GetAllItemsInCollisionRadius();

        float minDistance = 0f;
        Collider2D closestCollider = null;

        // Find closest
        foreach (var collider in colliders)
        {
            IDamageable target = collider.GetComponent<IDamageable>();

            //if (collider.CompareTag("IDamageable"))
            if(target != null && !target.Equals(player)) 
            {
                float distance = Vector2.Distance(player.transform.position, collider.transform.position);
                if (distance < minDistance || closestCollider == null)
                {
                    minDistance = distance;
                    closestCollider = collider;
                }
            }
        }

        if (closestCollider == null)
        {
            return false;
        }
        else
        {
            SetTarget(closestCollider);
            return true;
        }
    }

    private void SetTarget(Collider2D targetCollider)
    {
        _targetPos = targetCollider.transform.position;
        _targetEnemy = targetCollider.GetComponent<IDamageable>();
    }
}
