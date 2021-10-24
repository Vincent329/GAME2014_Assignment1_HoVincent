//-----------EnemyManager.cs------------
/* Name: Vincent Ho
 * Student Number: 101334300
 * 
 * Date Last Modified: October 21, 2021
 * 
 * Description: This script is used as a manager class for enemy spawning
 * Follows the Manager and Factory algorithms used in class to spawn differing types of enemies
 * 
 * Revision History:
 * 1) created script
 * 2) setting it up as a singleton
 * 3) finishing up functionality
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Enemy Manager class
/// </summary>
[System.Serializable]
public class EnemyManager
{
    private static EnemyManager instance = null;

    private EnemyManager()
    {
        Initialize();
    }

    /// <summary>
    /// Gets an instance of the enemy manager
    /// </summary>
    /// <returns></returns>
    public static EnemyManager GetInstance()
    {
        if (instance == null)
            instance = new EnemyManager();
        return instance;
    }

    // sets up a list of queues for different enemy pools
    public List<Queue<GameObject>> enemyPools;

    private void Initialize()
    {
        enemyPools = new List<Queue<GameObject>>(); // set the list of queues in memory

        // instantiate  new queue collections based on number of enemy types
        for (int count = 0; count < (int)EnemyType.NUM_ENEMY_TYPES; count++)
        {
            enemyPools.Add(new Queue<GameObject>()); // remember that it's a LIST, not an ARRAY
        }

    }

    /// <summary>
    /// Depending on enemy type, create the enemy and place it in the resepective enemy pool
    /// </summary>
    /// <param name="eType"></param>
    private void AddEnemy(EnemyType eType = EnemyType.OGRE)
    {
        var temp_Enemy = EnemyFactory.Instance().createEnemy(eType);
        enemyPools[(int)eType].Enqueue(temp_Enemy);
    }

    /// <summary>
    /// This method removes an enemy prefab from the enemy pool
    /// and returns a reference to it.
    /// </summary>
    /// <param name="spawnPosition"></param>
    /// <returns></returns>
    public GameObject GetEnemy(Vector2 spawnPosition, EnemyType eType = EnemyType.OGRE)
    {
        GameObject temp_Enemy = null;

        if (enemyPools[(int)eType].Count < 1)
        {
            AddEnemy(eType);
        }
        // get the enemy from the queue
        temp_Enemy = enemyPools[(int)eType].Dequeue();

        temp_Enemy.transform.position = spawnPosition;
        temp_Enemy.SetActive(true);

        return temp_Enemy;
    }

    /// <summary>
    /// This method returns a enemy back into its respective enemy pool pool
    /// </summary>
    /// <param name="returnedEnemy"></param>
    public void ReturnEnemy(GameObject returnedEnemy, EnemyType eType = EnemyType.OGRE)
    {
        returnedEnemy.SetActive(false);
        enemyPools[(int)eType].Enqueue(returnedEnemy);

    }
}
