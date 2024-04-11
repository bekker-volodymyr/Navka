using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class PlayerMoveToPointState : PlayerState
{
    public PlayerMoveToPointState(Player player, StateMachine playerStateMachine) : base(player, playerStateMachine) { }

    private Vector3 _targetPos;
    private Vector3 _direction;

    private float moveSpeed = 5f;

    public override void EnterState()
    {
        base.EnterState();
        _targetPos = GetEndPoint();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        _direction = (_targetPos - player.transform.position).normalized;
        player.Move(_direction * moveSpeed);

        if ((player.transform.position - _targetPos).sqrMagnitude < 0.01f)
        {
            player.StateMachine.ChangeState(player.IdleState);
        }

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            player.StateMachine.ChangeState(player.IdleState);
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

    private Vector3 GetEndPoint()
    {
        Vector3 mousePosition = Input.mousePosition;
        float distanceToPlane = 10f;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, distanceToPlane));

        return worldPosition;
    }
}
