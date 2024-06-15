using UnityEngine;

public class PlayerLockToTargetState : PlayerState
{
    public PlayerLockToTargetState(Player player, StateMachine playerStateMachine) : base(player, playerStateMachine) { }

    private Vector3 _targetPos;
    private Vector3 _direction;
    private IDamageable _targetEnemy;
    private GameObject _targetEnemyGO;

    private float moveSpeed = 5f;

    public override void EnterState()
    {
        base.EnterState();

        if (!TryFindClosestTarget())
        {
            player.StateMachine.ChangeState(player.IdleState);
        }
        else
        {
            player.SetTargetForAnimals(_targetEnemyGO);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if ((player.transform.position - _targetPos).sqrMagnitude < player.InteractRadius)
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
            if (collider.CompareTag("Damageable"))
            {
                // Checking if this is an befriended NPC
                BefriendableNPC npc = collider.GetComponentInParent<BefriendableNPC>();

                if (npc != null)
                {
                    if (player.Animals.Animals.Contains(npc))
                    {
                        continue;
                    }
                }

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
        _targetEnemyGO = targetCollider.gameObject.transform.parent.gameObject;
        player.SetTargetForAnimals(_targetEnemyGO);
        _targetEnemy = targetCollider.GetComponentInParent<IDamageable>();
    }
}
