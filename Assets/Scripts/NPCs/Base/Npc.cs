using UnityEngine;

public class Npc : MonoBehaviour /*, IDamageable, IAttack, IMoveable, ITriggerCheckable*/
{
    //[SerializeField] private NPCDescriptionSO description;
    //public NPCDescriptionSO DescriptionSO { get { return description; } }

    //[SerializeField] Indicator healthIndicator;
    //public float CurrentHealth { get; set; }
    //public float MaxHealth { get; set; }
    //public Rigidbody2D objectRB { get; set; }
    //public bool IsFacingRight { get; set; } = true;
    //public bool IsPlayerNoticed { get; set; }
    //public bool IsWithinAttackDistance { get; set; }
    //public bool isAggroed { get; set; }
    //public bool IsTargetNoticed { get; set; }
    //public List<Npc> targets;
    //public List<Npc> pack;
    //private GameObject chaseTarget;
    //public GameObject ChaseTarget { get {  return chaseTarget; } }

    //#region State Machine Fields

    //public NPCStateMachine StateMachine { get; set; }
    //public NPCIdleState IdleState { get; set; }
    //public NPCPlayerNoticedState PlayerNoticedState { get; set; }
    //public NPCAttackState AttackState { get; set; }
    //public NPCChaseState ChaseState { get; set; }

    //[SerializeField] private NPCIdleSOBase idleBase;
    //[SerializeField] private PlayerNoticedSOBase playerNoticedBase;
    //[SerializeField] private NPCAttackSOBase attackBase;
    //[SerializeField] private NPCChaseStateSOBase chaseStateBase;

    //public NPCIdleSOBase IdleBaseInstance { get; set; }
    //public PlayerNoticedSOBase PlayerNoticedBaseInstance { get; set; }
    //public NPCAttackSOBase AttackBaseInstance { get; set; }
    //public NPCChaseStateSOBase ChaseStateInstance { get; set; }

    //#endregion

    //private void Awake()
    //{
    //    IdleBaseInstance            = Instantiate(idleBase);
    //    PlayerNoticedBaseInstance   = Instantiate(playerNoticedBase);
    //    AttackBaseInstance          = Instantiate(attackBase);
    //    ChaseStateInstance          = Instantiate(chaseStateBase);

    //    StateMachine                = new NPCStateMachine();
    //    //IdleState                   = new NPCIdleState(this, StateMachine);
    //    //PlayerNoticedState          = new NPCPlayerNoticedState(this, StateMachine);
    //    //AttackState                 = new NPCAttackState(this, StateMachine);
    //    //ChaseState                  = new NPCChaseState(this, StateMachine);
    //}

    //private void Start()
    //{
    //    MaxHealth = description.HealthPoints;
    //    CurrentHealth = MaxHealth;

    //    isAggroed = description.Approach == Enums.NPCApproach.Agressive ? true : false;

    //    targets = new List<Npc>();
    //    pack = new List<Npc>();

    //    objectRB = GetComponent<Rigidbody2D>();

    //    //IdleBaseInstance.Initialize(gameObject, this);
    //    //PlayerNoticedBaseInstance.Initialize(gameObject, this);
    //    //AttackBaseInstance.Initialize(gameObject, this);
    //    //ChaseStateInstance.Initialize(gameObject, this);

    //    StateMachine.Initialize(IdleState);
    //}

    //private void Update()
    //{
    //    StateMachine.CurrentState.FrameUpdate();
    //}

    //private void FixedUpdate()
    //{
    //    StateMachine.CurrentState.PhysicsUpdate();
    //}

    //#region Attack Logic

    //[field: SerializeField] public float Damage { get; set; } = 5f;
    //[field: SerializeField] public float cooldown { get; set; } = 5f;
    //[field: SerializeField] public float delayBeforeDamage { get; set; } = 3f;

    //public bool isOnCooldown = false;

    //public void Attack(IDamageable target)
    //{
    //    objectRB.velocity = Vector2.zero;

    //    if (!isOnCooldown)
    //    {
    //        StartCoroutine(DelayedAttack(target));
    //    }
    //}

    //private IEnumerator DelayedAttack(IDamageable target)
    //{
    //    isOnCooldown = true;
    //    Debug.Log("Attack started");

    //    yield return new WaitForSeconds(delayBeforeDamage);

    //    if (IsWithinAttackDistance)
    //    {
    //        ApplyDamage(target);
    //    }

    //    yield return new WaitForSeconds(cooldown - delayBeforeDamage);

    //    isOnCooldown = false;
    //    Debug.Log("Attack ended");
    //}

    //public void ApplyDamage(IDamageable target)
    //{
    //    target.GetDamage(Damage);
    //}

    //#endregion

    //#region Health / Death

    //public void Death()
    //{
    //    Destroy(gameObject);
    //}

    //public void GetDamage(float damage)
    //{
    //    CurrentHealth -= damage;

    //    //healthIndicator.SetValue(CurrentHealth, MaxHealth);

    //    if (CurrentHealth < 0f) Death();
    //}

    //#endregion

    //#region Movement

    //public void Move(Vector2 velocity)
    //{
    //    objectRB.velocity = velocity;
    //    CheckFacing(velocity);
    //}

    //public void CheckFacing(Vector2 velocity)
    //{
    //    if (IsFacingRight && velocity.x < 0f)
    //    {
    //        Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
    //        transform.rotation = Quaternion.Euler(rotator);
    //        IsFacingRight = !IsFacingRight;
    //    }
    //    else if (!IsFacingRight && velocity.x > 0f)
    //    {
    //        Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
    //        transform.rotation = Quaternion.Euler(rotator);
    //        IsFacingRight = !IsFacingRight;
    //    }
    //}

    //#endregion

    //#region Animation Triggers
    //private void AnimationTriggerEvent(AnimationTriggerType type)
    //{
    //    // TODO
    //}

    //public enum AnimationTriggerType
    //{
    //    NpcDamaged,
    //    PlayFootstepsSound
    //}

    //#endregion

    //#region Distance Checks

    //public void AddTarget(Npc target)
    //{
    //    targets.Add(target);

    //    if(target.DescriptionSO == DescriptionSO)
    //    {
    //        pack.Add(target);
    //    }
    //}

    //public void RemoveTarget(Npc target)
    //{
    //    targets.Remove(target);

    //    if (target.DescriptionSO == DescriptionSO)
    //    {
    //        pack.Remove(target);
    //    }
    //}

    //public void SetPlayerNoticedStatus(bool isPlayerNoticed)
    //{
    //    IsPlayerNoticed = isPlayerNoticed;
    //}

    //public void SetAttackDistanceBool(bool isWithinAttackDistance)
    //{
    //    IsWithinAttackDistance = isWithinAttackDistance;
    //}

    //#endregion

    //public Enums.TargetDecisions DecideTarget(Npc target)
    //{
    //    if(DescriptionSO.AttackTargets.Contains(target.DescriptionSO))
    //    {
    //        return Enums.TargetDecisions.Chase;
    //    }
    //    else
    //    {
    //        return Enums.TargetDecisions.Ignore;
    //    }
    //}

    //public void SetChaseTarget(GameObject target)
    //{
    //    chaseTarget = target;
    //}
}
