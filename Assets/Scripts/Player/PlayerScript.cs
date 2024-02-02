using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerScript : MonoBehaviour, IDamageable//, IAttack, 
{
   private float HungerPoints;
    private float ManaPoints;

    private bool AttackAllowed;

    public float damage = 10;
    public float cooldown = 2;
    public float delayBeforeDamage = 1;
    public float CurrentHealth { get; set; } = 50;
    public float MaxHealth { get; set; } = 100;

    private float MoveSpeed = 5f;
    [SerializeField] private Rigidbody2D body;

    [SerializeField] private CircleCollider2D InteractionCollider;

    private Vector2 MoveDirection;

    //float IAttack.damage { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    //float IAttack.cooldown { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    //float IAttack.delayBeforeDamage { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    void Start()
    {
        HungerPoints = 50f;
        ManaPoints = 50f;

    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();

        
    }

    public void FixedUpdate()
    {
        Move();
    }

    //-------------------------------movement controls
    private void ProcessInput()
    {
        float MoveX = Input.GetAxisRaw("Horizontal");
        float MoveY = Input.GetAxisRaw("Vertical");

        MoveDirection = new Vector2(MoveX, MoveY).normalized;
    }
    private void Move()
    {
        body.velocity = new Vector2(MoveDirection.x * MoveSpeed, MoveDirection.y * MoveSpeed);
    }

    //-------------------------------attack controls

    public void Attack(IDamageable target)
    {
        if (InteractionCollider.gameObject.tag.Equals("Enemy"))
        {
            InteractionCollider.gameObject.SetActive(true);
            AttackAllowed = true;
        }
    }

    public void ApplyDamage(IDamageable target)
    {
        if (AttackAllowed == true & Input.GetMouseButtonDown(0))
        {
            //target.GetCurentHealth -= this.damage;
            //this.setCooldown = 5;
        }
    }

    public void GetDamage(float damage)
    {
        CurrentHealth -= damage;
        Debug.Log(CurrentHealth);
    }

    public void Death()
    {
        if (CurrentHealth <= 0)
        {
            GameState.isDead = true;
        }
    }
}
