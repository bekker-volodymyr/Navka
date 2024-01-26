using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FellowRunawayState : NPCState
{
    private Transform _playerTransform;
    private float _movementSpeed = 1.75f;
    public FellowRunawayState(Npc npc, NPCStateMachine npcStateMachine) : base(npc, npcStateMachine)
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void AnimationTriggerEvent(Npc.AnimationTriggerType type)
    {
        base.AnimationTriggerEvent(type);
    }

    public override void EnterState()
    {
        base.EnterState();

        Debug.Log("fellow runaway state");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        Vector2 moveDirection = - (_playerTransform.position - npc.transform.position).normalized;

        npc.Move(moveDirection * _movementSpeed);

        if(!npc.IsPlayerNoticed)
        {
            npc.StateMachine.ChangeState(npc.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
