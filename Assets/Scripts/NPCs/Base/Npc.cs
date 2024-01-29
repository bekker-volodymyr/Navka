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
    public bool IsWithinAttackDistance { get; set; }

    #region State Machine Fields

    public NPCStateMachine StateMachine { get; set; }
    public NPCIdleState IdleState { get; set; }
    public NPCPlayerNoticedState PlayerNoticedState { get; set; }

    #endregion

    #region Scriptable Object Fields

    [SerializeField] private NPCIdleSOBase idleBase;
    [SerializeField] private PlayerNoticedSOBase playerNoticedBase;

    public NPCIdleSOBase IdleBaseInstance { get; set; }
    public PlayerNoticedSOBase PlayerNoticedBaseInstance { get; set; }

    #endregion

    private void Awake()
    {
        IdleBaseInstance = Instantiate(idleBase);
        PlayerNoticedBaseInstance = Instantiate(playerNoticedBase);

        StateMachine = new NPCStateMachine();
        IdleState = new NPCIdleState(this, StateMachine);
        PlayerNoticedState = new NPCPlayerNoticedState(this, StateMachine);
    }

    private void Start()
    {
        CurrentHealth = MaxHealth;

        Rigidbody = GetComponent<Rigidbody2D>();

        IdleBaseInstance.Initialize(gameObject, this);
        PlayerNoticedBaseInstance.Initialize(gameObject, this);

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

    public void SetAttackDistanceBool(bool isWithinAttackDistance)
    {
        IsWithinAttackDistance = isWithinAttackDistance;
    }

    #endregion
}
