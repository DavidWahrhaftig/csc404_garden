using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour
{

    Animator gateAnim;

    // Start is called before the first frame update
    void Start()
    {
        gateAnim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player1" || other.transform.tag == "Player2")
        {
            gateAnim.SetTrigger("OpenGate");
        }
            
    }

    private void HoldDoorOpen()
    {
        gateAnim.enabled = false;
    }
}
