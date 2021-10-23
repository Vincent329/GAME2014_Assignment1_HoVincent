
//-----------ItemFactory.cs------------
/* Name: Vincent Ho
 * Student Number: 101334300
 * 
 * Date Last Modified: October 23, 2021
 * 
 * Description: This script is used as a factory class for item spawning
 * Follows the Manager and Factory algorithms used in class to spawn differing types of enemies
 * Given a blueprint of an item prefab, and then use it to create a pool for enemies on the scene
 * 
 * Revision History:
 * 1) created script
 * 2) setting it up as a singleton
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : MonoBehaviour
{
    private static ItemFactory instance = null;

    // reference to enemy prefabs
    [SerializeField]
    private GameObject healthPotion;
    private GameObject excitePotion;
    private GameObject scoreCoin;

    private ItemSpawner manager;

    // private constructor for the enemy factory
    private ItemFactory()
    {
        Initialize();
    }

    // return a static instance of the enemy factory so that it can be called anywhere
    public static ItemFactory Instance()
    {
        if (instance == null)
        {
            instance = new ItemFactory();
        }
        return instance;
    }

    /// <summary>
    /// Goes through the Resources folder and loads the enemies as their own respective game objects
    /// </summary>
    private void Initialize()
    {
        healthPotion = Resources.Load("Prefabs/Items/HealthPotion") as GameObject;
        excitePotion = Resources.Load("Prefabs/Items/ExcitePotion") as GameObject;
        scoreCoin = Resources.Load("Prefabs/Items/Coin") as GameObject;

        manager = GameObject.FindObjectOfType<ItemSpawner>();
    }

    public GameObject createItem(ItemType itemType = ItemType.HEALTH)
    {
        GameObject tempItem = null;
        switch (itemType)
        {

            // being very explicit with the instantiate function
            case ItemType.HEALTH:
                tempItem = MonoBehaviour.Instantiate(healthPotion);
                break;
            case ItemType.EXCITEMENT:
                tempItem = MonoBehaviour.Instantiate(excitePotion);
                break;
            case ItemType.SCORECOIN:
                tempItem = MonoBehaviour.Instantiate(scoreCoin);
                break;
        }
        // get the parent transform of the bullet, be the game controller's game object transform
        tempItem.transform.parent = manager.gameObject.transform;
        tempItem.SetActive(false);

        return tempItem;
    }
}
