using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private CircleCollider2D attackHitbox;
    [SerializeField] private PlayerBehaviour player;
    void Start()
    {
        if (transform.parent != null)
        {
            player = transform.parent.GetComponent<PlayerBehaviour>();
        }
        attackHitbox = GetComponent<CircleCollider2D>();
        attackHitbox.enabled = false;
    }

    public void ActivateHitBox()
    {
        attackHitbox.enabled = true;
    }

    public void DeactivateHitbox()
    {
        attackHitbox.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>() != null)
        {
            player.ScoreHandle.ScoreValue += collision.gameObject.GetComponent<Enemy>().PointValue;
            player.ScoreHandle.UpdateScore();

            EnemyManager.GetInstance().ReturnEnemy(collision.gameObject);
        }
    }
}
