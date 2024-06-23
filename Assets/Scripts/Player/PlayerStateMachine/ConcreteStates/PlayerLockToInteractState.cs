using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerLockToInteractState : PlayerState
{
    public PlayerLockToInteractState(Player player, StateMachine playerStateMachine) : base(player, playerStateMachine) { }

    private Transform _targetTransform;
    private IInteractable _targetInteractable;

    private Vector3 _direction;

    private float _moveSpeed;

    public override void EnterState()
    {
        base.EnterState();

        _moveSpeed = player.MoveSpeed;

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
        if ((player.transform.position - _targetTransform.position).sqrMagnitude < player.InteractRadius)
        {
            player.Interact(_targetInteractable);
        }
        else
        {
            _direction = (_targetTransform.position - player.transform.position).normalized;
            player.Move(_direction * _moveSpeed);
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
        _targetTransform = targetCollider.transform;
        _targetInteractable = targetCollider.GetComponentInParent<IInteractable>();
    }
}
