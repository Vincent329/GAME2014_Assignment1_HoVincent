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

[System.Serializable]
public class EnemyManager
{
    private static EnemyManager instance = null;

    private EnemyManager()
    {
        Initialize();
    }

    public static EnemyManager GetInstance()
    {
        if (instance == null)
            instance = new EnemyManager();
        return instance;
    }

    public List<Queue<GameObject>> enemyPools;

    private void Initialize()
    {
        enemyPools = new List<Queue<GameObject>>();

        // instantiate  new queue collections based on number of bullet types
        for (int count = 0; count < (int)EnemyType.NUM_ENEMY_TYPES; count++)
        {
            enemyPools.Add(new Queue<GameObject>()); // remember that it's a LIST, not an ARRAY
        }
        // find an object of type bullet factory in the hierarchy

    }

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
        // get the bullet from the queue
        temp_Enemy = enemyPools[(int)eType].Dequeue();

        temp_Enemy.transform.position = spawnPosition;
        temp_Enemy.SetActive(true);

        return temp_Enemy;
    }

    /// <summary>
    /// This method returns a bullet back into the bullet pool
    /// </summary>
    /// <param name="returnedBullet"></param>
    public void ReturnBullet(GameObject returnedEnemy, EnemyType eType = EnemyType.OGRE)
    {
        returnedEnemy.SetActive(false);

        // depending on the type of the bullet, return it back to its respective bullet pool
        enemyPools[(int)eType].Enqueue(returnedEnemy);

    }
}
