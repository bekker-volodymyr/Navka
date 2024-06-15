using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Player : ItemDropper, IMoveable, IDamageable, IAttack, IInteract, ICoverable
{
    private SpriteRenderer spriteRenderer;
    public SpriteRenderer SpriteRenderer => spriteRenderer;

    private ICover cover = null;
    public ICover CoverGetter => cover;
    private bool isUnderCover = false;

    private BefriendedAnimals animals;
    public BefriendedAnimals Animals => animals;

    private bool isInDialog = false;

    private GameObject hideoutGO;
    public GameObject HideoutGO => hideoutGO;

    [Space]
    [SerializeField] private List<NPCDescriptionSO> defendFromList;
    public List<NPCDescriptionSO> DefendFromList => defendFromList;

    private List<ItemSO> amulets = new List<ItemSO>();
    private AmuletsManager amuletsManager;

    private List<SpellSO> spells = new List<SpellSO>();

    #region Movement Variables
    private Rigidbody2D playerRB;
    public Rigidbody2D ObjectRB => playerRB;
    private bool isFacingRight = true;
    #endregion

    #region Radius & Colliders
    private float visionRadius = 20f;
    public float VisionRadius => visionRadius;

    private float interactRadius = 4.5f;
    public float InteractRadius => interactRadius;
    private CircleCollider2D damageCollider;
    public CircleCollider2D DamageCollider => damageCollider;
    #endregion

    #region Inventory Variables
    private InventoryController inventory;
    private ItemSO selectedItem = null;
    public ItemSO SelectedItem => selectedItem;
    #endregion

    #region Health Variables
    private float currentHealth;
    [Space]
    [SerializeField] private float maxHealth;
    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;
    #endregion

    #region Damage Variables
    [Space]
    [SerializeField] private float damage;
    public float Damage => damage;
    #endregion

    #region Hunger Variables
    [Space]
    [SerializeField] private float secondsToReduce;
    private float currentHunger;
    public float CurrentHunger => currentHunger;
    private float maxHunger = 100f;
    public float MaxHunger => maxHunger;
    #endregion

    #region Mana Variables
    private float currentMana;
    public float CurrentMana => currentMana;
    private float maxMana = 100f;
    public float MaxMana => maxMana;
    private float timeToRestore = 10f;
    private bool manaCanRestore = true;
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

    virtual protected void Start()
    {
        #region Initialize indicators
        
        currentHealth = maxHealth;
        currentHunger = maxHunger;
        currentMana = maxMana;
        
        healthIndicator = GameObject.FindGameObjectWithTag("HealthPlayer").GetComponent<Indicator>();
        hungerIndicator = GameObject.FindGameObjectWithTag("HungerPlayer").GetComponent<Indicator>();
        manaIndicator = GameObject.FindGameObjectWithTag("ManaPlayer").GetComponent<Indicator>();

        healthIndicator.SetValue(currentHealth, maxHealth);
        hungerIndicator.SetValue(currentHunger, maxHunger);
        manaIndicator.SetValue(currentMana, maxMana);

        #endregion

        StateMachine.Initialize(IdleState);

        amuletsManager = GameObject.FindGameObjectWithTag("Amulets Manager").GetComponent<AmuletsManager>();

        inventory = GameObject.FindGameObjectWithTag("Inventory Controller").GetComponent<InventoryController>();

        inventory.ItemSelectedEvent += OnItemSelected;
        inventory.ItemDeselectedEvent += OnItemDeselected;

        animals = GetComponent<BefriendedAnimals>();

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        playerRB = GetComponent<Rigidbody2D>();

        damageCollider = GetComponentInChildren<CircleCollider2D>();

        GameManager.DialogStartEvent += OnDialogStart;
        GameManager.DialogStopEvent += OnDialogEnd;

        Item.OnPickUp += OnItemPickedUp;

        StartCoroutine("HungerCountdown");
    }

    virtual protected void Update()
    {
        if (!isInDialog)
        {
            StateMachine.CurrentState.FrameUpdate();
        }
    }

    virtual protected void FixedUpdate()
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
        ChangeHealth(-CalculateDamageValue(damage));

        SetTargetForAnimals(attacker);
    }
    private float CalculateDamageValue(float damage)
    {
        float newDamage = damage;
        foreach(var amulet in amulets)
        {
            if(amulet.Effects.Count > 0)
            {
                foreach(var effect in amulet.Effects)
                {
                    if(effect.EffectProperty == Enums.EffectProperty.DamageGet)
                    {
                        newDamage -= effect.Value;
                    }
                }
            }
        }
        return newDamage <= 0f ? 0f : newDamage;
        
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

        target.GetDamage(CalculateAttackDamage(damage), gameObject);
    }
    private float CalculateAttackDamage(float damage)
    {
        float newDamage = damage;
        foreach (var amulet in amulets)
        {
            if (amulet.Effects.Count > 0)
            {
                foreach (var effect in amulet.Effects)
                {
                    if (effect.EffectProperty == Enums.EffectProperty.DamageAttack)
                    {
                        newDamage += effect.Value;
                    }
                }
            }
        }
        return newDamage <= 0f ? 0f : newDamage;
    }
    public Collider2D[] GetAllItemsInCollisionRadius()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, VisionRadius);
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
            Vector3 rotator = new Vector3(spriteRenderer.transform.rotation.x, 180f, spriteRenderer.transform.rotation.z);
            spriteRenderer.transform.rotation = Quaternion.Euler(rotator);
            isFacingRight = !isFacingRight;
        }
        else if (!isFacingRight && velocity.x > 0f)
        {
            Vector3 rotator = new Vector3(spriteRenderer.transform.rotation.x, 0f, spriteRenderer.transform.rotation.z);
            spriteRenderer.transform.rotation = Quaternion.Euler(rotator);
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
        if(selectedItem is not null && selectedItem.Type == Enums.ItemType.Amulet)
        {
            WearAmulet(selectedItem);
            return;
        }

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
    private void WearAmulet(ItemSO amulet)
    {
        amulets.Add(amulet);
        amuletsManager.AddAmulet(amulet, this);
        FeedItem();
    }
    public void TakeOffAmulet(ItemSO amulet)
    {
        amulets.Remove(amulet);
        inventory.AddItem(amulet, 1);
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

    #region Spells
    public void AddSpell(SpellSO spell)
    {
        spells.Add(spell);
    }

    public void RemoveSpell(SpellSO spell)
    {
        spells.Remove(spell);
    }

    public void ActivateSpell(SpellSO spell)
    {
        if(currentMana < spell.SpellDescription.ManaCost)
        {
            Debug.Log("Not enough mana");
            return;
        }

        spell.SpellLogic.Activate(this);

        ChangeMana(-spell.SpellDescription.ManaCost);
        if(manaCanRestore)
        {
            StartCoroutine(ManaRestore());
        }
    }

    private void ChangeMana(float value)
    {
        float newValue = currentMana + value;

        if(newValue >= 100f)
        {
            currentMana = 100f;
        }
        else if(newValue <= 0f)
        {
            currentMana = 0f;
        }
        else 
        {
            currentMana = newValue;
        }

        manaIndicator.SetValue(currentMana, maxMana);
    }

    private IEnumerator ManaRestore()
    {
        manaCanRestore = false;

        while (currentMana < maxMana)
        {
            yield return new WaitForSeconds(timeToRestore);
            ChangeMana(5f);
        }

        manaCanRestore = true;
    }
    #endregion
}