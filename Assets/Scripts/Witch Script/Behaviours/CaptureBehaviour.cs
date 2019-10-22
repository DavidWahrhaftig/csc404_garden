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
    GameManager gameManager;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        gameManager = FindObjectOfType<GameManager>();
        targetPlayer = gameManager.getTargetPlayer();
        gameManager.playSound(gameManager.witchLaughSound);
        //targetPlayer = animator.GetComponent<SphereCast>().targetPlayer;
        waitScript = animator.GetComponent<CaptureWait>();
        targetPlayer.GetComponent<PlayerController>().disableControls(); // make player disabled
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
            targetPlayer.GetComponent<PlayerController>().getAnimator().SetBool("isCaught", true);
            targetPlayer.GetComponent<PlayerController>().getAnimator().SetBool("isIdle", false);
            targetPlayer.GetComponent<PlayerController>().getAnimator().SetBool("isWalking", false);
            targetPlayer.GetComponent<PlayerController>().getAnimator().SetBool("isRunning", false);

        }

        // TODO: Decrament slowly player fruitcount 
        targetPlayer.GetComponent<PlayerLogic>().loseFruits();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        targetPlayer.GetComponent<PlayerController>().getAnimator().SetBool("isCaught", false);
        targetPlayer.GetComponent<PlayerController>().getAnimator().SetBool("isIdle", true);
        targetPlayer.GetComponent<PlayerLogic>().spawn();
        targetPlayer.GetComponent<PlayerController>().enableControls();
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

}
