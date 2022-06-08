using System.Collections;


using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public static Transform instance;

    private void Awake()
    {
        instance = this.transform;
    }

    [SerializeField] public float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    private Vector3 moveDirection;
    private Vector3 velocity;

    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;

    [SerializeField] private float jumpHeight;

    private CharacterController controller;
    private Animator animator;
    private PlayerStats stats;

    private void Start() 
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        stats = GetComponent<PlayerStats>();
    }

    private void Update() 
    {
        if(!stats.IsDead()){
        Move();

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }
        }
    }

    private void Move() 
    {

        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            //stops applying gravity when the player is grounded
            velocity.y = -2f;
        }

        float moveZ = Input.GetAxis("Vertical");
        moveDirection = new Vector3(0, 0, moveZ);
        moveDirection = transform.TransformDirection(moveDirection);

        if(isGrounded) 
        {
            if(moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
            {
                Walk();
            } 
            else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift)) 
            {
                Run();
            }
            else if(moveDirection == Vector3.zero)
            {
                Idle();
            }

            moveDirection *= moveSpeed;

            if(Input.GetKeyDown(KeyCode.Space)) 
            {
                Jump();
            }
        }
            controller.Move(moveDirection * Time.deltaTime);
            moveDirection *= walkSpeed;
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
    }

    private void Idle()
    {
        animator.SetFloat("Velocity", 0.01f, 0.05f, Time.deltaTime);
    }

    private void Walk()
    {
        moveSpeed = walkSpeed;
        animator.SetFloat("Velocity", 1, 0.05f, Time.deltaTime);
    }

    private void Run()
    {
        moveSpeed = runSpeed;
        animator.SetFloat("Velocity", 2, 0.05f, Time.deltaTime);
    }

    private void Jump()
    {
        animator.Play("Base Layer.Jump", 0, 0.05f);
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
    }

    private void Attack() 
    {
        animator.SetTrigger("Attack");
    }
    
}