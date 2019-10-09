using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehaviour : StateMachineBehaviour
{
    Vector3 newVector;
    GameManager gameManager;
    public float movingSeed;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        gameManager = animator.GetComponentInChildren<GameManagerReference>().gameManager;
        newVector = new Vector3(gameManager.getWitchBase().position.x, animator.transform.position.y, gameManager.getWitchBase().position.z);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("Idle");

        animator.transform.LookAt(newVector); //Quaternion.Slerp(animator.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
        animator.transform.position = Vector3.MoveTowards(animator.transform.position, newVector, movingSeed * Time.deltaTime);

        if ((Mathf.Abs(animator.transform.position.x - newVector.x) <= Mathf.Epsilon) && (Mathf.Abs(animator.transform.position.z - newVector.z) <= Mathf.Epsilon))
        {
            animator.SetBool("isIdle", false);
            animator.SetBool("isPatrolling", true);
            animator.SetBool("isChasing", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
