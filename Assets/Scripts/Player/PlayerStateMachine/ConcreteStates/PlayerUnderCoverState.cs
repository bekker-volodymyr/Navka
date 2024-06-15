using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerUnderCoverState : PlayerState
{
    public PlayerUnderCoverState(Player player, StateMachine playerStateMachine) : base(player, playerStateMachine) { }

    public override void EnterState()
    {
        base.EnterState();

        player.SpriteRenderer.gameObject.SetActive(false);

        player.DamageCollider.enabled = false;
    }

    public override void ExitState()
    {
        base.ExitState();

        player.SpriteRenderer.gameObject.SetActive(true);

        player.DamageCollider.enabled = true;

        player.LeaveCover();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if(Input.anyKeyDown)
        {
            player.StateMachine.ChangeState(player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
