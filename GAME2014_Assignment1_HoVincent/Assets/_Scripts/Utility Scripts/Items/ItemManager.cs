//-----------ItemManager.cs------------
/* Name: Vincent Ho
 * Student Number: 101334300
 * 
 * Date Last Modified: October 23, 2021
 * 
 * Description: This script is used as a manager class for item spawning
 * Follows the Manager and Factory algorithms used in class to spawn differing types of enemies
 * 
 * Revision History:
 * 1) created script
 * 2) Brought in initialization from the Enemy Manager class
 * 3) renamed variables to fit in with Item Manager context
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemManager : MonoBehaviour
{
    private static ItemManager instance = null;

    private ItemManager()
    {
        Initialize();
    }

    public static ItemManager GetInstance()
    {
        if (instance == null)
            instance = new ItemManager();
        return instance;
    }

    public List<Queue<GameObject>> itemPools;

    private void Initialize()
    {
        itemPools = new List<Queue<GameObject>>();

        // instantiate  new queue collections based on number of bullet types
        for (int count = 0; count < (int)ItemType.NUM_ITEM_TYPES; count++)
        {
            itemPools.Add(new Queue<GameObject>()); // remember that it's a LIST, not an ARRAY
        }

    }

    private void AddItem(ItemType itemType = ItemType.HEALTH)
    {
        var temp_Item = ItemFactory.Instance().createItem(itemType);
        itemPools[(int)itemType].Enqueue(temp_Item);
    }

    /// <summary>
    /// This method removes an enemy prefab from the enemy pool
    /// and returns a reference to it.
    /// </summary>
    /// <param name="spawnPosition"></param>
    /// <returns></returns>
    public GameObject GetItem(Vector2 spawnPosition, ItemType itemType = ItemType.HEALTH)
    {
        GameObject temp_Item = null;

        if (itemPools[(int)itemType].Count < 1)
        {
            AddItem(itemType);
        }
        // get the bullet from the queue
        temp_Item = itemPools[(int)itemType].Dequeue();

        temp_Item.transform.position = spawnPosition;
        temp_Item.SetActive(true);

        return temp_Item;
    }

    /// <summary>
    /// This method returns a bullet back into the bullet pool
    /// </summary>
    /// <param name="returnedBullet"></param>
    public void ReturnItem(GameObject returnedItem, ItemType itemType = ItemType.HEALTH)
    {
        returnedItem.SetActive(false);

        // depending on the type of the bullet, return it back to its respective bullet pool
        itemPools[(int)itemType].Enqueue(returnedItem);

    }
}
