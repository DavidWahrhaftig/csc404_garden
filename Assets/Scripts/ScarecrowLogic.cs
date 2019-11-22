﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScarecrowLogic : MonoBehaviour
{
    [SerializeField]
    private float fruitSnatchTimeThreshold = 1f;
    private float fruitSnatchTimer;
    public int snatchQuantity;

    public bool isActive = true;

    private void Start()
    {
        gameObject.SetActive(isActive);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collision Tag: " + other.transform.tag);

        if (other.transform.tag == "Player1" || other.transform.tag == "Player2")
        {
            other.GetComponent<PlayerLogic>().loseFruits(snatchQuantity, true);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player1" || other.transform.tag == "Player2")
        {
            fruitSnatchTimer += Time.deltaTime;
            if (fruitSnatchTimer > fruitSnatchTimeThreshold)
            {
                other.GetComponent<PlayerLogic>().loseFruits(snatchQuantity, true);
                //Debug.Log("Is Stopped :: " + navMeshAgent.isStopped);
                fruitSnatchTimer = 0;
            }

        }
    }

}
