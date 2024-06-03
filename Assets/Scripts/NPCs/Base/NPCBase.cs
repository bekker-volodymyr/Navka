using System.Collections.Generic;
using UnityEngine;

public class NPCBase : MonoBehaviour, IMoveable, IDamageable, IAttack, IInteractable
{
    [Space]
    [SerializeField] private NPCDescriptionSO description;
    public NPCDescriptionSO DescriptionSO { get { return description; } }

    [Space]
    [SerializeField] private Item itemPrefab;

    [Space]
    [SerializeField] private Indicator healthIndicator;

    [Space]
    [SerializeField] private Rigidbody2D npcRB;
    public Rigidbody2D ObjectRB { get {  return npcRB; } }

    private bool isFacingRight = true;

    [Space]
    [SerializeField] private GameObject spriteGO;

    [Space]
    [SerializeField] private Enums.InteractionType interactType;
    public Enums.InteractionType InteractionType { get { return interactType; } }

    private float currentHealth;
    private float maxHealth;
    public float CurrentHealth { get { return currentHealth; } }
    public float MaxHealth {  get { return maxHealth; }  }

    private float damage;
    public float Damage { get { return damage; } }

    private GameObject chaseTarget = null;
    public GameObject ChaseTarget { get { return chaseTarget; } }
    private List<NPCBase> pack;

    protected Player befriendedPlayer;
    public Player BefriendedPlayer => befriendedPlayer;

    #region Colliders
    [Space]
    [SerializeField] private CircleCollider2D noticeRadius;
    public CircleCollider2D NoticeRadius { get { return noticeRadius; } }
    [SerializeField] private CircleCollider2D attackRadius;
    public CircleCollider2D AttackRadius { get { return attackRadius; } }
    #endregion

    #region State Machine
    public NPCStateMachine StateMachine { get; set; }
    public NPCIdleState IdleState { get; set; }
    public NPCChaseState ChaseState { get; set; }
    public NPCDialogState DialogState { get; set; }
    public NPCBefriendedState BefriendedState { get; set; }

    [Space]
    [SerializeField] private NPCIdleSOBase idleStateBase;
    [SerializeField] private NPCChaseSOBase chaseStateBase;
    [SerializeField] private NPCDialogSOBase dialogStateBase;
    [SerializeField] private NPCBefriendedSOBase befriendedStateBase;

    public NPCIdleSOBase IdleStateInstance { get; set; }
    public NPCChaseSOBase ChaseStateInstance { get; set; }
    public NPCDialogSOBase DialogStateInstance { get; set; }
    public NPCBefriendedSOBase BefriendedStateInstance { get; set; }
    #endregion

    #region Movement Logic
    public void Move(Vector2 velocity)
    {
        npcRB.velocity = velocity;
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

    #region Damage / Death Logic
    public void GetDamage(float damage, GameObject attacker)
    {
        float newHealth = currentHealth;

        newHealth -= damage;

        currentHealth = newHealth <= 0f ? 0f : newHealth;

        healthIndicator.SetValue(currentHealth, maxHealth);

        foreach(var npc in pack)
        {
            npc.SetTarget(attacker);
        }

        if (currentHealth == 0f) Death();
    }
    public void Death()
    {
        DropLoot();

        Destroy(gameObject);
    }
    #endregion

    #region Drop Item Logic
    public void DropLoot()
    {
        for (int i = 0; i < description.Loot.Count; i++)
        {
            if (Random.value <= description.LootChance[i])
            {
                DropItem(description.Loot[i], 1);
            }
        }
    }
    public void DropItem(ItemSO item, int quantity)
    {
        Item newItem = Instantiate(itemPrefab);
        newItem.InitItem(item, quantity);
        newItem.gameObject.transform.position = transform.position + GetRandomPointInBottomSemiCircle();
    }
    public Vector3 GetRandomPointInBottomSemiCircle()
    {
        // Generate a random angle between 180 and 360 degrees
        float angle = Random.Range(180f, 360f);

        // Convert the angle to radians
        float angleInRadians = angle * Mathf.Deg2Rad;

        // Calculate the x and y coordinates using trigonometric functions
        float x = Mathf.Cos(angleInRadians);
        float y = Mathf.Sin(angleInRadians);

        // Return the point as a Vector2
        return new Vector3(x, y, transform.position.z);
    }
    #endregion

    public void Attack(IDamageable target)
    {
        target.GetDamage(damage, gameObject);
    }

    private void Awake()
    {
        IdleStateInstance = Instantiate(idleStateBase);
        ChaseStateInstance = Instantiate(chaseStateBase);
        DialogStateInstance = Instantiate(dialogStateBase);
        BefriendedStateInstance = Instantiate(befriendedStateBase);

        StateMachine = new NPCStateMachine();
        IdleState = new NPCIdleState(this, StateMachine);
        ChaseState = new NPCChaseState(this, StateMachine);
        DialogState = new NPCDialogState(this, StateMachine);
        BefriendedState = new NPCBefriendedState(this, StateMachine);
    }
    private void Start()
    {
        maxHealth = description.HealthPoints;
        currentHealth = maxHealth;
        healthIndicator.SetValue(currentHealth, maxHealth);

        damage = description.BasicDamage;

        pack = new List<NPCBase> { this };

        IdleStateInstance.Initialize(this);
        ChaseStateInstance.Initialize(this);
        DialogStateInstance.Initialize(this);
        BefriendedStateInstance.Initialize(this);

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

    public void AddNPCTarget(NPCBase target)
    {
        if(description.AttackTargets.Contains(target.DescriptionSO))
        {
            foreach (var npc in pack)
            {
                npc.SetTarget(target.gameObject);
            }
        }
        else if(description == target.DescriptionSO)
        {
            pack.Add(target);
        }
    }
    public void SetTarget(GameObject target)
    {
        if (chaseTarget == null)
        {
            chaseTarget = target;
            StateMachine.ChangeState(ChaseState);
        }
    }

    virtual public void OnInteraction(Player player)
    {
        Debug.Log($"No interaction with this NPC or not implemented. {player.name}.");
    }

    #region Triggers
    private void AnimationTriggerEvent(AnimationTriggerType type)
    {
        // TODO
    }
    public enum AnimationTriggerType
    {
        NPCDamaged,
        PlayFootstepsSound
    }
    #endregion
}
