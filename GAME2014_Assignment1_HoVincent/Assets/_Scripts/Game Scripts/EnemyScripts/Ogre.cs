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
 * 6) retooled player detection algorithm
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
        if (Detected == true)
        {
            Action();
        } 
        else
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
        Vector2 position = patrolPoint - transform.position;
        GetDirection = position.normalized;

        RotationAngle = Mathf.Atan2(GetDirection.y, GetDirection.x) * Mathf.Rad2Deg + 90; // https://forum.unity.com/threads/rotating-sprite-based-on-mouse-position.398478/
        transform.rotation = Quaternion.AngleAxis(RotationAngle, Vector3.forward);

        
        rb.velocity = GetDirection * Speed;
        if (position.magnitude < 0.3f)
        {
            UpdatePatrolLocation();
        }

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
        if (player != null) // if the colliding object has a component type of PlayerBehaviour
        {
            Vector2 dist = player.transform.position - transform.position;
            player.PushBack(dist, DamageValue);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
