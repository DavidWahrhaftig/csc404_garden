using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerReference : MonoBehaviour
{
    public GameManager gameManager;

    [SerializeField] AudioClip chasingSound;

    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void playSound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}
