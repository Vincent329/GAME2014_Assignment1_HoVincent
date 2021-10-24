//-----------PlayerBehaviour.cs------------
/* Name: Vincent Ho
 * Student Number: 101334300
 * 
 * Date Last Modified: October 23, 2021
 * 
 * Description: This script is used as a manager class for the player's attack characteristics
 * Attached to a game object that's a child of the player, this game object holds a collider that activates and deactivates 
 * 
 * Revision History:
 * 1) created script
 * 2)  
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Player Attack class
/// </summary>
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private CircleCollider2D attackHitbox; // reference to the attack collider
    [SerializeField] private PlayerBehaviour player; // reference to the player

    // Get data initialized
    void Start()
    {
        // should this actually be a child of an object
        if (transform.parent != null)
        {
            player = transform.parent.GetComponent<PlayerBehaviour>(); // gets the player behaviour of the parent game object
        }

        // gets the component of the attack hitbox and switches it off
        attackHitbox = GetComponent<CircleCollider2D>();
        attackHitbox.enabled = false;
    }

    // turn on the circle collider 
    public void ActivateHitBox()
    {
        attackHitbox.enabled = true;
    }

    // turn off the circle collider
    public void DeactivateHitbox()
    {
        attackHitbox.enabled = false;
    }

    /// <summary>
    /// Should the circle collider be active, check if the collider overlaps any enemies, and returns the enemy to the enemy pool if that's true
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {

        // if an enemy lies within the hitbox
        if (collision.gameObject.GetComponent<Enemy>() != null)
        {
            player.ScoreHandle.ScoreValue += collision.gameObject.GetComponent<Enemy>().PointValue; // Adds to the score UI
            player.ScoreHandle.UpdateScore();                                                       // update the UI 
            player.GetAudioSource.PlayOneShot(player.GetAudioClipArray[0]);                         // plays the sword contact sound clip
            EnemyManager.GetInstance().ReturnEnemy(collision.gameObject);                           // returns the enemy to the enemy pool
        }
    }
}
