
//------------EnemySpawner.cs--------------------
/* Name: Vincent Ho
 * Student Number: 101334300
 * 
 * Date Last Modified: October 24, 2021
 * 
 * Description: Script used to designate the Enemy Spawner
 * Can modify the rate at which enemies spawn, the area within to spawn the enemy in, and the maximum amount of enemies to spawn
 * 
 * Revision History:
 * 1) Create Script
 * 2) set up spawning logic, have a float that increases via time.deltaTime multiplied by spawn rate.  Once threshold is reached, spawn a random enemy
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Enemy Spawner class
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    // multiplier that dictates how fast the spawn timer increase
    // getter and setter created as well for altering purposes
    [SerializeField] private float spawnRate; 
    public float SpawnRate
    {
        get => spawnRate;
        set
        {
            spawnRate = value;
        }
    }

    // local spawn timer
    [SerializeField] private float spawnTimer;

    // bounding area for which enemies can spawn inside from
    [SerializeField] private Boundaries bounds;

    // maximum amount of enemies that can be spawned in a level
    [SerializeField] private int maxEnemies;
   
    void Start()
    {
        spawnTimer = 0;   
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += (spawnRate * Time.deltaTime); // spawn timer increases with Time.deltaTime * spawnRate, where spawn rate is a multiplier

        // should the spawn timer surpass a given threshold
        if (spawnTimer >= 10)
        {

            // create a vector 2 with a random x and y that lies within the boundaries set in the inspector
            float x = Random.Range(-bounds.absX, bounds.absX);
            float y = Random.Range(-bounds.absY, bounds.absY);
            Vector2 spawnPos = new Vector2(x, y);

            // spawn the enemy at that position
            SpawnEnemy(spawnPos);

            // reset the timer
            spawnTimer = 0;
        }
    }

    /// <summary>
    /// Function to spawn an enemy given a position
    /// </summary>
    /// <param name="spawnPos"></param>
    private void SpawnEnemy(Vector2 spawnPos)
    {
        // first get a count of enemies currently in the level
        int enemyCount = GameObject.FindObjectsOfType<Enemy>().Length + 1;
        
        // if we haven't reached that number yet
        if (enemyCount <= maxEnemies)
        {
            // generate a random type of enemy in the level
            var randomEnemy = Random.Range(0, (int)EnemyType.NUM_ENEMY_TYPES);
            EnemyType eType = (EnemyType)randomEnemy;

            // call the enemy manager to dequeue an enemy from that respective pool and place it at the set position
            EnemyManager.GetInstance().GetEnemy(spawnPos, eType);
        } else
        {
            Debug.Log("Max amount of enemies on field");
        }
    }
}
