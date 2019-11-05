using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureBehaviour : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    [Header("Capture Settings")]
    public float captureSpeed = 3.5f;
    public float waitTime = 1;
    private Transform targetPlayer;
    private CaptureWait waitScript;
    WitchLogic witchLogic;
    GameManager gameManager;

    [Header("Player-Witch Resistance Settings")]
    public float fruitDropTime = 1f;
    public bool canPlayerResist = false;
    public float playerPower;
    public float goal;
    private float dropTimer;
    private bool freePlayer = false;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        waitScript = animator.GetComponent<CaptureWait>();

        witchLogic = animator.GetComponent<WitchLogic>();
        witchLogic.playSound(witchLogic.laughSound);
        targetPlayer = witchLogic.getTargetPlayer();
        targetPlayer.GetComponent<PlayerLogic>().gotCaught(); // make player disabled

        goal = targetPlayer.GetComponent<PlayerLogic>().getFruitCounter() * 5;

        dropTimer = fruitDropTime;

        gameManager = witchLogic.getGameManager();

        gameManager.activateResistanceSlider(targetPlayer);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPower = targetPlayer.GetComponentInChildren<CameraShake>().getPower();

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


        if (canPlayerResist && !freePlayer)
        {
            // start decremeanting fruits slowly (1 every 0.75 seconds)
            dropTimer -= Time.deltaTime;

            if (goal >= 1f)
            {
                targetPlayer.GetComponentInChildren<CameraShake>().setResistanceMeter(playerPower / goal);
            }
            else
            {
                targetPlayer.GetComponentInChildren<CameraShake>().setResistanceMeter(0f);
            }


            if (dropTimer < Mathf.Epsilon)
            {
                if (targetPlayer.GetComponent<PlayerLogic>().getFruitCounter() != 0)
                {
                    targetPlayer.GetComponent<PlayerLogic>().loseFruits(1);
                }

                dropTimer = fruitDropTime;
            }

            if (targetPlayer.GetComponent<PlayerLogic>().getFruitCounter() == 0 || playerPower >= goal)
            {
                waitScript.DoCoroutine(waitTime);
                //canPlayerResist = false;
                freePlayer = true;
            }
        }
        
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        targetPlayer.GetComponent<Animator>().SetBool("isCaught", false);
        targetPlayer.GetComponent<Animator>().SetBool("isGettingUp", true);

        gameManager.deactivateResistanceSlider(targetPlayer);
        witchLogic.stopChasing();

        // reset fields
        canPlayerResist = false;
        freePlayer = false;
        targetPlayer.GetComponentInChildren<CameraShake>().setPower(0f);
        targetPlayer.GetComponentInChildren<CameraShake>().setResistanceMeter(0f);
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
