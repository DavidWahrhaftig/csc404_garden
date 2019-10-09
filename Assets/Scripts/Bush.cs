using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{
    private GameManager gameManager;
    PlayerController pc;
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
        print("player in Bush");
        if (gameManager.getTargetPlayer() != null && other.gameObject == gameManager.getTargetPlayer().gameObject)
        {
            PlayerController pc = gameManager.getTargetPlayer().GetComponent<PlayerController>();
            pc.setIsHidden(true);
            gameManager.playSound(gameManager.witchComplaningSound);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        print("Left the Bush");  
        if (pc != null)
        {
            pc.setIsHidden(false);
        }
        
    }
}
