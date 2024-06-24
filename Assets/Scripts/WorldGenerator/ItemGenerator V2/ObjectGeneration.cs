using System;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{

    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private int Density = 10;
    public Vector2 spawnAreaSize = new Vector2(10f, 10f);
    public LayerMask collisionLayer; // Define the layer mask for collision checking

    virtual protected void Start()
    {
        GenerateObjects();
    }

    void GenerateObjects()
    {
        for (int i = 0; i < Density; i++)
        {
            Vector2 randomPosition = GetRandomPosition();

            // Check for collision
            Collider2D[] colliders = Physics2D.OverlapBoxAll(randomPosition, objectPrefab.transform.localScale, 0, collisionLayer);
            if (colliders.Length == 0) // If no collision detected, instantiate the object
            {
                Instantiate(objectPrefab, randomPosition, Quaternion.identity, transform);
            }
        }
    }

    Vector2 GetRandomPosition()
    {
        return transform.position + new Vector3(UnityEngine.Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                                                 UnityEngine.Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2),
                                                 0);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnAreaSize.x, spawnAreaSize.y, 1));
    }
}