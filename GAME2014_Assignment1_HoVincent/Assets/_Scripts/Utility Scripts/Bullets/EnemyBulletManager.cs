//------------EnemyBulletManger.cs--------------------
/* Name: Vincent Ho
 * Student Number: 101334300
 * 
 * Date Last Modified: October 24, 2021
 * 
 * Description: This script is used to grab bullets/arrows off of the queue, this is where any classes that need a bullet will grab an instance of
 * Revision History:
 * 1) Create all necessary singleton functions for the Bullet factory, set up instance, bullet adding
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy Bullet Manager class
/// </summary>
[System.Serializable]
public class EnemyBulletManager
{
    private static EnemyBulletManager instance = null;

    private EnemyBulletManager()
    {
        Initialize();
    }

    // public static instance to access Singleton
    public static EnemyBulletManager Instance()
    {
        if (instance == null)
        {
            instance = new EnemyBulletManager();
        }
        return instance;
    }

    // queue of bullets to pull from
    public Queue<GameObject> bulletPool;
    private void Initialize()
    {
        bulletPool = new Queue<GameObject>();
    }

    /// <summary>
    /// Add the bullet/arrow to the queue
    /// </summary>
    private void AddBullet()
    {
        var temp_bullet = EnemyBulletFactory.Instance().createBullet();
        bulletPool.Enqueue(temp_bullet);
    }

    /// <summary>
    /// Dequeues a bullet from the bullet pool, sets up initial functions such as its position, rotation, and direction of velocity
    /// </summary>
    /// <param name="spawnPosition"></param>
    /// <param name="rotation"></param>
    /// <param name="direction"></param>
    /// <returns></returns>
    public GameObject GetBullet(Vector2 spawnPosition, float rotation, Vector2 direction)
    {
        GameObject temp_bullet = null;

        if (bulletPool.Count < 1)
        {
            AddBullet();
        }

        temp_bullet = bulletPool.Dequeue();
        temp_bullet.transform.position = spawnPosition;
        temp_bullet.transform.rotation = Quaternion.AngleAxis(rotation + 180, Vector3.forward);
        temp_bullet.GetComponent<EnemyBullet>().UnitDirection = direction;
        temp_bullet.SetActive(true);
        return temp_bullet;
    }

    /// <summary>
    /// This method returns a bullet back into the bullet pool
    /// </summary>
    /// <param name="returnedBullet"></param>
    public void ReturnBullet(GameObject returnedBullet)
    {
        returnedBullet.SetActive(false);
        bulletPool.Enqueue(returnedBullet);
    }
}
