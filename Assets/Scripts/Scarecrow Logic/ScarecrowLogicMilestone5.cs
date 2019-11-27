using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScarecrowLogicMilestone5 : MonoBehaviour
{
    [SerializeField] int snatchQuantity = 1;
    [SerializeField] float fruitSnatchTimeThreshold = 3f;

    [SerializeField] Transform scarecrowRotation;
    [SerializeField] Animator scarecrowAnimator;

    [Range(0, 1)] public float rotationSpeed = 0.1f;
    private Vector3 direction;

    private float fruitSnatchTimer = 0f;


    private void Start()
    {
        //gameObject.SetActive(isActive);
    }



    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collision Tag: " + other.transform.tag);
        if (other.transform.tag == "Player1" || other.transform.tag == "Player2")
        {
            scarecrowAnimator.SetTrigger("scare");
            if (canHarmPlayer(other.gameObject))
            {
                other.GetComponent<PlayerLogic>().loseFruits(snatchQuantity, true);
            }
        }

    }


    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player1" || other.transform.tag == "Player2")
        {

            direction = GameObject.FindGameObjectWithTag(other.transform.tag).transform.position - scarecrowRotation.position;
            scarecrowRotation.rotation = Quaternion.Slerp(scarecrowRotation.rotation, Quaternion.LookRotation(direction), rotationSpeed);

            if (canHarmPlayer(other.gameObject))
            {
                fruitSnatchTimer += Time.deltaTime;
                if (fruitSnatchTimer > fruitSnatchTimeThreshold)
                {
                    other.GetComponent<PlayerLogic>().loseFruits(snatchQuantity, true);
                    //Debug.Log("Is Stopped :: " + navMeshAgent.isStopped);
                    fruitSnatchTimer = 0f;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player1" || other.transform.tag == "Player2")
        {
            scarecrowAnimator.SetTrigger("idle");
        }

    }
    private bool canHarmPlayer(GameObject player)
    {
        return !FindObjectOfType<GameManager>().isGameOver() && !player.GetComponent<PlayerLogic>().isCaught() && !player.GetComponent<PlayerLogic>().isDisabled();
    }

}
