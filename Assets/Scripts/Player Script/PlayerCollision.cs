using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnCollisionEnter(Collision collision)
    //{
        //if (collision.transform.tag == "Ground")
          //  playerController.setIsJumping(true);
        /**
        if (collision.transform.tag == "FruitBush")
        {
            GameObject fruitBush = GameObject.Find(collision.transform.name);
            fruitBushScript = fruitBush.GetComponent<FruitBushScript>();
            playFruitSound();
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
            //numFruits += fruitBushScript.fruitsInBush;

            //FindObjectOfType<GameManager>().updateFruitCount(1); //added
            fruitBushScript.fruitsInBush = 0;
        }

        if (collision.transform.tag == "Witch")
        {
            audioSource.Stop();
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
            transform.position = playerBase.transform.position;
            // gameLost = true;
            FindObjectOfType<GameManager>().GameLost(); //added
        }

        if (collision.transform.tag == "Ground")
            isJumping = true;

        if (collision.gameObject.name == enemyProjectile.name + "(Clone)")
        {
            //Debug.Log("COLOR CHANGE!!!");
            var playerRend = gameObject.GetComponent<Renderer>();
            ogColor = playerRend.material.GetColor("_Color");
            playerRend.material.SetColor("_Color", Color.white);
            isLightUp = true;

            // change witch state to chase
            Animator witchAnimator = gameManager.getWitch().GetComponent<Animator>();
            witchAnimator.SetBool("isChasing", true);
            witchAnimator.SetBool("isIdle", false);
            witchAnimator.SetBool("isPatrolling", false);
            gameManager.setTargetPlayer(transform);

            // disable this player from shooting
        
        }
    **/
    //}
}
