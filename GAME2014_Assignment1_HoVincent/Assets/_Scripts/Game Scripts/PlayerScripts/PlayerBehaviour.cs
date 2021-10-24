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

 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    // UI values
    [Header("UI Values")]
    [SerializeField] private float healthValue;
    [SerializeField] private float maxHealthValue;

    [SerializeField] private Slider healthSlider; // get slider values, a drag in for now, but may have to decouple
    [SerializeField] private Score scoreHandle;
    [SerializeField] private ExcitementBar excitementHandle;

    [Header("Touch Variables")]
    [SerializeField] private float radius;
    [SerializeField] private float dashRadius;
    [SerializeField] private bool moveTrigger;
    [SerializeField] private bool attackTrigger;

    [Header("Movement Variables")]
    [Range(0.1f, 10.0f)]
    [SerializeField] private float speed;
    [Range(1.0f, 5.0f)]
    [SerializeField] private float dashMultiplier;
    private float originalSpeed;
    private float dashSpeed;
    private float rotationAngle;

    // get physics data
    private CircleCollider2D circleCollider;
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private PlayerAttack attackHandle;
    // dedicated vector for measuring the distance between the player and the touch input
    Touch fingerTouch;

    // distance between finger and avatar
    private Vector3 dragDist;

    // Start is called before the first frame update
    void Start()
    {
        // get components necessary
        circleCollider = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        radius = circleCollider.radius;
        moveTrigger = false;
        dragDist = Vector3.zero;
        healthValue = 100;
        maxHealthValue = healthValue;
        originalSpeed = speed;
        dashSpeed = speed * dashMultiplier;
        rotationAngle = 0;

        healthSlider.maxValue = maxHealthValue;
        
        scoreHandle = GameObject.FindObjectOfType<Score>();
        excitementHandle = GameObject.FindObjectOfType<ExcitementBar>();
        attackHandle = GameObject.FindObjectOfType<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        // touch differentiation in update?
       if (Input.touchCount > 0)
            TouchEvents();
        
    }

   
    /// <summary>
    /// Perpetually called by the update function to handle touch events
    /// </summary>
    private void TouchEvents()
    {
        fingerTouch = Input.GetTouch(0);
        Vector3 touchPos = Camera.main.ScreenToWorldPoint(fingerTouch.position);
        touchPos.z = 0.0f;

        // switch logic between movement and attacking depending on the position of touch from the player
        dragDist = touchPos - transform.position;

        if (fingerTouch.phase == TouchPhase.Began)
        {
            if (dragDist.sqrMagnitude <= radius * radius)
            {
                moveTrigger = true;
            }
            else
            {
                // Attack Logic
                Debug.Log("Attack here");
                moveTrigger = false;
                attackTrigger = true; 
                
                anim.Play("SwordAnim",0,0);
            }
        }

        if (fingerTouch.phase == TouchPhase.Ended)
        {
            moveTrigger = false;
            attackTrigger = false;
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
        if (attackTrigger)
        {
            // do something
        }

    }

    private void MovePlayer(Vector3 distance)
    {
        float radiusSquared = radius * radius;
        if (dragDist.sqrMagnitude >= radiusSquared)
        {
            ResetDrag();
            // normalize the direction of the drag
            Vector3 direction = distance.normalized;
            
            if (dragDist.sqrMagnitude >= radiusSquared*150)
            {
                speed = dashSpeed;
            }  else if (dragDist.sqrMagnitude <= radiusSquared * 25) { 
                speed = originalSpeed;
            }
            
            rb.velocity = direction * speed;
            // https://forum.unity.com/threads/rotating-sprite-based-on-mouse-position.398478/
            rotationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
            transform.rotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward);
        }
        else
        {
            Deceleration();
            speed = originalSpeed;
        }
        anim.SetFloat("Velocity", rb.velocity.magnitude);
    }
    private void Deceleration()
    {
        rb.drag = 10;
    }

    private void ResetDrag()
    {
        rb.drag = 0;
    }

    /// <summary>
    /// public function to alter the value level of the player's health after damage change
    /// </summary>
    /// <param name="changeValue"></param>
    public void HealthChange(float changeValue)
    {
        healthValue -= changeValue;
        if (healthValue <= 0.0f)
        {
            healthValue = 0.0f;
        }
        healthSlider.value = healthValue;
    }

    /// <summary>
    /// called from the item class, it should increase values from a given item
    /// </summary>
    /// <param name="inItem"></param>
    public void ItemChange(Item inItem)
    {
        switch (inItem.GetItemType)
        {
            case (ItemType.HEALTH):
            {
                healthValue += inItem.HealthValue;
                if (healthValue >= maxHealthValue)
                    healthValue = maxHealthValue;
                healthSlider.value = healthValue;
                break;
            } case (ItemType.EXCITEMENT):
            {
                excitementHandle.GetComponent<Slider>().value += inItem.ExciteValue;
                break;
            }
            case (ItemType.SCORECOIN):
            {
                scoreHandle.ScoreValue += inItem.ScoreValue;
                scoreHandle.UpdateScore();

                break;
            }
            default:
                break;
        }
    }

    public void AttackStart()
    {
        attackHandle.ActivateHitBox();
    }
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
}
