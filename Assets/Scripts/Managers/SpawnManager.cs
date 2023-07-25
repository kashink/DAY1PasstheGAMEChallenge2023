using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Space()]
    [Header("Config")]
    [SerializeField] EnemyController enemyPrefab;
    [SerializeField] Vector2 minPos;
    [SerializeField] Vector2 maxPos;

    [SerializeField] int spawnEnemyQuant = 1;
    [SerializeField] int increaseSpawnEnemyBy = 1;

    [SerializeField] float increaseSpawnEnemyAfter = 20f;
    [SerializeField] float intervalBetweenSpawn = 2f;
    float currentIntervalBetweenSpawn = 0;
    float currentIncreaseSpawnEnemyAfter = 0;

    [Space()]
    [Header("Enemy Stats")]
    [SerializeField] int life = 1;
    [SerializeField] int damage = 5;
    [SerializeField] int coins = 1;

    private void Update()
    {
        if (GameManager.Instance.gameState != GameState.GameRunning) return;

        HandleDecreaseSpawnTime();

        this.currentIntervalBetweenSpawn += Time.deltaTime;
        if (this.currentIntervalBetweenSpawn < this.intervalBetweenSpawn) return;

        this.currentIntervalBetweenSpawn = 0;
        SpawnEnemy();
    }

    void HandleDecreaseSpawnTime()
    {
        this.currentIncreaseSpawnEnemyAfter += Time.deltaTime;
        if (this.currentIncreaseSpawnEnemyAfter < this.increaseSpawnEnemyAfter) return;

        this.currentIncreaseSpawnEnemyAfter = 0;
        this.spawnEnemyQuant += this.increaseSpawnEnemyBy;
    }

    void SpawnEnemy()
    {
        int totalEnemySpawned = 0;

        while (totalEnemySpawned < this.spawnEnemyQuant)
        {
            Vector2 spawnPos = new Vector2(Random.Range(this.minPos.x, this.maxPos.x), Random.Range(this.minPos.y, this.maxPos.y));

            float randomPosLimit = Random.Range(0, 100);
            if (randomPosLimit < 25)
            {
                spawnPos.x = minPos.x;
            }
            else if (randomPosLimit < 50)
            {
                spawnPos.y = minPos.y;
            }
            else if (randomPosLimit < 75)
            {
                spawnPos.x = maxPos.x;
            }
            else
            {
                spawnPos.y = maxPos.y;
            }

            EnemyController enemyInstance = Instantiate(this.enemyPrefab, spawnPos, this.transform.rotation);
            enemyInstance.Setup(this.life, this.damage, this.coins);

            totalEnemySpawned++;
        }
    }
}
