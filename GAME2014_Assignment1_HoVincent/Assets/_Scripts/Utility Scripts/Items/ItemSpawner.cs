//------------ItemSpawner.cs------------
/* Name: Vincent Ho
 * Student Number: 101334300
 * 
 * Date Last Modified: October 24, 2021
 * 
 * Description: This script is used to manage how often items spawn
 * Refer back to enemy spawner for implementation details, as logic is the exact same
 * 
 * Revision History:
 * 1) Followed framework from EnemySpawner
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Item Spawner class
/// </summary>
public class ItemSpawner : MonoBehaviour
{
    // similar logic to how the Enemy Spawner works, spawn rate multiplier, local spawn timer, boundaries, and maximum amount of items needed
    [SerializeField] private float spawnRate;
    [SerializeField] private float spawnTimer;
    [SerializeField] private Boundaries bounds;
    [SerializeField] private int maxItems;

    void Start()
    {
        spawnTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += (spawnRate * Time.deltaTime);
        if (spawnTimer >= 10)
        {

            float x = Random.Range(-bounds.absX, bounds.absX);
            float y = Random.Range(-bounds.absY, bounds.absY);
            Vector2 spawnPos = new Vector2(x, y);

            SpawnItem(spawnPos);

            spawnTimer = 0;
        }
    }

    private void SpawnItem(Vector2 spawnPos)
    {
        int itemCount = GameObject.FindObjectsOfType<Item>().Length + 1;
        if (itemCount <= maxItems)
        {
            // generate a random type
            var randomItem = Random.Range(0, (int)ItemType.NUM_ITEM_TYPES);
            ItemType itemType = (ItemType)randomItem;

            ItemManager.GetInstance().GetItem(spawnPos, itemType);
        }
        else
        {
            Debug.Log("Max amount of items on field");
        }
    }
}
