using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Enemy
{
    [SerializeField] private float attackTimer;
    [SerializeField] private float attackTrigger;
    [SerializeField] private Transform bulletSpawnPos;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        Detected = true; 
        player = GameObject.FindObjectOfType<PlayerBehaviour>();
        audioSource = GetComponent<AudioSource>();
        attackTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Detected == true)
        {
            Action();
        }
    }

    protected override void Action()
    {
        base.Action();
        attackTimer += Time.deltaTime;
        if (attackTimer > attackTrigger)
        {
            ShootBullet();
            attackTimer = 0;
        }    
    }

    private void ShootBullet()
    {
        EnemyBulletManager.Instance().GetBullet(bulletSpawnPos.position, RotationAngle, GetDirection);
        audioSource.PlayOneShot(audioSource.clip);
    }
}
