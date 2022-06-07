using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoblinController : MonoBehaviour
{
    [SerializeField] private float stoppingDistance = 1.2f;
    private NavMeshAgent agent = null;
    private Animator animator = null;
    [SerializeField] private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    private void MoveToPlayer()
    {
        agent.SetDestination(target.position);

        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

        if(distanceToPlayer >= agent.stoppingDistance) 
        {
            animator.SetFloat("Velocity", 0.5f, 0.1f, Time.deltaTime);
        }

        if(distanceToPlayer <= agent.stoppingDistance)
        {
            animator.SetFloat("Velocity", 0.1f);
        }
    }

    private void RotateToPlayer() 
    {
        //transform.LookAt(target);

        Vector3 direction = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = rotation;
    }

    // Update is called once per frame
    void Update()
    {
        MoveToPlayer();
    }
}
