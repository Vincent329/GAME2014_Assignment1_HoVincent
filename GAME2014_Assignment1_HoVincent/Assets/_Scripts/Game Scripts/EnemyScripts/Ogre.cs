using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ogre will inherit from the enemy class
public class Ogre : Enemy
{

    private Transform[] patrolPoints;
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
    }

    protected override void Action()  {
        Debug.Log("Ogre Action");
        base.Action();
        rb.velocity = GetDirection * Speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerBehaviour>() != null)
        Detected = true;    // use the setter in the abstarct class
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyManager.GetInstance().ReturnEnemy(gameObject);
    }
}
