using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchPair : MonoBehaviour
{
    // Start is called before the first frame update

    public Torch[] torches = new Torch[2];

    string lastPlayerEneterd = null;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1" && lastPlayerEneterd != "Player1")
        {
            lastPlayerEneterd = "Player1";
            torches[0].makeFlameRed();
            torches[1].makeFlameRed();

        }
        else if (other.tag == "Player2" && lastPlayerEneterd != "Player2")
        {
            lastPlayerEneterd = "Player2";
            torches[0].makeFlameBlue();
            torches[1].makeFlameBlue();
        }
    }
}
