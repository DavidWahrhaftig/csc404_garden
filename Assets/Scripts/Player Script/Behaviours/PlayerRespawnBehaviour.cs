﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawnBehaviour : StateMachineBehaviour
{
    PlayerLogic playerLogic;
    PlayerController playerController;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerLogic = animator.transform.GetComponent<PlayerLogic>();
        playerController = animator.transform.GetComponent<PlayerController>();
        //playerLogic.setIsCaught(false);
        playerController.setGravity(true);
        playerLogic.stopChasingMe();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // changing state to Idle, so player is not caught anymore and then controls get enabled
        //playerLogic.setIsCaught(false); 
        playerLogic.enableControls(); // transition to Idle state so controls are enabled
    }
}
