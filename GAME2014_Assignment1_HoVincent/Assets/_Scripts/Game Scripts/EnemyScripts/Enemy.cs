//------------Enemy.cs------------
/* Name: Vincent Ho
 * Student Number: 101334300
 * 
 * Date Last Modified: October 21, 2021
 * 
 * Description: The main enemy class, which has data shared across all of its child classes
 * Revision History:
 * 1) Created the script
 * 2) provide the values that are shared among the enemy types such as points, damage values, and speed
 * 3) Set up the action function to be overrideable for different use cases
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Value")]
    [SerializeField] private int pointValue;
    public int PointValue => pointValue;

    // damage values to inflict on a player upon contact (Ogre and Bounce use)
    [SerializeField] private float damageValue;
    protected float DamageValue => damageValue;

    // the speed of the enemy type
    [Range(0.1f, 10.0f)]
    [SerializeField] private float speed;
    protected float Speed
    {
        get => speed;
        set
        {
            speed = value;
        }
    }

    // boolean to check if the enemy has been detected, used only for Ogre and Turret
    [SerializeField] private bool detected;
    protected bool Detected
    {
        get => detected;
        set
        {
            detected = value;
        }
    }

    // float representation of the rotation angle along the z axis, used for enemy to face towards the player
    [SerializeField] private float rotationAngle;
    protected float RotationAngle
    {
        get => rotationAngle;
        set
        {
            rotationAngle = value;
        }
            
    }

    // the direction vector of the enemy in question
    [SerializeField] private Vector2 direction;
    protected Vector2 GetDirection
    {
        get => direction;
        set
        {
            direction = value;
        }
    }

    [SerializeField] public PlayerBehaviour player; // get a reference to the player

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerBehaviour>();

        // initialize values
        detected = false;
        RotationAngle = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Every subclass is going to have its own iteration of the function action, but the basic functionality is for the enemy to face the player
    /// </summary>
    protected virtual void Action()
    {
        Vector2 position = player.transform.position - transform.position;
        direction = position.normalized;

        // representation in degrees for the rotation value the enemy needs in order to face the player
        rotationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90; // follows same logic from the player to face the mouse button
        transform.rotation = Quaternion.AngleAxis(RotationAngle, Vector3.forward); // change the direction around the z axis. (Vector3.forward represents (0,0,1)
    }    
}
