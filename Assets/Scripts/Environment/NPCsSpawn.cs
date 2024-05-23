using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCsSpawn : MonoBehaviour
{
    
    [SerializeField]private GameObject enemyPrefab; // The enemy prefab to spawn
    public Transform spawnPoint;   // The spawn point
    [SerializeField] private int maxEnemies = 5;     // Maximum number of enemies on the map
    [SerializeField] private float spawnInterval = 2f; // Time interval between spawns

    private int currentEnemyCount = 0;

    void Start()
    {
        // Start spawning enemies at intervals
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        // Check if the current enemy count is less than the max allowed
        if (currentEnemyCount < maxEnemies)
        {
            // Instantiate a new enemy at the spawn point
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            currentEnemyCount++;
        }
    }

    public void EnemyDestroyed()
    {
        // Decrease the enemy count when an enemy is destroyed
        currentEnemyCount--;
    }
}

