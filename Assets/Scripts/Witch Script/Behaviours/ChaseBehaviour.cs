using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBehaviour : StateMachineBehaviour
{
    Transform target;
    public float speed = 1;
    public float rotationSpeed = 2;

    WitchLogic witchLogic;
    GameManager gameManager;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        witchLogic = animator.GetComponent<WitchLogic>();
        gameManager = witchLogic.gameManager;
        target = gameManager.getTargetPlayer();
        Debug.Log(target.tag);
        // play chasing sound
        witchLogic.playSound(witchLogic.chasingSound);
        //gameManager.playSound(gameManager.witchChasingSound);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // continue chasing after player
        Vector3 targetPosition = new Vector3(target.position.x, animator.transform.position.y, target.position.z);
        Vector3 direction = animator.transform.position;
        direction.y = 0f;
        animator.transform.LookAt(targetPosition); //Quaternion.Slerp(animator.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
        animator.transform.position = Vector3.MoveTowards(animator.transform.position, targetPosition, speed * Time.deltaTime);

        if (target.GetComponent<PlayerLogic>().getIsHidden()) // to indicate a player is hidden
        {
            //gameManager.setTargetPlayer(null);
            target.GetComponent<PlayerLogic>().stopChasingMe();
            animator.SetBool("isIdle", false);
            animator.SetBool("isChasing", false);
            animator.SetBool("isPatrolling", true);
            animator.SetBool("isCapturing", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //gameManager.setTargetPlayer(null);
    }
}
