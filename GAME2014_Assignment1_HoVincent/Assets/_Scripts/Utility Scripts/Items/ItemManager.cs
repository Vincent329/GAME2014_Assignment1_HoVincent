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

/// <summary>
/// The Item Manager class
/// </summary>
[System.Serializable]
public class ItemManager : MonoBehaviour
{
    private static ItemManager instance = null;

    private ItemManager()
    {
        Initialize();
    }

    /// <summary>
    /// Get a static instance of the Item Manager for public access
    /// </summary>
    /// <returns></returns>
    public static ItemManager GetInstance()
    {
        if (instance == null)
            instance = new ItemManager();
        return instance;
    }

    // Like the Enemy Manager, create a list of queues for different item pools
    public List<Queue<GameObject>> itemPools;

    private void Initialize()
    {
        itemPools = new List<Queue<GameObject>>(); // set it aside in memory

        // instantiate  new queue collections based on number of item types
        for (int count = 0; count < (int)ItemType.NUM_ITEM_TYPES; count++)
        {
            itemPools.Add(new Queue<GameObject>()); // remember that it's a LIST, not an ARRAY
        }

    }

    /// <summary>
    /// Based on whatever item this is, place it in respective item pool
    /// </summary>
    /// <param name="itemType"></param>
    private void AddItem(ItemType itemType = ItemType.HEALTH)
    {
        var temp_Item = ItemFactory.Instance().createItem(itemType);
        itemPools[(int)itemType].Enqueue(temp_Item);
    }

    /// <summary>
    /// This method removes an item prefab from the item pool
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
        // get the item from the queue
        temp_Item = itemPools[(int)itemType].Dequeue();

        temp_Item.transform.position = spawnPosition;
        temp_Item.SetActive(true);

        return temp_Item;
    }

    /// <summary>
    /// This method returns a item back into its respective item pool
    /// </summary>
    /// <param name="returnedItem"></param>
    public void ReturnItem(GameObject returnedItem, ItemType itemType = ItemType.HEALTH)
    {
        returnedItem.SetActive(false);
        itemPools[(int)itemType].Enqueue(returnedItem);

    }
}
