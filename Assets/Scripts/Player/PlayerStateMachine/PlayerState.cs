using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected StateMachine playerStateMachine;

    public PlayerState(Player player, StateMachine playerStateMachine)
    {
        this.player = player;
        this.playerStateMachine = playerStateMachine;
    }

    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void FrameUpdate() {
        //TODO: sensor input abstraction

        if (Input.GetMouseButtonDown(0)) // move with mouse
        {
            player.StateMachine.ChangeState(player.MoveToPointState);
        }

        if (Input.GetKeyDown(KeyCode.F)) // attack
        // TODO: left mouse click - avoid conflick with "move"
        {
            player.StateMachine.ChangeState(player.LockToTargetState);
        }

        if (Input.GetKeyDown(KeyCode.R) || Input.GetMouseButtonDown(1)) //interact
        {
            player.StateMachine.ChangeState(player.LockToTargetState);
        }

    }
    public virtual void PhysicsUpdate() { }
    public virtual void AnimationTriggerEvent(Npc.AnimationTriggerType type) { }
}
