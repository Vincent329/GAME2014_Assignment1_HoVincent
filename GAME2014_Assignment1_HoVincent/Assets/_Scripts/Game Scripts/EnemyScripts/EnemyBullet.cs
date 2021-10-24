using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private Vector2 unitDirection;

    private Rigidbody2D rb;

    // set the unit direction
    public Vector2 UnitDirection
    {
        get => unitDirection;
        set
        {
            unitDirection = value;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = unitDirection * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerBehaviour>())
        {
            PlayerBehaviour player = collision.gameObject.GetComponent<PlayerBehaviour>();
            player.HealthChange(damage);
        }
        EnemyBulletManager.Instance().ReturnBullet(gameObject);
        // return bullet
    }
}
