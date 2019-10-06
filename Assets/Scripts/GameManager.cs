using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject player1;
    public GameObject player2;

    private GameObject targetPlayer;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }



    public void setTargetPlayer(GameObject target)
    {
        this.targetPlayer = target;
    }

    public GameObject getTargetPlayer()
    {
        return this.targetPlayer;
    }
}
