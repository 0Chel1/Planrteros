using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlyingEnemy : MonoBehaviour
{
    NavMeshAgent agent;
    public Rigidbody2D rb;
    public GameObject target;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.transform.position);
        }
        else if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
    }
}
