using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Point Value")]
    [SerializeField] private float pointValue;
    protected float PointValue => pointValue;

    [Range(0.1f, 10.0f)]
    [SerializeField] private float speed;
    protected float Speed
    {
        get => speed;
        set
        {
            speed = value;
        }
    }

    [SerializeField] private bool detected;
    protected bool Detected
    {
        get => detected;
        set
        {
            detected = value;
        }
    }

    [SerializeField] private float rotationAngle;
    protected float RotationAngle
    {
        get => rotationAngle;
        set
        {
            rotationAngle = value;
        }
            
    }

    [SerializeField] private Vector2 direction;
    protected Vector2 GetDirection
    {
        get => direction;
        set
        {
            direction = value;
        }
    }

    [SerializeField] public PlayerBehaviour player; // get a reference to the player
    //protected PlayerBehaviour Player
    //{
    //    get => player;
    //    set
    //    {
    //        Player = value;
    //    }
    //}

    // when the enemy is first enabled, Fade in
    private void OnEnable()
    {
        
    
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerBehaviour>();

        detected = false;
        RotationAngle = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Every subclass is going to have its own iteration of the function action
    /// </summary>
    protected virtual void Action()
    {
        Vector2 position = player.transform.position - transform.position;
        direction = position.normalized;

        rotationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90; // https://forum.unity.com/threads/rotating-sprite-based-on-mouse-position.398478/
        transform.rotation = Quaternion.AngleAxis(RotationAngle, Vector3.forward);
    }    
}
