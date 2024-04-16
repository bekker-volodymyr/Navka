using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour, IMoveable, IDamageable, IAttack, IInteract
{
    public Rigidbody2D objectRB { get; set; }

    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] public CircleCollider2D AttackRadius;
    [SerializeField] public CircleCollider2D TargetNoticeRadius;

    #region State Machine Fields

    public StateMachine StateMachine { get; set; }
    public PlayerIdleState IdleState { get; set; }
    public PlayerLockToInteractState LockToInteractState { get; set; }
    public PlayerLockToTargetState LockToTargetState { get; set; }
    public PlayerMoveToPointState MoveToPointState { get; set; }
    public PlayerUnderCoverState UnderCoverState { get; set; }

    #endregion

    private void Awake()
    {
        StateMachine = new StateMachine();
        IdleState    = new PlayerIdleState(this, StateMachine);
        MoveToPointState = new PlayerMoveToPointState(this, StateMachine);
        LockToInteractState = new PlayerLockToInteractState(this, StateMachine);
        LockToTargetState = new PlayerLockToTargetState(this, StateMachine);
        UnderCoverState = new PlayerUnderCoverState(this, StateMachine);
    }

    private void Start()
    {
        CurrentHealth = MaxHealth;

        objectRB = GetComponent<Rigidbody2D>();

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

    #region IDamageable Fields

    public float CurrentHealth { get; set; }
    [field: SerializeField] public float MaxHealth { get; set; }

    #endregion

    #region Indicators

    [SerializeField] private Indicator healthIndicator;
    [SerializeField] private Indicator hungerIndicator;
    [SerializeField] private Indicator manaIndicator;

    #endregion

    #region IAttack Fields

    [field: SerializeField] public float Damage { get; set; }
    public float cooldown { get; set ; }
    public float delayBeforeDamage { get ; set; }

    #endregion

    #region IDamageable Methods

    public void Death()
    {
        GameState.isDead = true;
        Time.timeScale = 0f;
    }

    public void GetDamage(float damage)
    {
        // TODO: застосування підсилень та послаблень показника шкоди

        float newHealth = CurrentHealth - damage;

        Debug.Log(newHealth);

        if(newHealth <= 0)
        {
            CurrentHealth = 0;
            healthIndicator.SetValue(CurrentHealth, MaxHealth);
            Death();
        }
        else
        {
            CurrentHealth = newHealth;
            healthIndicator.SetValue(CurrentHealth, MaxHealth);
        }

    }

    #endregion

    #region IAttack Methods

    public void Attack(IDamageable target)
    {
        // TODO: застосування підсилень та послаблень для показника шкоди

        target.GetDamage(Damage);
    }

    public void ApplyDamage(IDamageable target)
    {
        throw new System.NotImplementedException();
    }

    public Collider2D[] GetAllItemsInCollisionRadius()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, TargetNoticeRadius.radius);
        return colliders;
    }

    #endregion

    #region IMoveable Methods

    public void Move(Vector2 velocity)
    {
        objectRB.velocity = velocity;
    }
    public void CheckFacing(Vector2 velocity)
    {
        // TODO: повертання відповідно до напрямку руху
    }

    #endregion

    #region Interactions

    public void Interact(IInteractable target)
    {
        target.OnInteraction();
    }

    #endregion

}
