using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerLockToInteractState : PlayerState
{
    public PlayerLockToInteractState(Player player, StateMachine playerStateMachine) : base(player, playerStateMachine) { }

    private Vector3 _targetPos;
    private Vector3 _direction;
    private IInteractable _targetItem;

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
        if(_targetItem == null)
        {
            player.StateMachine.ChangeState(player.IdleState);
        }

        if ((player.transform.position - _targetPos).sqrMagnitude < player.InteractRadius)
        {
            player.Interact(_targetItem);
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
            if (collider.CompareTag("Interactable"))
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
        _targetItem = targetCollider.GetComponentInParent<IInteractable>();
    }

    public void RemoveTarget()
    {
        _targetItem = null;
    }
}
