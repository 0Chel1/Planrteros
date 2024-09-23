using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1AI : MonoBehaviour
{
    NavMeshAgent agent;
    public Rigidbody2D rb;
    public float CnockOutForce;
    public GameObject target;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        target = GameObject.FindGameObjectWithTag("Player");
        //rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(target != null)
        {
            agent.SetDestination(target.transform.position);
        }
        else if(target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            PlayerHealth plHealth = collision.transform.GetComponent<PlayerHealth>();
            plHealth.TakeDamage(1);
            rb.AddForce(-(target.transform.position - rb.transform.position).normalized * CnockOutForce, ForceMode2D.Impulse);
        }

        if (collision.transform.tag == "Sentry")
        {
            DefoultHealth strHealth = collision.transform.GetComponent<DefoultHealth>();
            strHealth.TakeDamage(1);
            rb.AddForce(-(target.transform.position - rb.transform.position).normalized * CnockOutForce, ForceMode2D.Impulse);
        }
    }
}
