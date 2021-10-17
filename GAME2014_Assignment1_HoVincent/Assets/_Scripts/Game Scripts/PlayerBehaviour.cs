using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private float dashRadius;
    [SerializeField] private bool moveTrigger;

    [Range(0.1f, 10.0f)]
    [SerializeField] private float speed;

    // get physics data
    private CircleCollider2D circleCollider;
    private Rigidbody2D rb;

    // dedicated vector for measuring the distance between the player and the touch input
    Touch fingerTouch;
    private Vector3 dragDist;

    // Start is called before the first frame update
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        radius = circleCollider.radius;
        moveTrigger = false;
        dragDist = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        TouchEvents();
    }

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
                Debug.Log("Within Bounds");
                moveTrigger = true;
            }
            else
            {
                // Attack Logic
            }
        }
        if (fingerTouch.phase == TouchPhase.Ended)
        {
            moveTrigger = false;
            dragDist = Vector3.zero;
            rb.velocity = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        if (moveTrigger)
            MovePlayer(dragDist);
    }

    private void MovePlayer(Vector3 distance)
    {
        // normalize the direction of the drag
        if (dragDist.sqrMagnitude >= radius * radius)
        {
            Vector3 direction = distance.normalized;
            rb.velocity = direction * speed;
        } else
        {
            rb.velocity = Vector2.zero;
        }

    }
}
