using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryBullet : Sounds
{
    public float speed = 20f;
    public int lifetime;
    public Rigidbody2D rb;
    public int damage = 1;
    public Hit WhoToHit = Hit.Enemy;
    public enum Hit
    {
        Enemy,
        Player
    }
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(WhoToHit == Hit.Enemy)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.GetComponent<EnemyHealth>().TakeDamage(damage);
                PlaySound(0, 0.7f, p1: 1f, p2: 1f, destroyed: true);
                Destroy(gameObject);
            }
        }
        else if(WhoToHit == Hit.Player)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.GetComponent<PlayerHealth>().TakeDamage(damage);
                PlaySound(0, 0.7f, p1: 1f, p2: 1f, destroyed: true);
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("whatIsWall"))
        {
            PlaySound(0, 0.7f, p1: 1f, p2: 1f, destroyed: true);
            Destroy(gameObject);
        }
    }

    void Update()
    {
        Destroy(gameObject, lifetime);
    }
}
