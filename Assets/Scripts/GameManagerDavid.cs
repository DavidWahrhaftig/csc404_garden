using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerDavid : MonoBehaviour
{

    public Transform player1;
    public Transform player2;
    public Transform targetPlayer;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void setTargetPlayer(Transform target)
    {
        this.targetPlayer = target;
    }

    public Transform getTargetPlayer()
    {
        return this.targetPlayer;
    }
}
