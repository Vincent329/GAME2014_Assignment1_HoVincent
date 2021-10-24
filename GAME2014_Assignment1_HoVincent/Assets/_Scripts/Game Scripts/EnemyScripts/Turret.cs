//-----------Ogre.cs------------
/* Name: Vincent Ho
 * Student Number: 101334300
 * 
 * Date Last Modified: October 23, 2021
 * 
 * Description: A subclass for the parent enemy class. Detailing behaviour of the turret enemy type
 * 
 * Revision History:
 * 1) created script
 * 2) Created a local timer that increments, if the timer surpasses the the trigger threshold (passes a set amount of time), launch a bullet from bullet pool then reset timer
 * 3) Make a sound effect play on launch
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// the Turret class that inherits form enemy
/// </summary>
public class Turret : Enemy
{
    // set up variables for attacking, and the transform in which the arrow will spawn from
    [SerializeField] private float attackTimer;
    [SerializeField] private float attackTrigger;
    [SerializeField] private Transform bulletSpawnPos;

    // audio source to play the arrow launch sound
    private AudioSource audioSource; 

    void Start()
    {
        // initialize variables on startup
        Detected = true; 
        player = GameObject.FindObjectOfType<PlayerBehaviour>();
        audioSource = GetComponent<AudioSource>();
        attackTimer = 0;
    }

    // Turret will always detect the enemy
    void Update()
    {
        if (Detected == true)
        {
            Action();
        }
    }

    /// <summary>
    /// Overrided Action function for bullet shooting
    /// </summary>
    protected override void Action()
    {
        base.Action(); // base action makes enemy face twoards player
        attackTimer += Time.deltaTime;
        if (attackTimer > attackTrigger) // timer increments, and if it surpasses the trigger thershold
        {
            // shoot the bullet and reset timer
            ShootBullet();
            attackTimer = 0;
        }    
    }

    /// <summary>
    /// Local shoot bullet function that takes a bullet from the queue and activates it, and plays the sound effect
    /// </summary>
    private void ShootBullet()
    {
        EnemyBulletManager.Instance().GetBullet(bulletSpawnPos.position, RotationAngle, GetDirection);
        audioSource.PlayOneShot(audioSource.clip);
    }
}
