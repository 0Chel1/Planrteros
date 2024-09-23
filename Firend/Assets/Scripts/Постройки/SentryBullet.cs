using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryBullet : MonoBehaviour
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
                Destroy(gameObject);
            }
        }
        else if(WhoToHit == Hit.Player)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.GetComponent<PlayerHealth>().TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("whatIsWall"))
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        Destroy(gameObject, lifetime);
    }
}
