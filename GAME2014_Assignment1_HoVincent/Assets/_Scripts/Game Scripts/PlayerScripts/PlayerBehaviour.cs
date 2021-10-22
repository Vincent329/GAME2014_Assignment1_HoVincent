using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    // UI values
    [Header("Health Values")]
    [SerializeField] private float healthValue;
    [SerializeField] private float maxHealthValue;

    [SerializeField] private Slider healthSlider; // get slider values


    [Header("Touch Variables")]
    [SerializeField] private float radius;
    [SerializeField] private float dashRadius;
    [SerializeField] private bool moveTrigger;

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

        radius = circleCollider.radius;
        moveTrigger = false;
        dragDist = Vector3.zero;
        healthValue = 100;
        originalSpeed = speed;
        dashSpeed = speed * dashMultiplier;
        rotationAngle = 0;

        healthSlider.maxValue = healthValue;
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
                EnemyManager.GetInstance().GetEnemy(touchPos);
            }
        }

        if (fingerTouch.phase == TouchPhase.Ended)
        {
            moveTrigger = false;
            dragDist = Vector3.zero;

            Deceleration();

            speed = originalSpeed;
        }
    }

    /// <summary>
    /// Handle the movement of the player using fixed time
    /// </summary>
    private void FixedUpdate()
    {
        if (moveTrigger)
            MovePlayer(dragDist);
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
        healthSlider.value = healthValue;
    }

    public void ItemHealthChange(Item inItem)
    {
        switch (inItem.GetItemType)
        {
            case (ItemType.HEALTH):
            {
                healthValue += inItem.HealthValue;
                healthSlider.value = healthValue;
                break;
            } case (ItemType.EXCITEMENT):
            {
                //healthValue += inItem.HealthValue;
            break;
            }
            case (ItemType.SCORECOIN):
            {
                //healthValue += inItem.HealthValue;
                break;
            }
            default:
                break;
        }
    }

    public void PushBack()
    {

    }
}
