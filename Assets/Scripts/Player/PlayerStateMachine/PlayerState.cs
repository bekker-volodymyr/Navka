using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerState
{
    protected Player player;
    protected StateMachine playerStateMachine;

    public PlayerState(Player player, StateMachine playerStateMachine)
    {
        this.player = player;
        this.playerStateMachine = playerStateMachine;
    }

    public virtual void EnterState() 
    {
        Debug.Log($"Player enters {playerStateMachine.CurrentState}");
    }
    public virtual void ExitState() 
    {
        // Stop all previous moves for player
        player.Move(Vector2.zero);
    }
    public virtual void FrameUpdate()
    {
        // If game paused - do nothing
        if (GameManager.isPaused)
        {
            return;
        }

        // If pressed Q - drop item
        if (Input.GetKeyDown(KeyCode.Q))
        {
            player.DropItem();
        }

        // If any input - interrupt current actions and change to idle state
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            if (player.StateMachine.CurrentState != player.IdleState)
            {
                player.StateMachine.ChangeState(player.IdleState);
                return;
            }
        }

        // If pressed at point on the ground - move to that point
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject())
        {
            player.StateMachine.ChangeState(player.MoveToPointState);
            return;
        }

        // If pressed F - start attack state
        if (Input.GetKeyDown(KeyCode.F) && player.StateMachine.CurrentState != player.LockToTargetState)
        {
            player.StateMachine.ChangeState(player.LockToTargetState);
            return;
        }

        // If pressed E and selected item - use selected item
        if (Input.GetKeyDown(KeyCode.R) && player.SelectedItem != null)
        {
            player.UseSelectedItem();
            return;
        }

        // If pressed E - start interact state
        if (Input.GetKeyDown(KeyCode.E) && player.StateMachine.CurrentState != player.LockToInteractState)
        {
            player.StateMachine.ChangeState(player.LockToInteractState);
            return;
        }
    }
    public virtual void PhysicsUpdate() { }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
