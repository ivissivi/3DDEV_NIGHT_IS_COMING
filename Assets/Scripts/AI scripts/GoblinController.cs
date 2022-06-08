using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoblinController : MonoBehaviour
{
    [SerializeField] private float stoppingDistance = 1.2f;
    private NavMeshAgent agent = null;
    private Animator animator = null;
    private GoblinStats stats = null;
    private Transform player;

    private float lastAttack = 0;
    private bool hasReachedPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        stats = GetComponent<GoblinStats>();
        player = ThirdPersonMovement.instance;
    }

    private void MoveToPlayer()
    {
        agent.SetDestination(player.position);

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if(distanceToPlayer >= agent.stoppingDistance) 
        {
            animator.SetFloat("Velocity", 0.5f, 0.1f, Time.deltaTime);
        }

        if(distanceToPlayer <= agent.stoppingDistance)
        {
            animator.SetFloat("Velocity", 0.1f);

            if(!hasReachedPlayer) //waits 1 second after reaching the player to attack
            {
                hasReachedPlayer = true;
                lastAttack = Time.time;
            }
            
            if(Time.time >= lastAttack + stats.attackSpeed) //waits for 1 second to do the attack again
            {
                lastAttack = Time.time;
                CharacterStats playerStats = player.GetComponent<CharacterStats>();
                AttackPlayer(playerStats);
            }
        } 
        else 
        {
            if(hasReachedPlayer)
            {
                hasReachedPlayer = false;
            }
        }
    }

    private void AttackPlayer(CharacterStats statsToDamage)
    {
        animator.SetTrigger("Attack");
        stats.DoDamage(statsToDamage);
    }

    private void RotateToPlayer() 
    {
        //transform.LookAt(player);

        Vector3 direction = player.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = rotation;
    }

    // Update is called once per frame
    void Update()
    {
        MoveToPlayer();
    }
}
