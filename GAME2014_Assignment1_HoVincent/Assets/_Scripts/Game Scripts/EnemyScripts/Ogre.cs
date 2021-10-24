//-----------Ogre.cs------------
/* Name: Vincent Ho
 * Student Number: 101334300
 * 
 * Date Last Modified: October 23, 2021
 * 
 * Description: A subclass for the parent enemy class. Detailing behaviour of the ogre enemy type
 * 
 * Revision History:
 * 1) created script
 * 2) Created 2 colliders, 1 for collision detection 1 for player detection
 * 3) Made behaviour so that if the player enters the trigger radius, ogre detects player
 * 4) Created chase behaviour so that the ogre will always run towards the player
 * 5) Created Patrol point behaviour, place a random patrol point within the bounds of the level for ogre to go to if Player undetected
 * 6) retooled player detection algorithm to follow traditional radial check rather than a trigger check
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ogre will inherit from the enemy class
public class Ogre : Enemy
{
    [SerializeField] private Vector3 patrolPoint; // referenced from https://www.youtube.com/watch?v=4Wh22ynlLyk

    [SerializeField] private Boundaries patrolBounds;
    private Rigidbody2D rb;

    [Range(1.0f, 10.0f)]
    [SerializeField] private float radius;

    /// <summary>
    /// the moment the Ogre is dequeued and placed in the scene, detected is false
    /// </summary>
    private void OnEnable()
    {
        Detected = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindObjectOfType<PlayerBehaviour>();

        UpdatePatrolLocation();
    }

    // Update is called once per frame
    void Update()
    {
        // once detected, the Ogre pursues the player
        if (Detected == true)
        {
            Action();
        } 
        else // otherwise travels to a random point on screen
        {
            Patrolling();
        }
    }

    /// <summary>
    /// overriding the action function from the base enemy class
    /// </summary>
    protected override void Action()  {
        Debug.Log("Ogre Action");
        base.Action(); 
        rb.velocity = GetDirection * Speed;
    }

    /// <summary>
    /// Using the trigger component of the Ogre, this is what we'll use to get the detection
    /// </summary>
    /// <param name="collision"></param>
    private void Patrolling()
    {
        // get a displacement vector between patrol point and current position
        Vector2 distToPosition = patrolPoint - transform.position; 
        GetDirection = distToPosition.normalized;

        // same logic as the base action class
        RotationAngle = Mathf.Atan2(GetDirection.y, GetDirection.x) * Mathf.Rad2Deg + 90; // setting up to face the patrol point
        transform.rotation = Quaternion.AngleAxis(RotationAngle, Vector3.forward);

        // change velocity to head towards the patrol point
        rb.velocity = GetDirection * Speed;

        // if the Ogre has almost reached its position, update the patrol location
        if (distToPosition.magnitude < 0.3f)
        {
            UpdatePatrolLocation();
        }

        // while the player hasn't been detected, try to see if the player has breached the detection radius
        if (Detected != true)
        {
            DetectPlayer();
        }
    }

    /// <summary>
    /// Upon call, updates the patrol position for the ogre to travel to
    /// </summary>
    private void UpdatePatrolLocation()
    {
        // within the patrol bounds, set a random vector3 for the Ogre to go to
        patrolPoint = new Vector3(Random.Range(-patrolBounds.absX, patrolBounds.absX), Random.Range(-patrolBounds.absY, patrolBounds.absY), 0.0f);
    }

    /// <summary>
    /// takes the distance between the ogre and the player 
    /// </summary>
    private void DetectPlayer()
    {
        Vector2 distToPlayer = player.transform.position - transform.position;
        if (distToPlayer.sqrMagnitude <= radius * radius) // optimization: square magnitude checks faster than square root checks
        {
            Detected = true;
        }
    }
    
    /// <summary>
    /// testing returning an enemy
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerBehaviour player = collision.gameObject.GetComponent<PlayerBehaviour>();
        // if the colliding object has a component type of PlayerBehaviour
        if (player != null) 
        {
            // push the player away from the ogre
            Vector2 dist = player.transform.position - transform.position;
            player.PushBack(dist, DamageValue);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
