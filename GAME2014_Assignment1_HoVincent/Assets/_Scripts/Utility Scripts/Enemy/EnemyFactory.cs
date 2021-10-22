//-----------EnemyFactory.cs------------
/* Name: Vincent Ho
 * Student Number: 101334300
 * 
 * Date Last Modified: October 18, 2021
 * 
 * Description: This script is used as a factory class for enemy spawning
 * Follows the Manager and Factory algorithms used in class to spawn differing types of enemies
 * Given a blueprint of an enemy prefab, and then use it to create a pool for enemies on the scene
 * 
 * Revision History:
 * 1) created script
 * 2) setting it up as a singleton
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory 
{
    private static EnemyFactory instance = null;

    // reference to enemy prefabs
    [SerializeField]
    private GameObject ogreEnemy;
    private GameObject turretEnemy;
    private GameObject bounceEnemy;

    private EnemySpawner manager;

    // private constructor for the enemy factory
    private EnemyFactory()
    {
        Initialize();
    }

    // return a static instance of the enemy factory so that it can be called anywhere
    public static EnemyFactory Instance()
    {
        if (instance == null)
        {
            instance = new EnemyFactory();
        }
        return instance;
    }

    /// <summary>
    /// Goes through the Resources folder and loads the enemies as their own respective game objects
    /// </summary>
    private void Initialize()
    {
        ogreEnemy = Resources.Load("Prefabs/Ogre") as GameObject;
        turretEnemy = Resources.Load("Prefabs/Turret") as GameObject;
        bounceEnemy = Resources.Load("Prefabs/Bounce") as GameObject;

        manager = GameObject.FindObjectOfType<EnemySpawner>();
    }

    public GameObject createEnemy(EnemyType enemyType = EnemyType.OGRE)
    {
        GameObject tempEnemy = null;
        switch (enemyType)
        {

            // being very explicit with the instantiate function
            case EnemyType.OGRE:
                tempEnemy = MonoBehaviour.Instantiate(ogreEnemy);
                break;
            case EnemyType.TURRET:
                tempEnemy = MonoBehaviour.Instantiate(turretEnemy);
                break;
            case EnemyType.BOUNCE:
                tempEnemy = MonoBehaviour.Instantiate(bounceEnemy);
                break;
        }
        // get the parent transform of the bullet, be the game controller's game object transform
        tempEnemy.transform.parent = manager.gameObject.transform;
        tempEnemy.SetActive(false);

        return tempEnemy;
    }
}

