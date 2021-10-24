using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : Enemy
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0.0f) * Speed;
    }

    protected override void Action()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerBehaviour>() != null)
        {
            PlayerBehaviour player = collision.gameObject.GetComponent<PlayerBehaviour>();

            player.HealthChange(DamageValue);
            EnemyManager.GetInstance().ReturnEnemy(gameObject);

        }
    }
}
