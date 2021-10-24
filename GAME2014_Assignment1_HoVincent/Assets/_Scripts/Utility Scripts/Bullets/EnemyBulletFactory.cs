//------------EnemyBulletFactory.cs--------------------
/* Name: Vincent Ho
 * Student Number: 101334300
 * 
 * Date Last Modified: October 24, 2021
 * 
 * Description: This script is used to manage how often items spawn
 * Revision History:
 * 1) Create all necessary singleton functions for the Bullet factory, set up instance, object to parent to, find the prefab, and bullet creation
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy Bullet Factory script
/// </summary>
[System.Serializable]
public class EnemyBulletFactory
{
    private static EnemyBulletFactory instance = null; // private static 
    public GameObject enemyBullet; // Reference to the enemy bullet
    private EnemyBulletSpawner bulletSpawner; // Use this to child any bullets created to the game object

    private EnemyBulletFactory()
    {
        Initialize();
    }

    public static EnemyBulletFactory Instance()
    {
        if (instance == null)
        {
            instance = new EnemyBulletFactory();
        }
        return instance;
    }

    /// <summary>
    /// Loads the enemy bullet variable as a game object found in the resources folder
    /// </summary>
    private void Initialize()
    {

        enemyBullet = Resources.Load("Prefabs/Bullet") as GameObject;
        bulletSpawner = GameObject.FindObjectOfType<EnemyBulletSpawner>();
    }

    /// <summary>
    /// Instantiate the bullet, while leaving it as false in the queue
    /// </summary>
    /// <returns></returns>
    public GameObject createBullet()
    {
        GameObject temp_bullet = null;

        temp_bullet = MonoBehaviour.Instantiate(enemyBullet);
        temp_bullet.transform.parent = bulletSpawner.gameObject.transform;
        temp_bullet.SetActive(false);

        return temp_bullet;
    }

}
