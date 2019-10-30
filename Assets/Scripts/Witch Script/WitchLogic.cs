using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchLogic : MonoBehaviour
{
    // Start is called before the first frame update

    public GameManager gameManager;

    public AudioClip laughSound;
    public AudioClip chasingSound;
    public AudioClip patrollingSound;
    public AudioClip complaningSound;
    public AudioClip idleSound;

    private AudioSource audioSource;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }

    public void playSound(AudioClip audioClip)
    {
        audioSource.Stop();
        audioSource.PlayOneShot(audioClip);
    }
}
