using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private float HealthPoints;
    private float HungerPoints;
    private float ManaPoints;

    private float MoveSpeed = 5f;
    [SerializeField] private Rigidbody2D body;

    private Vector2 MoveDirection;

    void Start()
    {
        HealthPoints = 50f;
        HungerPoints = 50f;
        ManaPoints = 50f;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();

        if (HealthPoints <= 0)
        {
            EditorApplication.ExitPlaymode();
        }
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
    private void BasicAttack()
    {

    }
}
