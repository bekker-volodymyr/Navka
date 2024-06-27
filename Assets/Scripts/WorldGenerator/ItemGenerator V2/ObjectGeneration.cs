using System;
using System.Drawing;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{

    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private int Density = 10;
    public Vector2 spawnAreaSize = new Vector2(10f, 10f);
    public LayerMask collisionLayer; // Define the layer mask for collision checking
    [SerializeField] private GameObject parentObject;

    virtual protected void Start()
    {
        if (objectPrefab.name == "Village")
        {
            GenerateBigObject();
            //Invoke("GenerateObjects", 0.5f);
        }
        else
        {
            GenerateObjects();
        }
        GenerateObjects();
    }

    void GenerateBigObject()
    {
        Vector2 randomPosition = GetRandomPosition();
        BoxCollider2D boxCollider = objectPrefab.GetComponent<BoxCollider2D>();
        Vector2 size = boxCollider.size;
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(randomPosition, size/2, 0, collisionLayer);

        // Destroy all objects that are touching the newly placed prefab
        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.gameObject != objectPrefab) // Make sure not to destroy the newly placed prefab
            {
                Destroy(hitCollider.gameObject);
            }
        }

        Instantiate(objectPrefab, randomPosition, Quaternion.identity, parentObject.transform);

    }

    void GenerateObjects()
    {
        for (int i = 0; i < Density; i++)
        {
            Vector2 randomPosition = GetRandomPosition();
            BoxCollider2D boxCollider = objectPrefab.GetComponent<BoxCollider2D>();
            Vector2 size = boxCollider.size;

            // Check for collision
            //Collider2D[] colliders = Physics2D.OverlapBoxAll(randomPosition, objectPrefab.transform.position, 0, collisionLayer);
            Collider2D hitCollider = Physics2D.OverlapBox(randomPosition, size, 0, collisionLayer);

            //if (colliders.Length == 0) // If no collision detected, instantiate the object
            if (hitCollider == null) // If no collision detected, instantiate the object
            {
                Instantiate(objectPrefab, randomPosition, Quaternion.identity, parentObject.transform);
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
        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnAreaSize.x, spawnAreaSize.y, 1));
    }
}