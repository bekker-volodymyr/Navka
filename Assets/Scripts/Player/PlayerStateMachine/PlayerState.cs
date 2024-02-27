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

        if (Input.GetMouseButtonDown(0))
        //TODO: sensor input abstraction
        {
            player.StateMachine.ChangeState(player.MoveToPointState);
        }

    }
    public virtual void PhysicsUpdate() { }
    public virtual void AnimationTriggerEvent(Npc.AnimationTriggerType type) { }
}
