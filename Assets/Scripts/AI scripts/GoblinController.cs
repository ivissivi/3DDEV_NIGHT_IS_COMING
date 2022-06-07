using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoblinController : MonoBehaviour
{
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
        animator.SetFloat("Velocity", 0.4f, 0.1f, Time.deltaTime);

        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

    }

    // Update is called once per frame
    void Update()
    {
        MoveToPlayer();
    }
}
