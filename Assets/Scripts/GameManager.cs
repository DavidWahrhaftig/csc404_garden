using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
public class GameManager : MonoBehaviour
{
    /***UI Texts ***/
    [Header("UI Settings")]
    public TextMeshProUGUI fruitCounter1;
    public TextMeshProUGUI fruitCounter2;
    public TextMeshProUGUI shotTimer1;
    public TextMeshProUGUI shotTimer2;
    public TextMeshProUGUI GameTimer;
    public TextMeshProUGUI gameResult1;
    public TextMeshProUGUI gameResult2;
    public TextMeshProUGUI countDownUI;
    public Slider magicSlider1;
    public Slider magicSlider2;
    public Slider resistanceSlider1;
    public Slider resistanceSlider2;

    public Image imageSlider1;
    public Image imageSlider2;


    [Header("Time Settings")]
    public float gameDuration = 60;
    public float restartDelay = 1f;
    public float countDownDuration = 7.0f;

    [Header("Game Objects Settings")]
    public Transform player1;
    public Transform player2;
    public Transform witch;


    private float startTime;
    private AudioSource audioSource;

    private float remainingTime;

    private float startCounterTime;

    private float remainingCountDownTime;
    private bool beginGame = false;
    private bool gameOver = false;

    private int trackNumberPlaying = 1;

    private void Start()
    {

        remainingTime = gameDuration;
        remainingCountDownTime = countDownDuration;

        // set initial game UI
        fruitCounter1.text = "0";
        fruitCounter2.text = "0";
        //shotTimer1.text = "Orb Ammo: " + player1.GetComponent<SpawnLightOrb>().getAmmo();
        //shotTimer2.text = "Orb Ammo: " + player2.GetComponent<SpawnLightOrb>().getAmmo();
        shotTimer1.text = "Magic Meter";
        shotTimer2.text = "Magic Meter";
        gameResult1.text = "";
        gameResult2.text = "";
        GameTimer.text = "";

        startCounterTime = Time.time;
               
        // disable both players
        player1.GetComponent<PlayerLogic>().disableControls();
        player2.GetComponent<PlayerLogic>().disableControls();

        resistanceSlider1.gameObject.SetActive(false);
        resistanceSlider2.gameObject.SetActive(false);
    }

    void Update()
    {


        // Force Restart from Keyboard
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }

        // Quit Game from Keyboard
        if (Input.GetKey(KeyCode.Q))
        {
            Application.Quit();
        }

        /*** UI Updates ***/

        startCountDown();

        // updating Ammo sliders
        updateSliders();

        if (beginGame)
        {
            fruitCounter1.text = player1.GetComponent<PlayerLogic>().getFruitCounter().ToString();
            fruitCounter2.text = player2.GetComponent<PlayerLogic>().getFruitCounter().ToString();

            updateTimer();
        }

        resistanceSlider1.value = player1.GetComponentInChildren<CameraShake>().getResistanceMeter();
        resistanceSlider2.value = player2.GetComponentInChildren<CameraShake>().getResistanceMeter();

    }

    private void updateSliders()
    {
        magicSlider1.value = player1.GetComponent<SpawnLightOrb>().magicCharge / 100f;
        magicSlider2.value = player2.GetComponent<SpawnLightOrb>().magicCharge / 100f;
    }

    private void updateTimer()
    {
        remainingTime = gameDuration - (Time.time - startTime);

        string minutes = ((int)remainingTime / 60).ToString();
        string seconds = (remainingTime % 60).ToString("f0");

        if (seconds == "60")
        {
            seconds = "0";
            minutes = ((int)Math.Ceiling(remainingTime) / 60).ToString();
        }



        // winner detection
        if (remainingTime <= Mathf.Epsilon)
        {
            gameOver = true;
            

            if (player1.GetComponent<PlayerLogic>().getFruitCounter() > player2.GetComponent<PlayerLogic>().getFruitCounter())
            {
                gameResult1.text = "Merlin's Apprentice";
                gameResult2.text = "Unemployed";
                player1.GetComponent<PlayerController>().won();
                player2.GetComponent<PlayerController>().lose();


            }
            else if (player1.GetComponent<PlayerLogic>().getFruitCounter() < player2.GetComponent<PlayerLogic>().getFruitCounter())
            {
                gameResult2.text = "Merlin's Apprentice";
                gameResult1.text = "Unemployed";
                player1.GetComponent<PlayerController>().lose();
                player2.GetComponent<PlayerController>().won();
            }
            else
            {
                gameResult1.text = "No One Likes Ties\nPlay Again";
                gameResult2.text = "No One Likes Ties\nPlay Again";
                player1.GetComponent<PlayerController>().lose();
                player2.GetComponent<PlayerController>().lose();
            }



            // restart game when pressing Y or triangle button
            if (player1.GetComponent<PlayerController>().getGamePadController().GetButtonDown("Restart") || player2.GetComponent<PlayerController>().getGamePadController().GetButtonDown("Restart"))
            {
                Restart();
            }
        }
        else
        {
            if (Int32.Parse(seconds) < 10) { seconds = "0" + seconds; }

            GameTimer.text = minutes + ":" + seconds;
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(0); //game scene at index 0 until we get the menu scene
    }

    public Transform getWitch()
    {
        return this.witch;
    }


    private void startCountDown()
    {

        remainingCountDownTime = countDownDuration - (Time.time - startCounterTime);

        string seconds = remainingCountDownTime.ToString("f0");


        if (remainingCountDownTime > Mathf.Epsilon)
        {

            countDownUI.text = "Game Begins in \n" + seconds;

        }
        else
        {
            countDownUI.text = "";
            if (!beginGame)
            {
                startTime = Time.time;
                player1.GetComponent<PlayerLogic>().enableControls();
                player2.GetComponent<PlayerLogic>().enableControls();
            }

            beginGame = true;
        }
    }

    public void activateResistanceSlider(Transform player)
    {
        if (player.gameObject.tag == "Player1")
        {
            resistanceSlider1.gameObject.SetActive(true);

        } else if (player.gameObject.tag == "Player2")
        {
            resistanceSlider2.gameObject.SetActive(true);
        }
    }

    public void deactivateResistanceSlider(Transform player)
    {
        if (player.gameObject.tag == "Player1")
        {
            resistanceSlider1.gameObject.SetActive(false);

        }
        else if (player.gameObject.tag == "Player2")
        {
            resistanceSlider2.gameObject.SetActive(false);
        }
    }

    public Transform getPlayer(int playerNum)
    {
        if (playerNum == 1)
        {
            return this.player1;
        }

        if (playerNum == 2)
        {
            return this.player2;
        }

        return null;
    }

    public bool isGameOver()
    {
        return this.gameOver;
    }

    public float getTimeRemaining()
    {
        return this.remainingTime;
    }
}
