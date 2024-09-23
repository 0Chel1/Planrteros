using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public Enemy1AI AI;
    public SentryAI SentryAI;
    private GameObject collTarget;
    public float detectionDist = 5f;
    public TriggerFor currentTrigger = TriggerFor.Enemy;
    public enum TriggerFor
    {
        Enemy,
        Sentry,
        EnemySentry
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        collTarget = collision.gameObject;
        if(currentTrigger == TriggerFor.Enemy)
        {
            if (collision.transform.tag == "Sentry")
            {
                AI.target = collision.gameObject;
            }
        }
    }

    void Update()
    {
        if (currentTrigger == TriggerFor.Sentry)
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, detectionDist, LayerMask.GetMask("Enemy"));
            if(enemies.Length > 0)
            {
                SentryAI.agent.stoppingDistance = 0;
                Vector2 clossestEnemy = (Vector2)enemies[0].transform.position;
                float closestDist = Vector2.Distance(transform.position, clossestEnemy);

                foreach(Collider2D enemy in enemies)
                {
                    float dist = Vector2.Distance(transform.position, enemy.transform.position);
                    if(dist < closestDist)
                    {
                        closestDist = dist;
                        clossestEnemy = (Vector2)enemy.transform.position;
                    }
                }

                Vector2 currentPos = transform.position;
                Vector2 dir = (currentPos - clossestEnemy).normalized;
                SentryAI.target = (Vector2)transform.position + dir * detectionDist;

            }

            if(SentryAI.target == null)
            {
                var player = GameObject.FindGameObjectWithTag("Player");
                SentryAI.target = player.transform.position;
            }
        }
        else if (currentTrigger == TriggerFor.EnemySentry)
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, detectionDist, LayerMask.GetMask("Player"));
            if (enemies.Length > 0)
            {
                Vector2 clossestEnemy = (Vector2)enemies[0].transform.position;
                float closestDist = Vector2.Distance(transform.position, clossestEnemy);

                foreach (Collider2D enemy in enemies)
                {
                    float dist = Vector2.Distance(transform.position, enemy.transform.position);
                    if (dist < closestDist)
                    {
                        closestDist = dist;
                        clossestEnemy = (Vector2)enemy.transform.position;
                    }
                }

                Vector2 currentPos = transform.position;
                Vector2 dir = (currentPos - clossestEnemy).normalized;
                SentryAI.target = (Vector2)transform.position + dir * detectionDist;

            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (currentTrigger == TriggerFor.Sentry)
        {
            if (collision.transform.tag == "Player")
            {
                SentryAI.target = collision.transform.position;
                SentryAI.agent.stoppingDistance = 3;
            }
        }
    }
}
