using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IMoveable, IDamageable, IAttack, IInteract
{
    public Rigidbody2D ObjectRB { get; set; }

    [SerializeField] private float moveSpeed = 5f;

    [Space]
    [SerializeField] public CircleCollider2D AttackRadius;
    [SerializeField] public CircleCollider2D TargetNoticeRadius;

    #region Inventory

    [SerializeField] private InventoryController inventory;
    private ItemSO selectedItem = null;

    #endregion

    #region IDamageable Fields

    public float CurrentHealth { get; set; }
    [field: SerializeField] public float MaxHealth { get; set; }

    #endregion

    #region IAttack Fields

    [SerializeField] private float damage;
    public float Damage { get { return damage; } }
    //public float cooldown { get; set; }
    //public float delayBeforeDamage { get; set; }

    #endregion

    #region Hunger

    [Space]
    [SerializeField] private float secondsToReduce;
    private float MaxHunger = 100;
    private float CurrentHunger;

    #endregion

    #region Indicators

    [Space]
    [SerializeField] private Indicator healthIndicator;
    [SerializeField] private Indicator hungerIndicator;
    [SerializeField] private Indicator manaIndicator;

    #endregion

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

        CurrentHunger = MaxHunger;

        ObjectRB = GetComponent<Rigidbody2D>();

        StateMachine.Initialize(IdleState);

        inventory.ItemSelectedEvent += OnItemSelected;
        inventory.ItemDeselectedEvent += OnItemDeselected;

        StartCoroutine("HungerCountdown");
    }

    private void Update()
    {
        StateMachine.CurrentState.FrameUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    private void OnDestroy()
    {
        inventory.ItemSelectedEvent -= OnItemSelected;
        inventory.ItemDeselectedEvent -= OnItemDeselected;
    }

    #region IDamageable Methods

    public void Death()
    {
        GameState.isDead = true;
        Time.timeScale = 0f;
    }

    public void GetDamage(float damage, GameObject attacker)
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

        target.GetDamage(Damage, gameObject);
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
        ObjectRB.velocity = velocity;
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

    private IEnumerator HungerCountdown()
    {
        while(CurrentHunger > 0)
        {
            yield return new WaitForSeconds(secondsToReduce);

            float reduceValue = -1;

            // TODO: Застосувати послаблення/посилення

            ChangeHunger(reduceValue);
        }
    }

    private void ChangeHunger(float changeValue)
    {
        float newValue = CurrentHunger + changeValue;

        if(newValue < 0)
        {
            CurrentHunger = 0;
        }
        else if(newValue > 100)
        {
            CurrentHunger = 100;
        }
        else
        {
            CurrentHunger = newValue;
        }

        hungerIndicator.SetValue(CurrentHunger, MaxHunger);
    }

    private void OnItemSelected(ItemSO item)
    {
        selectedItem = item;

        Debug.Log(item.Title);
    }

    private void OnItemDeselected()
    {
        selectedItem = null;
    }

    public void UseSelectedItem()
    {
        if(selectedItem is not null && selectedItem.Effect is not null)
        {
            switch (selectedItem.Effect.EffectProperty)
            {
                case Enums.EffectProperty.Hunger:
                    ChangeHunger(selectedItem.Effect.Value);
                    inventory.ConsumeSelectedItem();
                    break;
                default:
                    Debug.Log($"No effect or not implemented: {selectedItem.Effect.EffectProperty}");
                    break;
            }
        }
    }
}
