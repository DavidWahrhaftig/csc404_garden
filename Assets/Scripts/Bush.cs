using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{
    private WitchLogic witchLogic;
    private GameManager gameManager;
    PlayerLogic playerLogic;

    // Start is called before the first frame update
    void Start()
    {
        witchLogic = FindObjectOfType<WitchLogic>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameManager.getTargetPlayer() != null && other.gameObject == gameManager.getTargetPlayer().gameObject)
        {
            playerLogic = gameManager.getTargetPlayer().GetComponent<PlayerLogic>();
            playerLogic.setIsHidden(true);
            witchLogic.playSound(witchLogic.complaningSound);
            //gameManager.playSound(gameManager.witchComplaningSound);
        }
    }

    private void OnTriggerExit(Collider other)
    { 
        if (playerLogic != null)
        {
            playerLogic.setIsHidden(false);
        }
        
    }
}
