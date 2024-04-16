using System.Collections;
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

    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void FrameUpdate() {
        //TODO: sensor input abstraction

        if (!GameState.isPaused && !GameState.isInPlayerMenu)
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                player.StateMachine.ChangeState(player.IdleState);
            }

            if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject()) // move with mouse
            {
                player.StateMachine.ChangeState(player.MoveToPointState);
            }

            if (Input.GetKeyDown(KeyCode.F)) // attack
                                             // TODO: left mouse click - avoid conflick with "move"
            {
                player.StateMachine.ChangeState(player.LockToTargetState);
            }

            if (Input.GetKeyDown(KeyCode.R) /*|| Input.GetMouseButtonDown(1)*/) //interact
            {
                Debug.Log("R pressed");
                player.StateMachine.ChangeState(player.LockToInteractState);
            }
        }

    }
    public virtual void PhysicsUpdate() { }
    public virtual void AnimationTriggerEvent(Npc.AnimationTriggerType type) { }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
