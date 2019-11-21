using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGateScript : MonoBehaviour
{
    // Start is called before the first frame update



    [SerializeField] GameObject gateBlock;
    
    [SerializeField] AudioClip openSound, closeSound;

    AudioSource audioSource;

    bool isGateOpen = false;

    bool insidePlayer1 = false;
    bool insidePlayer2 = false;

    void Start()
    {
        gateBlock.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (insidePlayer1 && insidePlayer2 && isGateOpen)
        {
            closeGate();
        }
    }
    
    public void setInsidePlayer1(bool b)
    {
        insidePlayer1 = b;
    }

    public void setInsidePlayer2(bool b)
    {
        insidePlayer2 = b;
    }


    public void openGate()
    {
        
        if (!isGateOpen)
        {
            isGateOpen = true;
            
            gateBlock.SetActive(false);
            GetComponent<Animator>().SetTrigger("open");

            // play open gate sound
            audioSource.PlayOneShot(openSound);
        }
        
    }

    public void closeGate()
    {
        isGateOpen = false;
        gateBlock.SetActive(true);
        GetComponent<Animator>().SetTrigger("close");
        
        //play close gate sound
        audioSource.PlayOneShot(openSound);
    }

}
