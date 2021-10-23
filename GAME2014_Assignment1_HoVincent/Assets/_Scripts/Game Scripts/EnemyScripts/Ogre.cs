using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ogre will inherit from the enemy class
public class Ogre : Enemy
{
    [SerializeField]
    private Vector3 patrolPoint; // referenced from https://www.youtube.com/watch?v=4Wh22ynlLyk

    [SerializeField]
    private Boundaries patrolBounds;
    private Rigidbody2D rb;

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

    protected override void Action()  {
        Debug.Log("Ogre Action");
        base.Action(); 
        rb.velocity = GetDirection * Speed;
    }

    /// <summary>
    /// Using the trigger component of the Ogre, this is what we'll use to get the detection
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerBehaviour>() != null) // if the object in the trigger zone has a component type of PlayerBehaviour
        Detected = true;    // use the setter in the abstract class
    }

  

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


    }

    private void UpdatePatrolLocation()
    {
        patrolPoint = new Vector3(Random.Range(-patrolBounds.absX, patrolBounds.absX), Random.Range(-patrolBounds.absY, patrolBounds.absY), 0.0f);
    }
    
    /// <summary>
    /// testing returning an enemy
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerBehaviour>() != null) // if the colliding object has a component type of PlayerBehaviour
            EnemyManager.GetInstance().ReturnEnemy(gameObject);
    }
}
