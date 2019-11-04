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

    WitchLogic witchLogic;
    GameManager gameManager;

    public float fruitDropTime = 0.75f;

    private float dropTimer;
    public bool canPlayerResist = false;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        waitScript = animator.GetComponent<CaptureWait>();

        witchLogic = animator.GetComponent<WitchLogic>();
        targetPlayer = witchLogic.getTargetPlayer();
        targetPlayer.GetComponent<PlayerLogic>().gotCaught(); // make player disabled
        witchLogic.playSound(witchLogic.laughSound);


        dropTimer = fruitDropTime;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        if (animator.transform.position.x != targetPlayer.position.x && animator.transform.position.z != targetPlayer.position.z)
        {
            centerOnPlayer(targetPlayer, animator);      
        }
        else
        {
            if (!canPlayerResist)
            {
                targetPlayer.GetComponent<PlayerLogic>().playSound(targetPlayer.GetComponent<PlayerLogic>().caughtSound);
                targetPlayer.GetComponent<Animator>().SetBool("isCaught", true);
                targetPlayer.GetComponent<Animator>().SetBool("isIdle", false);
                targetPlayer.GetComponent<Animator>().SetBool("isWalking", false);
                targetPlayer.GetComponent<Animator>().SetBool("isRunning", false);
            }

            canPlayerResist = true;
            
        }

        // TODO: Decrament player fruitcount slowly
        // targetPlayer.caught = true;  // so player cannot 

        if (canPlayerResist)
        {
            // start decremeanting fruits slowly (1 every 0.75 seconds)
            dropTimer -= Time.deltaTime;

            if (dropTimer < Mathf.Epsilon)
            {
                if (targetPlayer.GetComponent<PlayerLogic>().getFruitCounter() != 0)
                {
                    targetPlayer.GetComponent<PlayerLogic>().loseFruits(1);
                }
                
                dropTimer = fruitDropTime;
            }

            if (targetPlayer.GetComponent<PlayerLogic>().getFruitCounter() == 0)
            {
                // when player fruit count reaches zero, respawn player to base
                waitScript.DoCoroutine(waitTime);
                
            }    
        }
        
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        targetPlayer.GetComponent<PlayerController>().getAnimator().SetBool("isCaught", false);
        targetPlayer.GetComponent<PlayerController>().getAnimator().SetBool("isGettingUp", true);
        //targetPlayer.GetComponent<PlayerLogic>().spawn();

        witchLogic.stopChasing();

        canPlayerResist = false;
        //targetPlayer.GetComponent<PlayerLogic>().enableControls(); moved to PlayerRespawnBehaviour.cs
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
