//-----------PlayerBehaviour.cs------------
/* Name: Vincent Ho
 * Student Number: 101334300
 * 
 * Date Last Modified: October 23, 2021
 * 
 * Description: This script is used as a manager class for the player's behaviour
 * Handles all values such as movement, attacking, UI updating
 * 
 * Revision History:
 * 1) created script
 * 2) Created movement logic based on touch input
 * 3) Created UI attachments
 * 4) Created dash mechanic
 * 5) Created Attack Behaviours
 * 6) Update UI states based on items picked up
 * 7) Create Damage function that subtracts the player's health value
 * 8) Created function that pushes the player back and deals damage right after
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The Player Behaviour class
/// </summary>
public class PlayerBehaviour : MonoBehaviour
{
    // UI values
    [Header("UI Values")]
    [SerializeField] private float healthValue;
    [SerializeField] private float maxHealthValue;

    [SerializeField] private Slider healthSlider; // get slider values, a drag in for now, but may have to decouple
    [SerializeField] private Score scoreHandle;
    public Score ScoreHandle => scoreHandle; // accessor for the object with this Score component

    [SerializeField] private ExcitementBar excitementHandle; // the reference to the Excitement bar

    [Header("Touch Variables")]
    [SerializeField] Vector3 touchPos; // touchPosition continuously updates
    [SerializeField] private float radius; // Technically the radius of the circle collider
    [SerializeField] private float attackRadius; // Used for a distance check when attacking the enemy

    // booleans that trigger when the player is either in a moving state or an attacking state
    [SerializeField] private bool moveTrigger; 
    [SerializeField] private bool attackTrigger;

    // values dictating the speed of the player, both normal speed and dashing speed
    [Header("Movement Variables")]
    [Range(0.1f, 10.0f)]
    [SerializeField] private float speed;
    [Range(1.0f, 5.0f)]
    [SerializeField] private float dashMultiplier;
    private float originalSpeed;
    private float dashSpeed;

    // value of rotation represented as a float
    private float rotationAngle;

    // get physics data
    private CircleCollider2D circleCollider;
    private Rigidbody2D rb;
    private Animator anim;

    // Attack Abilities
    [SerializeField] private PlayerAttack attackHandle;
    [SerializeField] private Enemy targetEnemy;
    Touch fingerTouch;

    // distance between finger and avatar
    private Vector3 dragDist;    // dedicated vector for measuring the distance between the player and the touch input

    // game manager
    private MainMenuButtonManager manager;

    // Audio Source and array of potential clips to play
    private AudioSource audioSource;
    public AudioSource GetAudioSource => audioSource;
    [SerializeField] private AudioClip[] audioClipArray;
    public AudioClip[] GetAudioClipArray => audioClipArray;

    // Start is called before the first frame update
    void Start()
    {
        StaticScore.SetStaticScore = 0; // reset static score

        // get components necessary
        circleCollider = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        // Initialize all local components
        radius = circleCollider.radius + 0.5f;
        touchPos = Vector3.zero;
        moveTrigger = false;
        dragDist = Vector3.zero;
        healthValue = 100;
        maxHealthValue = healthValue;
        originalSpeed = speed;
        dashSpeed = speed * dashMultiplier;
        rotationAngle = 0;

        healthSlider.maxValue = maxHealthValue;
        
        // find necessary game objects
        scoreHandle = GameObject.FindObjectOfType<Score>();
        excitementHandle = GameObject.FindObjectOfType<ExcitementBar>();
        attackHandle = GameObject.FindObjectOfType<PlayerAttack>();
        manager = GameObject.FindObjectOfType<MainMenuButtonManager>();

        // starts as null
        targetEnemy = null;
    }

    // Update is called once per frame
    void Update()
    {
        // If there's an actual touch registered
       if (Input.touchCount > 0)
            TouchEvents();
        
    }


    /// <summary>
    /// Perpetually called by the update function to handle touch events
    /// </summary>
    private void TouchEvents()
    {
        // register a finger touch
        fingerTouch = Input.GetTouch(0);
        touchPos = Camera.main.ScreenToWorldPoint(fingerTouch.position);
        touchPos.z = 0.0f;

        // switch logic between movement and attacking depending on the position of touch from the player
        dragDist = touchPos - transform.position;

        // Once we have a finger registered
        if (fingerTouch.phase == TouchPhase.Began)
        {
            if (dragDist.sqrMagnitude <= radius * radius)
            {
                moveTrigger = true;
            }
            else
            {
                moveTrigger = false;
                DetectAttack(touchPos);
            }
        }

        // once we release the finger from the screen (unclick), slow down the plyaer and set the velocity;
        if (fingerTouch.phase == TouchPhase.Ended)
        {
            moveTrigger = false;
            dragDist = Vector3.zero;

            Deceleration();

            speed = originalSpeed;
            anim.SetFloat("Velocity", 0);
        }
    }

    /// <summary>
    /// Handle the movement of the player using fixed time
    /// </summary>
    private void FixedUpdate()
    {
        if (moveTrigger)
            MovePlayer(dragDist);
        else if (attackTrigger)
            Attack(targetEnemy.transform.position);        
    }

    /// <summary>
    /// Touch case where we check if the position we touch has na enemy or not
    /// </summary>
    /// <param name="detectTouchPos"></param>
    private void DetectAttack(Vector3 detectTouchPos)
    {
        RaycastHit2D ray = Physics2D.Raycast(detectTouchPos, -Vector2.zero); // utilizing the raycast hit functionality from the Unity Documentation https://docs.unity3d.com/ScriptReference/Physics2D.Raycast.html
        
        // if our ray has a collider
        if (ray.collider != null)
        {
            // if that collider has a type of Enemy
            if (ray.collider.GetComponent<Enemy>() != null)
            {
                // the attack state is true
                attackTrigger = true;
                targetEnemy = ray.collider.GetComponent<Enemy>(); // get the current enemy's reference
            }
            else
            {
                attackTrigger = false;
            }
        }
    }


    /// <summary>
    /// When Attack Trigger happens, linearly interpolate to the position of touch
    /// </summary>
    /// <param name="attackTouchPos"></param>
    private void Attack(Vector3 attackTouchPos)
    {
        ResetDrag(); 
        float attackRadiusSquared = attackRadius * attackRadius; // squared radius for checking the distance between player and enemy

        // get displacement and direction vectors between the position of touch and the current player position (attackTouchPos is the referenced enemy's position)
        Vector2 distToTarget = attackTouchPos - transform.position;
        Vector3 direction = distToTarget.normalized;

        // linearly interpolate the position of the player towards the position of the enemy
        transform.position = Vector3.Lerp(transform.position, attackTouchPos, Time.deltaTime * speed / 2);

        // rotate character to face the direction of travel
        rotationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
        transform.rotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward);

        // if the distance to he enemy is within the attack radius
        if (distToTarget.sqrMagnitude <= attackRadiusSquared)
        {
            // attack the enemy
            attackTouchPos = transform.position;
            anim.Play("SwordAnim", 0, 0); // play the sword animation, it has trigger events that will call the activation and deactivation of the hitbox
            audioSource.PlayOneShot(audioClipArray[1]); // Attack Swipe Sound effect plays

            // slows down the player and reset velocity
            Deceleration(); 
            rb.velocity = Vector2.zero; 

            // reset attack trigger state
            attackTrigger = false;
        }
    }

    /// <summary>
    /// logic to move the player on a set direction given the displacement between the finger dragging on the screen to the player
    /// </summary>
    /// <param name="distance"></param>
    private void MovePlayer(Vector3 distance)
    {
        float radiusSquared = radius * radius; // square the radius for optimization purposes
        if (dragDist.sqrMagnitude >= radiusSquared) // while keeping touch, when the player drags the finger outside of the player's collider
        {
            ResetDrag(); 
            Vector3 direction = distance.normalized;    // normalize the direction of the finger touch drag

            // Dashing logic, if the finger is far enough away from the player while it's moving
            if (distance.sqrMagnitude >= radiusSquared * 25) 
            {
                speed = dashSpeed;
            }
            // if finger doesn't reach that far, reset to the original speed
            else if (dragDist.sqrMagnitude < radiusSquared * 25) { 
                speed = originalSpeed;
            }
            
            rb.velocity = direction * speed; // set the velocity based on the direction of the finger multiplied by the speed value

            // https://forum.unity.com/threads/rotating-sprite-based-on-mouse-position.398478/
            rotationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
            transform.rotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward); // rotation value of z axis
        }
        else // if touch is close enough to player
        {
            Deceleration();
            speed = originalSpeed;
        }
        anim.SetFloat("Velocity", rb.velocity.magnitude);
    }

    /// <summary>
    /// introduce drag to slow down the player's velocity
    /// </summary>
    private void Deceleration() 
    {
        rb.drag = 10;
    }

    /// <summary>
    /// return to a frictionless state
    /// </summary> 
    private void ResetDrag() // return 
    {
        rb.drag = 0;
    }

    /// <summary>
    /// public function to alter the value level of the player's health after damage change
    /// </summary>
    /// <param name="changeValue"></param>
    public void HealthChange(float changeValue)
    {
        attackTrigger = false;
        Deceleration();

        healthValue -= changeValue;
        audioSource.PlayOneShot(audioClipArray[3]); // plays the damage sound clip

        if (healthValue <= 0.0f) // set the health to 0 to if it surpasses to negative
        {
            healthValue = 0.0f;
        }
        CheckDeath();
        healthSlider.value = healthValue; // update the health slider's value
    }

    /// <summary>
    /// called from the item class, it should increase values from a given item
    /// </summary>
    /// <param name="inItem"></param>
    public void ItemPickup(Item inItem)
    {
        switch (inItem.GetItemType)
        {
            // if the player picks up a health potion
            case (ItemType.HEALTH):
            {
                audioSource.PlayOneShot(audioClipArray[4]); // Health Potion Drink sound effect

                // increase the health value and make sure it doesn't surpass the maximum health value
                healthValue += inItem.HealthValue;
                if (healthValue >= maxHealthValue)
                    healthValue = maxHealthValue;

                healthSlider.value = healthValue; // update the health bar UI
                break;
            } 

            // if the player picks up an excitement potion
            case (ItemType.EXCITEMENT):
            {
                    audioSource.PlayOneShot(audioClipArray[5]); // Excitement Potion Drink soound effect
                    excitementHandle.GetComponent<Slider>().value += inItem.ExciteValue; // increase the value of the excitement slider
                break;
            }

            // if the player picks up a coin
            case (ItemType.SCORECOIN):
            {
                audioSource.PlayOneShot(audioClipArray[2]); // Coin Pickup sound effect

                // increase the score value and update the UI 
                scoreHandle.ScoreValue += inItem.ScoreValue;
                scoreHandle.UpdateScore();

                break;
            }
            default:
                break;
        }
    }


    /// <summary>
    /// Hitbox activates via animation trigger
    /// </summary>
    public void AttackStart()
    {
        attackHandle.ActivateHitBox();
    }

    /// <summary>
    /// hitbox deactivates via animation trigger
    /// </summary>
    public void AttackEnd()
    {
        attackHandle.DeactivateHitbox();

    }

    public void PushBack(Vector2 hitVector, float damageValue)
    {
        Vector2 directionVector = hitVector.normalized;
        rb.AddForce(directionVector * 10, ForceMode2D.Impulse);
      
        HealthChange(damageValue);
    }

    /// <summary>
    /// loads game over screen if the player's health drops to 0
    /// </summary>
    private void CheckDeath()
    {
        if (healthValue <= 0)
        {
            manager.loadGameOverScene();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

}
