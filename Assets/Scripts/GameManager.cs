using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
public class GameManager : MonoBehaviour
{



    /***Texts ***/
    [Header("UI Settings")]
    public TextMeshProUGUI fruitCounter1;
    public TextMeshProUGUI fruitCounter2;
    public TextMeshProUGUI shotTimer1;
    public TextMeshProUGUI shotTimer2;
    public TextMeshProUGUI GameTimer;
    public TextMeshProUGUI gameResult1;
    public TextMeshProUGUI gameResult2;
    public TextMeshProUGUI countDownUI;
    public Slider slider1;
    public Slider slider2;

    public Image imageSlider1;
    public Image imageSlider2;

    /*** Audio ***/
    [Header("Game Audio Settings")]
    [SerializeField] AudioClip[] fruitSounds;
    public AudioClip gateOpenSound;

    [Header("Time Settings")]
    public float gameDuration = 60;
    public float restartDelay = 1f;
    public float countDownDuration = 7.0f;

    [Header("Game Objects Settings")]
    public Transform player1;
    public Transform player2;
    public Transform targetPlayer;
    public Transform witch;
    public Transform witchBase;


    private float startTime;
    private AudioSource audioSource;

    private float remainingTime;

    private float startCounterTime;

    private float remainingCountDownTime;
    private bool beginGame = false;



    private void Start()
    {

        remainingTime = gameDuration;
        remainingCountDownTime = countDownDuration;
        audioSource = GetComponent<AudioSource>();

        // set initial game UI
        fruitCounter1.text = "Fruit Count: 0";
        fruitCounter2.text = "Fruit Count: 0";
        //shotTimer1.text = "Orb Ammo: " + player1.GetComponent<SpawnLightOrb>().getAmmo();
        //shotTimer2.text = "Orb Ammo: " + player2.GetComponent<SpawnLightOrb>().getAmmo();
        shotTimer1.text = "Magic Meter";
        shotTimer2.text = "Magic Meter";
        gameResult1.text = "";
        gameResult2.text = "";
        GameTimer.text = "";

        startCounterTime = Time.time;

        for (int i = 0; i < Input.GetJoystickNames().Length; i++)
        {
            Debug.Log(Input.GetJoystickNames()[i]);
        }


        // disable both players
        player1.GetComponent<PlayerLogic>().disableControls();
        player2.GetComponent<PlayerLogic>().disableControls();
    }

    void Update()
    {
        // updating Ammo sliders
        slidersUpdate();

        // Force Restart
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }

        // Quit Game
        if (Input.GetKey(KeyCode.Q))
        {
            Application.Quit();
        }

        /*** UI Updates ***/

        startCountDown();

        if (beginGame)
        {
            fruitCounter1.text = "Fruit Count: " + player1.GetComponent<PlayerLogic>().getFruitCounter();
            fruitCounter2.text = "Fruit Count: " + player2.GetComponent<PlayerLogic>().getFruitCounter();

            //updateLightBallChargeUI();
            updateTimer();
        }


    }

    private void slidersUpdate()
    {
        slider1.value = player1.GetComponent<SpawnLightOrb>().magicCharge / 100f;
        slider2.value = player2.GetComponent<SpawnLightOrb>().magicCharge / 100f;
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

    /*
    private void updateLightBallChargeUI()
    {
        if (player1.GetComponent<SpawnLightOrb>().getSpawnTimer() != player1.GetComponent<SpawnLightOrb>().getReloadTime())
        {
            shotTimer1.text = "Charging ... " + player1.GetComponent<SpawnLightOrb>().getSpawnTimer().ToString("f0");
        }
        else
        {
            shotTimer1.text = "Orb Ammo: " + player1.GetComponent<SpawnLightOrb>().getAmmo();
        }

        if (player2.GetComponent<SpawnLightOrb>().getSpawnTimer() != player2.GetComponent<SpawnLightOrb>().getReloadTime())
        {
            shotTimer2.text = "Charging ... " + player2.GetComponent<SpawnLightOrb>().getSpawnTimer().ToString("f0");
        }
        else
        {
            shotTimer2.text = "Orb Ammo: " + player2.GetComponent<SpawnLightOrb>().getAmmo();
        }
    }
    */

    void Restart()
    {
        SceneManager.LoadScene(0); //game scene at index 0 until we get the menu scene
    }

    public Transform getTargetPlayer()
    {
        return this.targetPlayer;
    }

    public void setTargetPlayer(Transform target)
    {
        this.targetPlayer = target;
    }

    public Transform getWitch()
    {
        return this.witch;
    }

    public Transform getWitchBase()
    {
        return witchBase;
    }

    public void playFruitSound()
    {
        if (fruitSounds.Length == 0) { return; } // if no sounds were added
        int index = UnityEngine.Random.Range(0, fruitSounds.Length);

        if (!audioSource.isPlaying) // so it doesn't layer
        {
            audioSource.PlayOneShot(fruitSounds[index]);
        }
    }

    public void playSound(AudioClip audio)
    {
        if (!audioSource.isPlaying) // so it doesn't layer
        {
            audioSource.PlayOneShot(audio);
        }
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


}
