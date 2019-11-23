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
        if (!FindObjectOfType<GameManager>().isGameOver())
        {
            if (other.transform.tag == "Player1" || other.transform.tag == "Player2")
            {
                if (!other.GetComponent<PlayerLogic>().isCaught() && !other.GetComponent<PlayerLogic>().isDisabled())
                {
                    other.GetComponent<PlayerLogic>().loseFruits(snatchQuantity, true);
                }
            }
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (!FindObjectOfType<GameManager>().isGameOver())
        {
            if (other.transform.tag == "Player1" || other.transform.tag == "Player2")
            {
                if (!other.GetComponent<PlayerLogic>().isCaught() && !other.GetComponent<PlayerLogic>().isDisabled())
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
    }

}
