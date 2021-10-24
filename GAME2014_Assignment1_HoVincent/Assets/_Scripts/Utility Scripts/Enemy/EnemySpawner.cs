using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate;
    public float SpawnRate
    {
        get => spawnRate;
        set
        {
            spawnRate = value;
        }
    }

    [SerializeField] private float spawnTimer;

    [SerializeField] private Boundaries bounds;
    [SerializeField] private int maxEnemies;
   
    void Start()
    {
        spawnTimer = 0;   
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += (spawnRate * Time.deltaTime);
        if (spawnTimer >= 10)
        {
            Debug.Log("SpawnEnemy");

            float x = Random.Range(-bounds.absX, bounds.absX);
            float y = Random.Range(-bounds.absY, bounds.absY);
            Vector2 spawnPos = new Vector2(x, y);

            SpawnEnemy(spawnPos);

            spawnTimer = 0;
        }
    }

    private void SpawnEnemy(Vector2 spawnPos)
    {
        int enemyCount = GameObject.FindObjectsOfType<Enemy>().Length + 1;
        if (enemyCount <= maxEnemies)
        {

            // generate a random type
            var randomEnemy = Random.Range(0, (int)EnemyType.NUM_ENEMY_TYPES);
            EnemyType eType = (EnemyType)randomEnemy;

            EnemyManager.GetInstance().GetEnemy(spawnPos, eType);
        } else
        {
            Debug.Log("Max amount of enemies on field");
        }
    }
}
