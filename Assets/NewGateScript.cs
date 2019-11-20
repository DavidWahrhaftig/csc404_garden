using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGateScript : MonoBehaviour
{
    // Start is called before the first frame update

    //public Collider beforeGate;
    //public Collider afterGate;

    //GameObject[] players;
    bool player1Entered = false;
    bool player2Entered = false;

    bool player1Exited = false;
    bool player2Exited = false;

    public GameObject gateBlock; 

    void Start()
    {
        gateBlock.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player1")
        {
            if (!player1Entered)
            {
                player1Entered = true;    
            }
        } else if (other.gameObject.tag == "Player2")
        {
            if (!player2Entered)
            {
                player2Entered = true;
            }
        }

        if (player1Entered && player2Entered)
        {
            GetComponent<Animator>().SetTrigger("open");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player1")
        {
            player1Exited = true;
            
        }
        else if (other.gameObject.tag == "Player2")
        {

            player2Exited = true;

        }

        if (player1Exited && player2Exited)
        {
            GetComponent<Animator>().SetTrigger("close");
            gateBlock.SetActive(true);
        }
    }


}
