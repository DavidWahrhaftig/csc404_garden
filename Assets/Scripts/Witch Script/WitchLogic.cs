using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchLogic : MonoBehaviour
{
    // Start is called before the first frame update

    public GameManager gameManager;

    public Transform witchBase;

    public Transform targetPlayer = null;

    public AudioClip laughSound;
    public AudioClip chasingSound;
    public AudioClip patrollingSound;
    public AudioClip complaningSound;
    public AudioClip idleSound;

    private Animator witchAnimator;
    private AudioSource audioSource;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioSource = GetComponent<AudioSource>();
        witchAnimator = GetComponent<Animator>();
    }

    public void playSound(AudioClip audioClip)
    {
        audioSource.Stop();
        audioSource.PlayOneShot(audioClip);
    }

    public void chase(Transform player)
    {
        setTargetPlayer(player);

        witchAnimator.SetBool("isChasing", true);
        witchAnimator.SetBool("isIdle", false);
        witchAnimator.SetBool("isPatrolling", false);
    }

    public void stopChasing()
    {
        witchAnimator.SetBool("isChasing", false);
        witchAnimator.SetBool("isIdle", true);
        //witchAnimator.SetBool("isPatrolling", false);

        targetPlayer = null;
    }

    public Transform getTargetPlayer()
    {
        return targetPlayer;
    }

    public void setTargetPlayer(Transform player)
    {
        targetPlayer = player;
    }

    public Transform getWitchBase()
    {
        return witchBase;
    }

  
}
