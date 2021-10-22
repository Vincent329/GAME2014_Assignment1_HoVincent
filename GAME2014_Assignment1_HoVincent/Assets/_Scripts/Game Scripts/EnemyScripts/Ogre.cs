using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ogre will inherit from the enemy class
public class Ogre : Enemy
{
    [SerializeField]
    private Transform[] patrolPoints; // referenced from https://www.youtube.com/watch?v=4Wh22ynlLyk
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindObjectOfType<PlayerBehaviour>();

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
        rb.velocity = GetDirection * Speed * Time.deltaTime;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerBehaviour>() != null) // if the object in the trigger zone has a component type of PlayerBehaviour
        Detected = true;    // use the setter in the abstract class
    }

    /// <summary>
    /// testing returning an enemy
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyManager.GetInstance().ReturnEnemy(gameObject);
    }

    private void Patrolling()
    {

    }

}
