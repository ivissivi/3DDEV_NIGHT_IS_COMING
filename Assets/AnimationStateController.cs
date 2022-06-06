using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
    }

    // Update is called once per frame
    void Update()
    {
        bool forwardPressed = Input.GetKey("w");
        bool leftPressed = Input.GetKey("a");
        bool backwardPressed = Input.GetKey("s");
        bool rightPressed = Input.GetKey("d");
        bool runPressed = Input.GetKey("left shift");
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);

        if ((!isRunning && forwardPressed) || (!isRunning && leftPressed) || (!isRunning && rightPressed) || (!isRunning && backwardPressed)) 
        {
            animator.SetBool(isWalkingHash, true);
        }

        if ((isRunning && !forwardPressed) || (isRunning && !leftPressed) || (isRunning && !rightPressed) || (isRunning && !backwardPressed)) 
        {
            animator.SetBool(isWalkingHash, false);
        }

        if ((!isRunning && (forwardPressed && runPressed)) || (!isRunning && (leftPressed && runPressed)) || (!isRunning && (rightPressed && runPressed)) || (!isRunning && (backwardPressed && runPressed))) 
        {
            animator.SetBool(isRunningHash, true);
        }

        if ((isRunning && (!isRunning || !forwardPressed)) || (isRunning && (!isRunning || !leftPressed)) || (isRunning && (!isRunning || !rightPressed)) || (isRunning && (!isRunning || !backwardPressed))) 
        {
            animator.SetBool(isRunningHash, false);
        }
    }
}
