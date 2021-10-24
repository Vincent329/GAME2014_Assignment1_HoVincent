//------------EnemyBullet.cs------------
/* Name: Vincent Ho
 * Student Number: 101334300
 * 
 * Date Last Modified: October 23, 2021
 * 
 * Description: Characteristic script for the enemy bullet/arrows
 * Revision History:
 * 1) Created the script
 * 2) Set up default values such as damage, speed, path of direction
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Enemy Bullet Class
/// </summary>
public class EnemyBullet : MonoBehaviour
{
    // variable data for characteristics
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private Vector2 unitDirection;

    private Rigidbody2D rb;

    // get and set the unit direction
    public Vector2 UnitDirection
    {
        get => unitDirection;
        set
        {
            unitDirection = value;
        }
    }


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = unitDirection * speed; // the velocity of the bullet/arrow is calculated with its unit direction and speed
    }

    /// <summary>
    /// Logic for when the bullet/arrow hits something
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if the arrow hits the player
        if (collision.gameObject.GetComponent<PlayerBehaviour>())
        {
            PlayerBehaviour player = collision.gameObject.GetComponent<PlayerBehaviour>();
            player.HealthChange(damage); // deal damage to the player wrt the bullet/arrow's damage value
        }
        
        // return the projectile to the bullet pool
        EnemyBulletManager.Instance().ReturnBullet(gameObject);
        
    }
}
