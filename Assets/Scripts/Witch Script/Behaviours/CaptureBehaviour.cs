using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureBehaviour : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public float captureSpeed = 2.5f;
    public float waitTime = 3;
    private Transform targetPlayer;
    private CaptureWait waitScript;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        
        targetPlayer = animator.GetComponent<GameManagerReference>().gameManager.getTargetPlayer();
        //targetPlayer = animator.GetComponent<SphereCast>().targetPlayer;
        waitScript = animator.GetComponent<CaptureWait>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //oscillator.isOscillating = false; // stop floating

        
        if (animator.transform.position.x != targetPlayer.position.x && animator.transform.position.z != targetPlayer.position.z)
        {
            centerOnPlayer(targetPlayer, animator);
            //oscillator.isOscillating = true; // float on the spot

            // pause for a little while before moving back to Idle state
            Debug.Log("before CoRouting");            
        }
        else
        {
            waitScript.DoCoroutine(waitTime);
        }
    }

    private void centerOnPlayer(Transform t, Animator animator)
    {
        /*
         * Moves this gameObject towards t's position
         */

        Vector3 targetPosition = new Vector3(t.position.x, animator.transform.position.y, t.position.z);
        animator.transform.position = Vector3.MoveTowards(animator.transform.position, targetPosition, captureSpeed * Time.deltaTime);

        // Note animator.transform pivot should be the same as the spotlight pivot

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}
}
