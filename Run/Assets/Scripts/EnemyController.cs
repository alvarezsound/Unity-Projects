using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float aggroRange = 10f;

    private float agentSpeed;
    private Transform player;

    private NavMeshAgent _navAgent;

    private Animator anim;

    private void Awake()
    {
            _navAgent = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();
            if (_navAgent != null) { agentSpeed = _navAgent.speed; }
            player = GameObject.FindGameObjectWithTag("Player").transform;

            InvokeRepeating("Tick", 0, 0.5f);
    }

    private void Update()
    {
        anim.SetFloat("Speed", _navAgent.velocity.magnitude);
    }

    void Tick()
    {
        if (GameController.instance.gamePlaying)
        {
            if (player != null && Vector3.Distance(transform.position, player.position) < aggroRange)
            {
                _navAgent.destination = player.position;
                _navAgent.speed = agentSpeed;
            }
        }
    }
}
