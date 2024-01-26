using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour, IDamageable, IMoveable, ITriggerCheckable
{
    public float CurrentHealth { get; set; }
    [field: SerializeField] public float MaxHealth { get; set; }
    public Rigidbody2D Rigidbody { get; set; }
    public bool IsFacingRight { get; set; } = true;
    public bool IsPlayerNoticed { get; set; }
    public bool IsWithinStrikingDistance { get; set; }

    #region State Machine Fields

    public NPCStateMachine StateMachine { get; set; }
    public NpcIdleState IdleState { get; set; }
    public NpcPlayerNoticedState PlayerNoticedState { get; set; }
    public FellowRunawayState FellowRunawayState { get; set; }

    #endregion

    #region Idle Fields

    public float RandomMovementRange = 5f;
    public float RandomMovementSpeed = 1f;

    #endregion

    private void Awake()
    {
        StateMachine = new NPCStateMachine();
        IdleState = new NpcIdleState(this, StateMachine);
        PlayerNoticedState = new NpcPlayerNoticedState(this, StateMachine);
        FellowRunawayState = new FellowRunawayState(this, StateMachine);

    }

    private void Start()
    {
        CurrentHealth = MaxHealth;

        Rigidbody = GetComponent<Rigidbody2D>();

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        StateMachine.CurrentState.FrameUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    #region Health / Death

    public void Death()
    {
        Destroy(gameObject);
    }

    public void GetDamage(float damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth < 0f) Death();
    }

    #endregion

    #region Movement
    public void Move(Vector2 velocity)
    {
        Rigidbody.velocity = velocity;
        CheckFacing(velocity);
    }

    public void CheckFacing(Vector2 velocity)
    {
        if (IsFacingRight && velocity.x < 0f)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            IsFacingRight = !IsFacingRight;
        }
        else if (!IsFacingRight && velocity.x > 0f)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            IsFacingRight = !IsFacingRight;
        }
    }
    #endregion

    #region Animation Triggers
    private void AnimationTriggerEvent(AnimationTriggerType type)
    {
        // TODO
    }

    public enum AnimationTriggerType
    {
        NpcDamaged,
        PlayFootstepsSound
    }

    #endregion

    #region Distance Checks

    public void SetPlayerNoticedStatus(bool isPlayerNoticed)
    {
        IsPlayerNoticed = isPlayerNoticed;
    }

    public void SetStrikingDistanceBool(bool isWithinStrikingDistance)
    {
        IsWithinStrikingDistance = isWithinStrikingDistance;
    }

    #endregion
}
