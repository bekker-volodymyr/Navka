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

        player.SpriteGO.SetActive(false);

        player.DamageCollider.enabled = false;
        //player.InteractCollider.enabled = false;
    }

    public override void ExitState()
    {
        base.ExitState();

        Debug.Log("Leave cover");

        player.SpriteGO.SetActive(true);

        player.DamageCollider.enabled = true;
        //player.InteractCollider.enabled = true;

        player.LeaveCover();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if(!Input.GetKeyDown(KeyCode.None))
        {
            player.StateMachine.ChangeState(player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
