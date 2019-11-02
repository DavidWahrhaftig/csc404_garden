using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBehaviour : StateMachineBehaviour
{

    public float speed = 1;
    public float rotationSpeed = 2;

    WitchLogic witchLogic;
    Transform targetPlayer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        witchLogic = animator.GetComponent<WitchLogic>();
        targetPlayer = witchLogic.getTargetPlayer();
        
        // play chasing sound
        witchLogic.playSound(witchLogic.chasingSound);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // continue chasing after player
        Vector3 targetPosition = new Vector3(targetPlayer.position.x, animator.transform.position.y, targetPlayer.position.z);
        Vector3 direction = animator.transform.position;
        direction.y = 0f;
        animator.transform.LookAt(targetPosition); //Quaternion.Slerp(animator.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
        animator.transform.position = Vector3.MoveTowards(animator.transform.position, targetPosition, speed * Time.deltaTime);

        if (targetPlayer.GetComponent<PlayerLogic>().isHidden()) // to indicate a player is hidden
        {
            targetPlayer.GetComponent<PlayerLogic>().stopChasingMe();
            witchLogic.playSound(witchLogic.complaningSound);
        }
    }
}
