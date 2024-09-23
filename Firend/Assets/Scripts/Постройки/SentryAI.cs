using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SentryAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Rigidbody2D rb;
    public Vector2 target;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        var player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform.position;
    }

    void Update()
    {
        agent.SetDestination(target);
    }
}
