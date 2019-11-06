using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Transform[] allowedPlayers;
    [SerializeField] AudioClip openSound, closeSound;
    private Animator animator;
    private AudioSource audioSource;



    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPermitted(other.gameObject))
        {
            animator.SetTrigger("open");
            audioSource.PlayOneShot(openSound);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isPermitted(other.gameObject))
        {
            animator.SetTrigger("close");
            audioSource.PlayOneShot(closeSound);
        }
    }


    bool isPermitted(GameObject player)
    {
        for (int i = 0; i < allowedPlayers.Length; i++)
        {
            if (player.tag == allowedPlayers[i].gameObject.tag)
            {
                return true;
            }
        }

        return false;
    }
}
