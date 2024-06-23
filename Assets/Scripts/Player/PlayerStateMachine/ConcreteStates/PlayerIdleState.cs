using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player player, StateMachine playerStateMachine) : base(player, playerStateMachine) { }
    private float _moveSpeed;

    public override void EnterState()
    {
        base.EnterState();

        _moveSpeed = player.MoveSpeed;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        float MoveX = Input.GetAxisRaw("Horizontal");
        float MoveY = Input.GetAxisRaw("Vertical");

        Vector2 velocity = (new Vector2(MoveX, MoveY).normalized) * _moveSpeed;

        player.Move(velocity);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
