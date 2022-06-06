using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoblinController : MonoBehaviour
{
    private NavMeshAgent agent = null;
    [SerializeField] private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void MoveToPlayer()
    {
        agent.SetDestination(target.position);
    }

    // Update is called once per frame
    void Update()
    {
        MoveToPlayer();
    }
}
