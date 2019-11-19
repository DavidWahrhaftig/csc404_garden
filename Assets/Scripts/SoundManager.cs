﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [SerializeField] AudioClip last30SecondsTrack;
    [SerializeField] AudioClip gameOverTrack;

    GameManager gameManager;
    AudioSource audioSource;

    bool isLast30SecondsTrackPlaying = false;
    bool isGamOverTrackPlaying = false;

    void Start()
    {
        gameManager = GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (gameManager.getTimeRemaining() <= 30f)
        {
            // play the last 30 seconds track
            if (!isLast30SecondsTrackPlaying)
            {
                isLast30SecondsTrackPlaying = true;
                audioSource.Stop();
                audioSource.PlayOneShot(last30SecondsTrack);
            }
           
        }

        if (gameManager.isGameOver())
        {
            
            if (!isGamOverTrackPlaying)
            {
                isGamOverTrackPlaying = true;
                audioSource.Stop();
                
            }

            if(!audioSource.isPlaying) // to loop game over track
            {
                audioSource.PlayOneShot(gameOverTrack);
            }

        }

    }
}