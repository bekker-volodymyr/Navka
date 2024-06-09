using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Player : ItemDropper, IMoveable, IDamageable, IAttack, IInteract, ICoverable
{
    [Space]
    [SerializeField] private GameObject spriteGO;
    public GameObject SpriteGO { get { return spriteGO; } }

    //PhotonView view;

    private ICover cover = null;
    public ICover CoverGetter { get { return cover; } }
    private bool isUnderCover = false;

    [Space]
    [SerializeField] private BefriendedAnimals animals;
    public BefriendedAnimals Animals { get { return animals; } }

    private bool isInDialog = false;

    [Space]
    [SerializeField] private GameObject hideoutGO;
    public GameObject HideoutGO { get { return hideoutGO; } }

    [Space]
    [SerializeField] private List<NPCDescriptionSO> defendFromList;
    public List<NPCDescriptionSO> DefendFromList { get { return defendFromList; } }

    #region Movement Variables
    [Space]
    [SerializeField] private Rigidbody2D playerRB;
    public Rigidbody2D ObjectRB { get { return playerRB; } }
    private bool isFacingRight = true;
    #endregion

    #region Colliders
    [Space]
    [SerializeField] private CircleCollider2D attackRadius;
    public CircleCollider2D AttackRadius { get { return attackRadius; } }
    [SerializeField] private CircleCollider2D noticeRadius;
    public CircleCollider2D NoticeRadius { get { return noticeRadius; } }
    [SerializeField] private CircleCollider2D interactCollider;
    public CircleCollider2D InteractCollider { get { return interactCollider; } }
    [SerializeField] private CircleCollider2D damageCollider;
    public CircleCollider2D DamageCollider { get { return damageCollider; } }

    #endregion

    #region Inventory Variables
    [Space]
    [SerializeField] private InventoryController inventory;
    private ItemSO selectedItem = null;
    public ItemSO SelectedItem { get { return selectedItem; } }
    #endregion

    #region Health Variables
    private float currentHealth;
    [Space]
    [SerializeField] private float maxHealth;
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
    private Indicator healthIndicator;
    private Indicator hungerIndicator;
    private Indicator manaIndicator;
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
        IdleState = new PlayerIdleState(this, StateMachine);
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

        inventory = GameObject.FindGameObjectWithTag("Inventory Controller").GetComponent<InventoryController>();

        inventory.ItemSelectedEvent += OnItemSelected;
        inventory.ItemDeselectedEvent += OnItemDeselected;

        GameManager.DialogStartEvent += OnDialogStart;
        GameManager.DialogStopEvent += OnDialogEnd;

        Item.OnPickUp += OnItemPickedUp;

        StartCoroutine("HungerCountdown");

        healthIndicator = GameObject.FindGameObjectWithTag("HealthPlayer").GetComponent<Indicator>();
        hungerIndicator = GameObject.FindGameObjectWithTag("HungerPlayer").GetComponent<Indicator>();
        manaIndicator = GameObject.FindGameObjectWithTag("ManaPlayer").GetComponent<Indicator>();

        //view = GetComponent<PhotonView>();
    }

    private void Update()
    {
        //if (view.IsMine)
        //{
            if (!isInDialog)
            {
                StateMachine.CurrentState.FrameUpdate();
            }
        //}
    }

    private void FixedUpdate()
    {
        if (!isInDialog)
        {
            StateMachine.CurrentState.PhysicsUpdate();
        }
    }

    private void OnDestroy()
    {
        inventory.ItemSelectedEvent -= OnItemSelected;
        inventory.ItemDeselectedEvent -= OnItemDeselected;

        GameManager.DialogStartEvent -= OnDialogStart;
        GameManager.DialogStopEvent -= OnDialogEnd;

        StopAllCoroutines();
    }

    #region Damage / Death Logic
    public void Death()
    {
        GameManager.isDead = true;
        Time.timeScale = 0f;
    }
    public void GetDamage(float damage, GameObject attacker)
    {
        ChangeHealth(-damage);

        SetTargetForAnimals(attacker);
    }
    private void ChangeHealth(float value)
    {
        float newHealth = currentHealth - damage;

        Debug.Log(newHealth);

        if (newHealth <= 0)
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
        // TODO: apply bonuses and debufs to damage value

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
        target.OnInteraction(gameObject);
        if (!isUnderCover)
        {
            StateMachine.ChangeState(IdleState);
        }
    }

    private void OnDialogStart(IDialog npc)
    {
        isInDialog = true;
    }

    private void OnDialogEnd()
    {
        isInDialog = false;
    }

    #endregion

    #region Hunger Logic
    private IEnumerator HungerCountdown()
    {
        while (!GameManager.isDead)
        {
            yield return new WaitForSeconds(secondsToReduce);

            float reduceValue = -1;

            // TODO: ����������� �����������/���������
            if (currentHunger > 0)
            {
                ChangeHunger(reduceValue);
            }
            else
            {
                ChangeHealth(-1);
            }
        }
    }
    private void ChangeHunger(float changeValue)
    {
        float newValue = currentHunger + changeValue;

        if (newValue < 0)
        {
            currentHunger = 0;
        }
        else if (newValue > 100)
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
    private void OnItemPickedUp(ItemSO item, int quantity)
    {
        StateMachine.ChangeState(IdleState);
    }
    private void OnItemSelected(ItemSO item)
    {
        selectedItem = item;
    }
    private void OnItemDeselected()
    {
        selectedItem = null;
    }
    public void UseSelectedItem()
    {
        if (selectedItem is not null && selectedItem.Effects is not null)
        {
            foreach (var effect in selectedItem.Effects)
            {
                switch (effect.EffectProperty)
                {
                    case Enums.EffectProperty.Hunger:
                        ChangeHunger(effect.Value);
                        inventory.ConsumeSelectedItem();
                        break;
                    default:
                        Debug.Log($"No effect or not implemented: {effect.name}");
                        break;
                }
            }
        }
    }
    public void DropItem()
    {
        if (selectedItem == null) return;

        inventory.DropSelectedItem();
        SpawnItem(selectedItem, 1);
        selectedItem = inventory.GetSelectedItem();
    }
    #endregion

    #region Befriending Logic
    public void FeedItem()
    {
        inventory.ConsumeSelectedItem();
        selectedItem = inventory.GetSelectedItem();
    }
    public void Befriend(BefriendableNPC npc)
    {
        Debug.Log($"{npc.name}");
        animals.AddAnimal(npc);
    }
    public void SetTargetForAnimals(GameObject target)
    {
        animals.SetTarget(target);
    }
    #endregion

    #region Cover
    public void Cover(ICover cover)
    {
        transform.position = cover.Position;
        this.cover = cover;

        isUnderCover = true;
        StateMachine.ChangeState(UnderCoverState);
    }
    public void LeaveCover()
    {
        cover.LeaveCover();
        cover = null;
    }
    #endregion
}