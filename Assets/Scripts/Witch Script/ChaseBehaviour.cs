using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBehaviour : StateMachineBehaviour
{
    Transform target;
    public float speed = 1;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // disable oscillation 
        // only approach target if spherecast hasn't collided since spherecast controls the player movement, or can change

        Vector3 targetPosition = new Vector3(target.position.x, animator.transform.position.y, target.position.z);
        animator.transform.position = Vector3.MoveTowards(animator.transform.position, targetPosition, speed * Time.deltaTime); // Vector3.MoveTowards(transform.position, targetPosition, step);


        //Debug.Log("--> Chasing");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("isChasing", false);
        }

        

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
