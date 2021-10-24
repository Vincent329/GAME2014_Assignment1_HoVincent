using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyBulletManager
{
    private static EnemyBulletManager instance = null;

    private EnemyBulletManager()
    {
        Initialize();
    }

    public static EnemyBulletManager Instance()
    {
        if (instance == null)
        {
            instance = new EnemyBulletManager();
        }
        return instance;
    }

    public Queue<GameObject> bulletPool;
    private void Initialize()
    {
        bulletPool = new Queue<GameObject>();
    }

    private void AddBullet()
    {
        var temp_bullet = EnemyBulletFactory.Instance().createBullet();
        bulletPool.Enqueue(temp_bullet);
    }

    public GameObject GetBullet(Vector2 spawnPosition, float rotation, Vector2 direction)
    {
        GameObject temp_bullet = null;

        if (bulletPool.Count < 1)
        {
            AddBullet();
        }

        temp_bullet = bulletPool.Dequeue();
        temp_bullet.transform.position = spawnPosition;
        temp_bullet.transform.rotation = Quaternion.AngleAxis(rotation + 180, Vector3.forward);
        temp_bullet.GetComponent<EnemyBullet>().UnitDirection = direction;
        temp_bullet.SetActive(true);
        return temp_bullet;
    }

    /// This method returns a bullet back into the bullet pool
    /// </summary>
    /// <param name="returnedBullet"></param>
    public void ReturnBullet(GameObject returnedBullet)
    {
        returnedBullet.SetActive(false);
        bulletPool.Enqueue(returnedBullet);
    }
}
