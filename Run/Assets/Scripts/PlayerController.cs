using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent _navAgent;

    private GameController gameController;

    private Animator anim;

    void Awake()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        anim.SetFloat("Speed", _navAgent.velocity.magnitude);

        if (GameController.instance.gamePlaying)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, 1000, _navAgent.areaMask))
                {
                    _navAgent.SetDestination(hit.point);
                }

            }

        }      
    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collision");

        if (collision.gameObject.tag == "Enemy")
        {
            GameController.instance.EndGame();
        }
    }
}
