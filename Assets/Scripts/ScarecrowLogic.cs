using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScarecrowLogic : MonoBehaviour
{
    [SerializeField]
    private float fruitSnatchTimeThreshold = 1f;
    private float fruitSnatchTimer;
    public int snatchCounter;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collision Tag: " + other.transform.tag);

        if (other.transform.tag == "Player1" || other.transform.tag == "Player2")
        {

            if (other.GetComponent<PlayerLogic>().getFruitCounter() >= snatchCounter)
            {
                other.GetComponent<PlayerLogic>().loseFruits(snatchCounter);
            }

        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player1" || other.transform.tag == "Player2")
        {
            fruitSnatchTimer += Time.deltaTime;
            if (fruitSnatchTimer > fruitSnatchTimeThreshold && other.GetComponent<PlayerLogic>().getFruitCounter() >= snatchCounter)
            {
                other.GetComponent<PlayerLogic>().loseFruits(snatchCounter);
                //Debug.Log("Is Stopped :: " + navMeshAgent.isStopped);
                fruitSnatchTimer = 0;
            }

        }
    }

}
