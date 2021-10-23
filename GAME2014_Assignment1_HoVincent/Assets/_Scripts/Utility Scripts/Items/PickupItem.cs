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
 * 2) Set up data values, now all you need is to right click in the Scriptable Objects folder, create an object, and set the values
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pickup data container class as a generic holder for pickups
/// </summary>
[CreateAssetMenu(fileName = "PickupItem", menuName = "Pickups/Pickup Item")]
public class PickupItem : ScriptableObject
{
    public ItemType itemType; 
    public string itemName = "Item";

    [TextArea]
    public string description = "Basic Item Description";

    public float healthValue = 0.0f;
    public float exciteValue = 0.0f;
    public int scoreValue = 0;

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
