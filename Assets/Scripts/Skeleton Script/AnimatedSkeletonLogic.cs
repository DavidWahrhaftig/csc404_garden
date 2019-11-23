using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimatedSkeletonLogic : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    private int playersInProximity;
    [SerializeField]
    private float fruitSnatchTimeThreshold = 1f;
    private float fruitSnatchTimer;
    public int snatchQuantity;
    private Animator animator;
    private bool isIdleState;
    private bool isWalkingState;


    private void Start()
    {
        //Debug.Log("NavMesh Registered");
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collision Tag: " + other.transform.tag);
        if (!FindObjectOfType<GameManager>().isGameOver())
        {

            if (other.transform.tag == "Player1" || other.transform.tag == "Player2")
            {

                if (!other.GetComponent<PlayerLogic>().isCaught() || !other.GetComponent<PlayerLogic>().isDisabled())
                {
                    //Debug.Log("Skeleton spotted player! Stop!!!");
                    navMeshAgent.isStopped = true;
                    //Debug.Log("Is Stopped :: " + navMeshAgent.isStopped);
                    playersInProximity += 1;

                    // Rotate to face Player (Need to implement lock on player)


                    // Turn on Attack animation
                    animator.SetBool("isAttacking", true);
                    isIdleState = animator.GetBool("isIdle");
                    isWalkingState = animator.GetBool("isWalking");
                    animator.SetBool("isIdle", false);
                    animator.SetBool("isWalking", false);
                    
                }
            }
        }
        
	}


    //private void OnTriggerStay(Collider other)
    //{

    //    if (!FindObjectOfType<GameManager>().isGameOver())
    //    {
    //        if (other.transform.tag == "Player1" || other.transform.tag == "Player2")
    //        {
    //            if (!other.GetComponent<PlayerLogic>().isCaught() || !other.GetComponent<PlayerLogic>().isDisabled())
    //            {
    //                fruitSnatchTimer += Time.deltaTime;
    //                if (fruitSnatchTimer > fruitSnatchTimeThreshold)
    //                {
    //                    fruitSnatchTimer = 0;
    //                }
    //            }

    //        }
    //    }

    //}

    private void OnTriggerExit(Collider other)
    {
        if (!FindObjectOfType<GameManager>().isGameOver())
        {
            if (other.transform.tag == "Player1" || other.transform.tag == "Player2")
            {
                if (!other.GetComponent<PlayerLogic>().isCaught() || !other.GetComponent<PlayerLogic>().isDisabled())
                {
                    //Debug.Log("Skeleton lost sight of a player...");
                    playersInProximity -= 1;

                    if (playersInProximity == 0)
                    {
                        //Debug.Log("No more players to see. Resume!");
                        navMeshAgent.isStopped = false;
                        //Debug.Log("Is Stopped :: " + navMeshAgent.isStopped);


                        // Have to Unlock from player


                        //Turn off attacking animation
                        animator.SetBool("isAttacking", false);
                        animator.SetBool("isIdle", isIdleState);
                        animator.SetBool("isWalking", isWalkingState);

                    }
                }
            }
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    //Debug.Log("Collision Tag: " + other.transform.tag);

    //    if (collision.transform.tag == "Player1" || collision.transform.tag == "Player2")
    //    {

    //        collision.gameObject.GetComponent<PlayerLogic>().loseFruits(snatchQuantity, true);


    //    }
    //}



}
