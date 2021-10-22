//-----------PickupItem.cs------------
/* Name: Vincent Ho
 * Student Number: 101334300
 * 
 * Date Last Modified: October 21, 2021
 * 
 * Description: This script is used as a container for item pickups
 * Prefabs with this attached will have basic data to it
 * 
 * Revision History:
 * 1) created script
 * 2) setting it up as a singleton
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pickup data container class as a generic hodler for pickups
/// </summary>
[CreateAssetMenu(fileName = "PickupItem", menuName = "Pickups/Pickup Item")]
public class PickupItem : ScriptableObject
{
    public ItemType itemType;
    public string itemName = "Item";

    [TextArea]
    public string description = "Basic Item Description";

    public float healthValue = 0.0f;
    public float timerValue = 0.0f;
    public float scoreValue = 0.0f;

    public Sprite sprite = null;

    /// <summary>
    /// different use depending on the type of item this is
    /// </summary>
    public void Consume()
    {
        Debug.Log("Consume Item");
        // put a switch case in here?
    }
}
