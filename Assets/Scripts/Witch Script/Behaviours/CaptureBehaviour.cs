﻿using System.Collections;
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
    public int fruitLossRate = 1; // one fruit every fruitDropTime
    private float dropTimer;
    private bool freePlayer = false;

    private float originalFruitDropTime;
    private int originalFruitLossRate;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        waitScript = animator.GetComponent<CaptureWait>();

        witchLogic = animator.GetComponent<WitchLogic>();
        witchLogic.playSound(witchLogic.laughSound);
        targetPlayer = witchLogic.getTargetPlayer();
        targetPlayer.GetComponent<PlayerLogic>().gotCaught(); // make player disabled


        originalFruitDropTime = fruitDropTime;
        originalFruitLossRate = fruitLossRate;

        //goal = targetPlayer.GetComponent<PlayerLogic>().getFruitCounter() * 5;

        goal = getGoal(targetPlayer.GetComponent<PlayerLogic>().getFruitCounter());

        dropTimer = fruitDropTime;

        gameManager = witchLogic.getGameManager();

      

        
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

                canPlayerResist = true;
                targetPlayer.GetComponentInChildren<CameraShake>().setCanShake(true);

                if (targetPlayer.GetComponent<PlayerLogic>().getFruitCounter() > 0)
                {
                    // activate prompt message
                    gameManager.activateResistanceSlider(targetPlayer);

                    
  
                }
            }

            

        }


        if (canPlayerResist && !freePlayer)
        {
            // start decremeanting fruits slowly 
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
                    targetPlayer.GetComponent<PlayerLogic>().loseFruits(fruitLossRate);
                }

                dropTimer = fruitDropTime;
            }

            if (targetPlayer.GetComponent<PlayerLogic>().getFruitCounter() == 0 || playerPower >= goal)
            {
                targetPlayer.GetComponentInChildren<CameraShake>().setCanShake(false);
                waitScript.DoCoroutine(waitTime);
                freePlayer = true;

                
            }
        }
        
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        targetPlayer.GetComponent<Animator>().SetBool("isCaught", false);
        targetPlayer.GetComponent<Animator>().SetBool("isGettingUp", true);
        //deactivate prompt message
        gameManager.deactivateResistanceSlider(targetPlayer);
        witchLogic.stopChasing();

        // reset fields
        canPlayerResist = false;
        freePlayer = false;
        targetPlayer.GetComponentInChildren<CameraShake>().setPower(0f);
        targetPlayer.GetComponentInChildren<CameraShake>().setResistanceMeter(0f);

        fruitLossRate = originalFruitLossRate;
        fruitDropTime = originalFruitDropTime;
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

    private float getGoal(int fruitCount)
    {
        goal = 0f;

        if (fruitCount <= 5)
        {
            goal = fruitCount * 4f;
        }
        else if (fruitCount <= 10)
        {
            goal = fruitCount * 4f;
            fruitDropTime *= 1.2f;

        } else if (fruitCount <= 15)
        {
            goal = fruitCount * 3f;
            //fruitDropTime = fruitDropTime * 0.8f;

        } else if (fruitCount <= 25)
        {
            goal = 40f + (fruitCount - 10) * 1;
            fruitDropTime = fruitDropTime * 1.1f;
            fruitLossRate *= 2;
        }

        else
        {
            goal = 40f + (fruitCount - 10) * 1;
            fruitDropTime = fruitDropTime * 1.3f;
            fruitLossRate *= 2;
        }



        return goal;
    }

}
