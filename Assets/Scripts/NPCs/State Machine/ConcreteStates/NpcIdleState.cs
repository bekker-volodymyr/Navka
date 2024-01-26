using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcIdleState : NPCState
{
    private Vector3 _targetPos;
    private Vector3 _direction;
    public NpcIdleState(Npc npc, NPCStateMachine npcStateMachine) : base(npc, npcStateMachine)
    {
    }

    public override void AnimationTriggerEvent(Npc.AnimationTriggerType type)
    {
        base.AnimationTriggerEvent(type);
    }

    public override void EnterState()
    {
        base.EnterState();

        _targetPos = GetRandomPointInCircle();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if(npc.IsPlayerNoticed)
        {
            npc.StateMachine.ChangeState(npc.FellowRunawayState);
        }

        _direction = (_targetPos - npc.transform.position).normalized;
        npc.Move(_direction * npc.RandomMovementSpeed);

        if((npc.transform.position - _targetPos).sqrMagnitude < 0.01f)
        {
            _targetPos = GetRandomPointInCircle();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private Vector3 GetRandomPointInCircle()
    {
        return npc.transform.position + (Vector3)UnityEngine.Random.insideUnitCircle * npc.RandomMovementRange;
    }
}
