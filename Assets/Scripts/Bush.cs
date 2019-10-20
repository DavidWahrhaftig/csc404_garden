using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{
    private GameManager gameManager;
    PlayerLogic pl;

    // Start is called before the first frame update
    void Start()
    {
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
            pl = gameManager.getTargetPlayer().GetComponent<PlayerLogic>();
            pl.setIsHidden(true);
            gameManager.playSound(gameManager.witchComplaningSound);
        }
    }

    private void OnTriggerExit(Collider other)
    { 
        if (pl != null)
        {
            pl.setIsHidden(false);
        }
        
    }
}
