 //------------Bounce.cs------------
 /* Name: Vincent Ho
  * Student Number: 101334300
  * 
  * Date Last Modified: October 24, 2021
  * 
  * Description: A subclass of the enemy class, this script details behaviours for the bounce type enemy
  * Revision History:
  * 1) Attached a Rigid Body 2D
  * 2) Velocity will be a random Vector3 multiplied by speed
  * 3) Bounce will happen when the bounce enemy hits all wall, which will have a bounce material (Learned from Game Physics Winter 2021 course)
  */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Bounce Enemy class that inherits from an Enemy superclass
/// </summary>
public class Bounce : Enemy
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // randomize a velocity upon startup
        rb.velocity = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0.0f) * Speed;
    }

    protected override void Action()
    {

    }

    /// <summary>
    /// should the Bounce enemy hit the player, it wwill damage the player and be returned to the enemy pool
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerBehaviour>() != null)
        {
            PlayerBehaviour player = collision.gameObject.GetComponent<PlayerBehaviour>();

            player.HealthChange(DamageValue);
            EnemyManager.GetInstance().ReturnEnemy(gameObject);

        }
    }
}
