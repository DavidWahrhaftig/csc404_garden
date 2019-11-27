using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [SerializeField] AudioClip last30SecondsTrack;
    [SerializeField] AudioClip gameOverTrack;
    [SerializeField] AudioSource merlinAudio;

    [SerializeField] AudioClip merlinLast30Second, merlinGameOver;

    GameManager gameManager;
    AudioSource audioSource;

    bool isPlayingMerlinSound = false;
    

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

        if (gameManager.getTimeRemaining() <= 27f && !isPlayingMerlinSound && !gameManager.isGameOver())
        {
            isPlayingMerlinSound = true;
            merlinAudio.PlayOneShot(merlinLast30Second);
   
        }

        if (gameManager.isGameOver())
        {
            
            if (!isGamOverTrackPlaying)
            {
                isGamOverTrackPlaying = true;
                audioSource.Stop();
                audioSource.clip = gameOverTrack;
                audioSource.Play();

                if(gameManager.getPlayer(1).GetComponent<PlayerLogic>().getFruitCounter() != gameManager.getPlayer(2).GetComponent<PlayerLogic>().getFruitCounter()) // only when there isn't a tie, tell the winner congratulations
                {
                    Invoke("delayMerlinGameOverPhrase", 2f); // a nice delay
                }
               

            }
        }

    }

    void delayMerlinGameOverPhrase()
    {
        merlinAudio.PlayOneShot(merlinGameOver);
    }
}
