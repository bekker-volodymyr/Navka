using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour, IMoveable, IDamageable, IAttack, IInteract
{
    public Rigidbody2D objectRB { get; set; }

    [SerializeField] private float moveSpeed = 5f;

    #region State Machine Fields

    public StateMachine StateMachine { get; set; }
    public PlayerIdleState IdleState { get; set; }

    #endregion

    private void Awake()
    {
        StateMachine = new StateMachine();
        IdleState    = new PlayerIdleState(this, StateMachine);
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
    public float cooldown { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public float delayBeforeDamage { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    #endregion

    #region IDamageable Methods

    public void Death()
    {
        GameState.isDead = true;
        Time.timeScale = 0f;
    }

    public void GetDamage(float damage)
    {
        // TODO: ������������ �������� �� ���������� ��������� �����

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
        // TODO: ������������ �������� �� ���������� ��� ��������� �����

        target.GetDamage(Damage);
    }

    public void ApplyDamage(IDamageable target)
    {
        throw new System.NotImplementedException();
    }

    #endregion

    #region IMoveable Methods

    public void Move(Vector2 velocity)
    {
        objectRB.velocity = new Vector2(velocity.x * moveSpeed, velocity.y * moveSpeed); ;
    }

    public void CheckFacing(Vector2 velocity)
    {
        // TODO: ���������� �������� �� �������� ����
    }

    #endregion

    #region Interactions

    public void Interact(IInteractable target)
    {
        target.OnInteraction();
    }

    #endregion
}