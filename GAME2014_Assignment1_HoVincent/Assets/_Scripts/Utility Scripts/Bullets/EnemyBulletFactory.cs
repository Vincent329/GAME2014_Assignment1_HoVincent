//------------ItemSpawner.cs------------
/* Name: Vincent Ho
 * Student Number: 101334300
 * 
 * Date Last Modified: October 24, 2021
 * 
 * Description: This script is used to manage how often items spawn
 * Revision History:
 * 1) Followed framework from EnemySpawner
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyBulletFactory
{
    private static EnemyBulletFactory instance = null;

    public GameObject enemyBullet;

    private EnemyBulletSpawner bulletSpawner;

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

    private void Initialize()
    {
        enemyBullet = Resources.Load("Prefabs/Bullet") as GameObject;
        bulletSpawner = GameObject.FindObjectOfType<EnemyBulletSpawner>();
    }

    public GameObject createBullet()
    {
        GameObject temp_bullet = null;

        temp_bullet = MonoBehaviour.Instantiate(enemyBullet);
        temp_bullet.transform.parent = bulletSpawner.gameObject.transform;
        temp_bullet.SetActive(false);

        return temp_bullet;
    }

}
