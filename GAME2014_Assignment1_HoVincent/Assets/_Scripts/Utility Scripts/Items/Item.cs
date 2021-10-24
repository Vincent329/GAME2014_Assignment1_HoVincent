//-----------Item.cs------------
/* Name: Vincent Ho
 * Student Number: 101334300
 * 
 * Date Last Modified: October 21, 2021
 * 
 * Description: This script is used as a generic script for items via ScriptableObject Use
 * Attach a ScriptableObject to the component and fill this prefab's data with the data from the Scriptable object
 * 
 * Revision History:
 * 1) created script
 * 2) with a passed in scriptable object, data for this specific item shall be filled in
 * 3) filled OnTriggerEnter2D data
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Item class
/// </summary>
public class Item : MonoBehaviour
{
    [SerializeField]
    private PickupItem pickup; // SCRIPTABLE OBJECT, place a ScriptableObject of PickupItem to fill in data
    [SerializeField]
    private SpriteRenderer itemSprite;

    private ItemType itemType; // enumeration for specific item

    // getter for the item's type
    public ItemType GetItemType => itemType;

    // different data values that will be filled in based on what was in the scriptable object
    private float healthValue;
    private float exciteValue;
    private int scoreValue;
    
    /// <summary>
    /// getters for the item's values
    /// </summary>
    public float HealthValue => healthValue;
    public float ExciteValue => exciteValue;
    public int ScoreValue => scoreValue;

    /// <summary>
    /// In Start, fill in local item variable data with data from the Scriptable Object
    /// </summary>
    private void Start()
    {
        // if we have a scriptable object attached, fill in the values as dictated
        if (pickup != null)
        {
            itemSprite = GetComponent<SpriteRenderer>();
            itemSprite.sprite = pickup.sprite;
            itemType = pickup.itemType;
            healthValue = pickup.healthValue;
            exciteValue = pickup.exciteValue;
            scoreValue = pickup.scoreValue;
        }
    }

    /// <summary>
    /// Collision logic, should player interact with the item, 
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if it's a player that has collided with this object
        PlayerBehaviour test = collision.gameObject.GetComponent<PlayerBehaviour>();
        if (test != null)
        {
            // call the player's ItemPickup function that will alter the player's stats + UI based on the data in the item class
            // then return the item to the queue
            test.ItemPickup(gameObject.GetComponent<Item>());
            ItemManager.GetInstance().ReturnItem(gameObject);
        }
    }
}
