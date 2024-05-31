using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IMoveable, IDamageable, IAttack, IInteract
{
    [Space]
    [SerializeField] private GameObject spriteGO;

    #region Movement Variables
    [Space]
    [SerializeField] private Rigidbody2D playerRB;
    public Rigidbody2D ObjectRB { get { return playerRB; } }
    [SerializeField] private float moveSpeed = 5f;
    private bool isFacingRight = true;
    #endregion

    #region Colliders
    [Space]
    [SerializeField] private CircleCollider2D attackRadius;
    public CircleCollider2D AttackRadius { get { return attackRadius; } }
    [SerializeField] private CircleCollider2D noticeRadius;
    public  CircleCollider2D NoticeRadius { get { return noticeRadius; } }
    #endregion

    #region Inventory Variables
    [Space]
    [SerializeField] private InventoryController inventory;
    private ItemSO selectedItem = null;
    public ItemSO SelectedItem { get { return selectedItem; } }
    #endregion

    #region Health Variables
    private float currentHealth;
    private float maxHealth;
    public float CurrentHealth { get { return currentHealth; } }
    public float MaxHealth { get { return maxHealth; } }
    #endregion

    #region Damage Variables
    [Space]
    [SerializeField] private float damage;
    public float Damage { get { return damage; } }
    #endregion

    #region Hunger Variables
    [Space]
    [SerializeField] private float secondsToReduce;
    private float currentHunger;
    public float CurrentHunger { get { return currentHunger; } }
    private float maxHunger = 100f;
    public float MaxHunger { get { return maxHunger; } }
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
    public PlayerDialogState DialogState { get; set; }
    #endregion

    private void Awake()
    {
        StateMachine = new StateMachine();
        IdleState    = new PlayerIdleState(this, StateMachine);
        MoveToPointState = new PlayerMoveToPointState(this, StateMachine);
        LockToInteractState = new PlayerLockToInteractState(this, StateMachine);
        LockToTargetState = new PlayerLockToTargetState(this, StateMachine);
        UnderCoverState = new PlayerUnderCoverState(this, StateMachine);
        DialogState = new PlayerDialogState(this, StateMachine);
    }

    private void Start()
    {
        currentHealth = maxHealth;

        currentHunger = maxHunger;

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

        StopAllCoroutines();
    }

    #region Damage / Death Logic
    public void Death()
    {
        GameState.isDead = true;
        Time.timeScale = 0f;
    }
    public void GetDamage(float damage, GameObject attacker)
    {
        // TODO: ������������ �������� �� ���������� ��������� �����

        float newHealth = currentHealth - damage;

        Debug.Log(newHealth);

        if(newHealth <= 0)
        {
            currentHealth = 0;
            healthIndicator.SetValue(currentHealth, maxHealth);
            Death();
        }
        else
        {
            currentHealth = newHealth;
            healthIndicator.SetValue(currentHealth, maxHealth);
        }
    }
    #endregion

    #region Attack Logic
    public void Attack(IDamageable target)
    {
        // TODO: ������������ �������� �� ���������� ��� ��������� �����

        target.GetDamage(damage, gameObject);
    }
    public Collider2D[] GetAllItemsInCollisionRadius()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, noticeRadius.radius);
        return colliders;
    }
    #endregion

    #region Movement Logic
    public void Move(Vector2 velocity)
    {
        playerRB.velocity = velocity;
        CheckFacing(velocity);
    }
    public void CheckFacing(Vector2 velocity)
    {
        if (isFacingRight && velocity.x < 0f)
        {
            Vector3 rotator = new Vector3(spriteGO.transform.rotation.x, 180f, spriteGO.transform.rotation.z);
            spriteGO.transform.rotation = Quaternion.Euler(rotator);
            isFacingRight = !isFacingRight;
        }
        else if (!isFacingRight && velocity.x > 0f)
        {
            Vector3 rotator = new Vector3(spriteGO.transform.rotation.x, 0f, spriteGO.transform.rotation.z);
            spriteGO.transform.rotation = Quaternion.Euler(rotator);
            isFacingRight = !isFacingRight;
        }
    }
    #endregion

    #region Interactions

    public void Interact(IInteractable target)
    {
        target.OnInteraction();
    }

    #endregion

    #region Hunger Logic
    private IEnumerator HungerCountdown()
    {
        while(CurrentHunger > 0)
        {
            yield return new WaitForSeconds(secondsToReduce);

            float reduceValue = -1;

            // TODO: ����������� �����������/���������

            ChangeHunger(reduceValue);
        }
    }
    private void ChangeHunger(float changeValue)
    {
        float newValue = currentHunger + changeValue;

        if(newValue < 0)
        {
            currentHunger = 0;
        }
        else if(newValue > 100)
        {
            currentHunger = 100;
        }
        else
        {
            currentHunger = newValue;
        }

        hungerIndicator.SetValue(currentHunger, maxHunger);
    }
    #endregion

    #region Item Logic
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
    #endregion
}
