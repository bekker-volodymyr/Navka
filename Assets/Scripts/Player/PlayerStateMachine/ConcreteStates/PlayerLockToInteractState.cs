using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLockToInteractState : PlayerState
{
    public PlayerLockToInteractState(Player player, StateMachine playerStateMachine) : base(player, playerStateMachine) { }

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
        Debug.Log("Interactable search engaged");

        // Get all colliders within the specified circle
        //IMPORTANT NOTE: looking for colliders with tag enemy, tag is set on AttackRadius on enemy,
        //because it cant find enemy as a hole unit
        Collider2D[] colliders = player.GetAllItemsInCollisionRadius();

        foreach (Collider2D collider in colliders)
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                Debug.Log("Interactable scan disengaged or abandoned");
                player.StateMachine.ChangeState(player.IdleState);
            }

            if (collider.CompareTag("Item"))
            {
                Debug.Log("Item detected");

                _targetPos = collider.transform.position;

                if ((player.transform.position - _targetPos).sqrMagnitude < player.AttackRadius.radius) // NOTE: player attack radius
                {
                    player.Interact(collider.GetComponentInParent<Item>());
                }
                else
                {
                    _direction = (_targetPos - player.transform.position).normalized;
                    player.Move(_direction * moveSpeed);
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
