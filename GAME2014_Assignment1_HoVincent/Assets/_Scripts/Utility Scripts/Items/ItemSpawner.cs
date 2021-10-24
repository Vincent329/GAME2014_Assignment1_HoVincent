using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float spawnRate;
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
            Debug.Log("SpawnItem");

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
